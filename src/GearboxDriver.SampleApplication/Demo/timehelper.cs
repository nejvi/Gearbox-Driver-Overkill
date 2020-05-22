using System;
using System.Threading.Tasks;

namespace GearboxDriver.SampleApplication.Demo
{
    class TimeHelper
    {
        public static void WaitSeconds(int seconds)
        {
            Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
        }

        public static void PlayMessage(string message, int delayAfterInSeconds)
        {
            Console.WriteLine(message);
            Task.Delay(TimeSpan.FromSeconds(delayAfterInSeconds)).Wait();
        }
    }
}
