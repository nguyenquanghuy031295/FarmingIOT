﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Exceptions
{
    /// <summary>
    /// Exception when user want to change Period FAIL by some errors
    /// </summary>
    public class ChangePeriodException : Exception
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public ChangePeriodException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            this.ErrorMessages = new[] { message };
        }

        public ChangePeriodException(IEnumerable<string> messages, Exception innerException = null)
            : base(string.Join("\r\n", messages), innerException)
        {
            ErrorMessages = messages;
        }
    }
}