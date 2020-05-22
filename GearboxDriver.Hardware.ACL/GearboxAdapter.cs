using GearboxDriver.Hardware.API;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Hardware.ACL
{
    public class GearboxAdapter  : IGearbox
    {
        private readonly Gearbox _gearbox;
        private const int MaximumGear = 6;
        private const int MinimumDrivingGear = 1;

        public GearboxAdapter(Gearbox gearbox)
        {
            _gearbox = gearbox;
            _gearbox.setCurrentGear(0);
        }

        public void Upshift()
        {
            if ((int) _gearbox.getCurrentGear() < MaximumGear)
            {
                _gearbox.setCurrentGear((int)_gearbox.getCurrentGear() + 1);
            }
                
        }

        public void Downshift()
        {
            if ((int)_gearbox.getCurrentGear() > MinimumDrivingGear)
            { 
                _gearbox.setCurrentGear((int)_gearbox.getCurrentGear() - 1);
            }
        }

        public Gear CurrentGear => new Gear((int)_gearbox.getCurrentGear());
    }
}
