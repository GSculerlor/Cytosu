// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Cytosu.UI.HUD;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Cytosu.Tests.Components
{
    public class TestSceneBorders : OsuTestScene
    {
        [TestCase(true)]
        [TestCase(false)]
        public void TestBorder(bool reverse)
        {
            AddStep("Clear field", Content.Clear);
            AddStep("Add borders", () =>
            {
                Add(new Borders(reverse)
                {
                    RelativePositionAxes = Axes.Y,
                    Y = 0.5f
                });
            });
        }
    }
}
