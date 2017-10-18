using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Exceptions
{
    /// <summary>
    /// Exception user access some data not exist in database
    /// </summary>
    public class NoDataException : Exception
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public NoDataException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            this.ErrorMessages = new[] { message };
        }

        public NoDataException(IEnumerable<string> messages, Exception innerException = null)
            : base(string.Join("\r\n", messages), innerException)
        {
            ErrorMessages = messages;
        }
    }
}
