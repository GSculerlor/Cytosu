// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Cytosu.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Tests.Visual;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Tests.Objects
{
    [TestFixture]
    public class TestSceneHold : OsuTestScene
    {
        private readonly Container content;
        protected override Container<Drawable> Content => content;

        public TestSceneHold()
        {
            base.Content.Add(content = new CytosuInputManager(new RulesetInfo { ID = 0 }));

            AddStep("Add hold", addHold);
        }

        private void addHold()
        {
            var hold = new Hold
            {
                StartTime = Time.Current + 1000,
                EndTime = Time.Current + 6000,
                Position = new Vector2(0, 0),
            };

            hold.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty { CircleSize = 2 });

            Add(new TestDrawableHold(hold, false)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        protected class TestDrawableHold : DrawableHold
        {
            private readonly bool auto;

            public TestDrawableHold(Hold h, bool auto)
                : base(h)
            {
                this.auto = auto;
            }

            public void TriggerJudgement() => UpdateResult(true);

            protected override void CheckForResult(bool userTriggered, double timeOffset)
            {
                if (auto && !userTriggered && timeOffset > 0)
                {
                    ApplyResult(r => r.Type = HitResult.Great);
                }
                else
                    base.CheckForResult(userTriggered, timeOffset);
            }
        }
    }
}
