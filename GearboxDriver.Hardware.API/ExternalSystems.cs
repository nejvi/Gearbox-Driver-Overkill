using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.API
{
    public class ExternalSystems
    {
        private double currentRpm;
        private double angularSpeed = 150;
        private Lights lights = new Lights();

        public double getCurrentRpm()
        {
            //sciagnij RPM z dostepnego miejsca
            return currentRpm;
        }

        public void setCurrentRpm(double currentRpm)
        {
            this.currentRpm = currentRpm;
        }

        public double getAngularSpeed()
        {
            return angularSpeed;
        }

        public void setAngularSpeed(double angularSpeed)
        {
            this.angularSpeed = angularSpeed;
        }

        public Lights getLights()
        {
            return lights;
        }
    }
}
