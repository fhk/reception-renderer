using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReceptionRenderer.Antenna.Events
{
    public struct AddAntennaEvent
    {
        public Antenna Antenna { get; set; }

        public AddAntennaEvent(Antenna antenna)
        {
            Antenna = antenna;
        }
    }
}