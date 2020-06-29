// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Timing;
using osu.Game.Overlays;
using osu.Game.Rulesets.Cytosu.UI.HUD;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Cytosu.Tests.Components
{
    public class TestSceneScanLine : OsuTestScene
    {
        private readonly NowPlayingOverlay np;

        [Cached]
        private MusicController musicController = new MusicController();

        public TestSceneScanLine()
        {
            Clock = new FramedClock();
            Clock.ProcessFrame();

            AddRange(new Drawable[]
            {
                musicController,
                new Scanner
                {
                    RelativeSizeAxes = Axes.Both
                },
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
