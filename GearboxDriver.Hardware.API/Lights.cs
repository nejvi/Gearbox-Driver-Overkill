using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.API
{
    public class Lights
    {
        int position;

        /**
            * null - brak opcji w samochodzie
            * 1-3 - w dół
            * 7-10 - w górę
        */

        public int getLightsPosition()
        {
            return position;
        }

        // method added to enable demo
        public void setLightsPosition(int position)
        {
            this.position = position;
        }
    }
}
