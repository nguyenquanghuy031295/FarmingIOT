using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Exceptions
{
    public class UnAuthorizedException: Exception
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public UnAuthorizedException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            this.ErrorMessages = new[] { message };
        }

        public UnAuthorizedException(IEnumerable<string> messages, Exception innerException = null)
            : base(string.Join("\r\n", messages), innerException)
        {
            ErrorMessages = messages;
        }
    }
}
