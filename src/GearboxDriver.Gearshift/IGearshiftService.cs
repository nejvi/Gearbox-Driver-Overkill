using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift
{
    public interface IGearshiftService
    {
        void AbstainFromChangingGears();
        void StopAbstainingFromChangingGears();
        void TargetGear(Gear gearNumber);
        void StopTargetingAnyGear();
        void KeepFollowingRpm(ShiftpointRange shiftpointRange);
        void DoEngineBraking();
        void StopDoingEngineBraking();
    }
}
