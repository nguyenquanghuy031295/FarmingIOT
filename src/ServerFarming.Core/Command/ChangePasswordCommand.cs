using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Command
{
    /// <summary>
    /// Command for changing password
    /// </summary>
    public class ChangePasswordCommand
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
