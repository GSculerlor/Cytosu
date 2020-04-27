// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Timing;
using osu.Game.Overlays;
using osu.Game.Rulesets.Cytosu.UI.Components;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Cytosu.Tests.Components
{
    public class TestSceneScanLine : OsuTestScene
    {
        private readonly ScanLine scanLine;
        private readonly NowPlayingOverlay np;

        [Cached]
        private MusicController musicController = new MusicController();

        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(ScanLine)
        };

        public TestSceneScanLine()
        {
            Clock = new FramedClock();
            Clock.ProcessFrame();

            AddRange(new Drawable[]
            {
                musicController,
                scanLine = new ScanLine(),
                np = new NowPlayingOverlay
                {
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            np.ToggleVisibility();
        }
    }
}
