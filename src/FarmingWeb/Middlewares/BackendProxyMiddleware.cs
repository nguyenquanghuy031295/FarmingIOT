using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FarmingWeb.Middlewares
{
    public class BackendProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RESTProxyOptions _options;

        public BackendProxyMiddleware(RequestDelegate next, RESTProxyOptions options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            var cookieContainer = new CookieContainer();
            var requestMessage = new HttpRequestMessage();
            if (!string.Equals(context.Request.Method, "GET", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(context.Request.Method, "HEAD", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(context.Request.Method, "DELETE", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(context.Request.Method, "OPTIONS", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(context.Request.Method, "TRACE", StringComparison.OrdinalIgnoreCase))
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }

            // Copy the request headers
            foreach (var header in context.Request.Headers)
            {
                if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                {
                    requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            // Additional headers and request basic info

            var path = context.Request.Path.Value;

            var uriStringBuilder = new UriBuilder();
            uriStringBuilder.Host = _options.Host;
            uriStringBuilder.Scheme = _options.Scheme;
            uriStringBuilder.Port = _options.Port;
            uriStringBuilder.Path = PathString.FromUriComponent(context.Request.PathBase + path);
            uriStringBuilder.Query = context.Request.QueryString.Value;

            requestMessage.Headers.Host = uriStringBuilder.Uri.GetComponents(UriComponents.HostAndPort, UriFormat.UriEscaped);
            requestMessage.RequestUri = uriStringBuilder.Uri;
            requestMessage.Method = new HttpMethod(context.Request.Method);

            if (!requestMessage.Headers.Contains("Origin"))
                requestMessage.Headers.TryAddWithoutValidation("Origin", context.Request.Scheme + "://" + context.Request.Host.Value);

            var baseAddress = new Uri(
                uriStringBuilder.Uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped)
            );

            if (context.Request.Headers.ContainsKey("Cookie") && context.Request.Headers["Cookie"].Count > 0)
                foreach (var cookie in context.Request.Headers["Cookie"][0].Split(';').Select(x => x.Trim()))
                    cookieContainer.SetCookies(baseAddress, cookie);

            using (var httpClient = new HttpClient(new HttpClientHandler() { CookieContainer = cookieContainer }))
            using (var responseMessage = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                context.Response.StatusCode = (int)responseMessage.StatusCode;
                foreach (var header in responseMessage.Headers)
                    context.Response.Headers[header.Key] = header.Value.ToArray();

                foreach (var header in responseMessage.Content.Headers)
                    context.Response.Headers[header.Key] = header.Value.ToArray();

                // SendAsync removes chunking from the response. This removes the header so it doesn't expect a chunked response.
                context.Response.Headers.Remove("transfer-encoding");
                await responseMessage.Content.CopyToAsync(context.Response.Body);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BackendProxyMiddlewareExtensions
    {
        public static IApplicationBuilder UseBackendProxyMiddleware(
            this IApplicationBuilder builder,
            RESTProxyOptions options)
        {
            return builder.UseMiddleware<BackendProxyMiddleware>(options);
        }
    }

    public class RESTProxyOptions
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string RESTService { get; set; }
        public int Port { get; set; }
    }
}
