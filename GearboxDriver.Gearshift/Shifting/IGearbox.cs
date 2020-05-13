using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift.Shifting
{
    public interface IGearbox
    {
        void Upshift();
        void Downshift();

        Gear CurrentGear { get; }
    }
}
