using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Command
{
    public enum SignalPeriod
    {
        IsAvailable = 0,
        IsLastPeroid = 1,
        IsNotEnoughDay = 2,
        IsNotAvailable = 3
    }
    public class ChangePeriodSignal
    {
        public SignalPeriod Signal { get; set; }
    }
}
