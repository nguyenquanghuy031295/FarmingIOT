using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Command
{
    /// <summary>
    /// Enum all signal can be of a Period
    /// </summary>
    public enum SignalPeriod
    {
        IsAvailable = 0, 
        IsLastPeroid = 1, 
        IsNotEnoughDay = 2,
        IsNotAvailable = 3
    }

    /// <summary>
    /// Sigal Period when user asking for changing to next Period
    /// </summary>
    public class ChangePeriodSignal
    {
        public SignalPeriod Signal { get; set; }
    }
}
