using GearboxDriver.Cabin.Towing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearboxDriver.Dashboard.Tests.Towing
{
    public class TrailerHookTests
    {
        [Test]
        public void AttachingTrailerResultsInEvent()
        {
            var trailerHook = new TrailerHook();

            var events = trailerHook.AttachTrailer();

            Assert.True(events.Any(x => x is TrailerHookBecameOccupied));
        }

        [Test]
        public void DetachingTrailerResultsInEvent()
        {
            var trailerHook = new TrailerHook();

            var events = trailerHook.DetachTrailer();

            Assert.True(events.Any(x => x is TrailerHookStoppedBeingOccupied));
        }
    }
}
