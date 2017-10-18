using FarmingDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    public enum Acutator_Status
    {
        Close = 0,
        Open = 1
    }
    /// <summary>
    /// Actuator Action
    /// </summary>
    public class Actuator_Action
    {
        public ActuatorType ActuatorType { get; set; }
        public Acutator_Status ActuatorStatus { get; set; }
    }
}
