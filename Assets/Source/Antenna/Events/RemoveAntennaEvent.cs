using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Antenna.Events
{
    public struct RemoveAntennaEvent
    {
        public Antenna Antenna { get; set; }

        public RemoveAntennaEvent(Antenna antenna)
        {
            Antenna = antenna;
        }
    }
}
