using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Gearshift
{
    public interface IGearbox
    {
        void Upshift();
        void Downshift();
    }
}
