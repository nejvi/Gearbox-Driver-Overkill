using System;
using System.Threading.Tasks;
using GearboxDriver.CabinControls;
using GearboxDriver.Gearshift;
using GearboxDriver.Gearshift.Negotiation;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Hardware.API;
using GearboxDriver.Processes;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.SampleApplication.Demo;
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
           

            DemoShow.Play();

            

            Console.ReadKey();
        }

        public void Setup()
        {
            

        }
    }
}
