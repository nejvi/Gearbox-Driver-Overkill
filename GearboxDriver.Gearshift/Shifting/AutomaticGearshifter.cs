using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift.Shifting
{
    public class AutomaticGearshifter
    {
        private IGearbox Gearbox { get; }
        private IShiftingProgram Program { get; set; }

        public AutomaticGearshifter(IGearbox gearbox)
        {
            Gearbox = gearbox;
            Program = new YieldingShiftingProgram();
        }

        public void HandleRpmChange(Rpm rpm)
        {
            var suggestedAction = Program.GetSuggestedActionFor(Gearbox.CurrentGear, rpm);

            if (suggestedAction == SuggestedAction.Upshift)
                Gearbox.Upshift();
            
            if (suggestedAction == SuggestedAction.Downshift)
                Gearbox.Downshift();
        }

        public void SetProgram(IShiftingProgram program)
        {
            Program = program;
        }
    }
}
