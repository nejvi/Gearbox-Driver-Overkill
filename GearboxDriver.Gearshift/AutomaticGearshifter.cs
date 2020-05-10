using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    public class Gearshifter
    {
        private IShiftingProgram Program { get; set; }
        private IGearbox Gearbox { get; set; }

        public void HandleRpmChange(Rpm rpm)
        {
            var suggestedAction = Program.GetSuggestedActionFor(rpm);

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
