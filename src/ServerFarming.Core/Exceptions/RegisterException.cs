using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Exceptions
{
    public class RegisterException : Exception
    {
        private IEnumerable<string> messages;

        public IEnumerable<string> ErrorMessages { get; set; }

        public RegisterException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            this.ErrorMessages = new[] { message };
        }

        public RegisterException(IEnumerable<string> messages)
        {
            this.messages = messages;
        }
    }
}
