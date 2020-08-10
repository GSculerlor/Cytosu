// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Rulesets.Cytosu.UI.HUD;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Cytosu.Tests.Components
{
    public class TestSceneBorders : OsuTestScene
    {
        public TestSceneBorders()
        {
            Children = new Drawable[]
            {
                new Borders
                {
                    RelativePositionAxes = Axes.Y,
                    Y = 0.7f,
                },
            };
        }
    }
}
