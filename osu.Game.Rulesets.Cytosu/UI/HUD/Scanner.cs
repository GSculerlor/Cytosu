// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI.HUD
{
    public class Scanner : CompositeDrawable
    {
        private readonly IBindable<WorkingBeatmap> beatmap = new Bindable<WorkingBeatmap>();

        private ScanLine scanLine;

        private const double default_beat_length = 120000.0 / 60.0;
        private const float scan_gap = 25;

        [BackgroundDependencyLoader]
        private void load(IBindable<WorkingBeatmap> working)
        {
            beatmap.BindTo(working);

            InternalChildren = new Drawable[]
            {
                scanLine = new ScanLine
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    LineColour = Color4.White
                },
                new Container
                {
                    Name = "Borders Container",
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Vertical = 20 },
                    Depth = float.MaxValue,
                    Children = new[]
                    {
                        new Borders
                        {
                            Origin = Anchor.TopLeft,
                            Anchor = Anchor.TopLeft
                        },
                        new Borders(true)
                        {
                            Origin = Anchor.BottomLeft,
                            Anchor = Anchor.BottomLeft
                        },
                    },
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ScheduleAfterChildren(() =>
            {
                scanLine.MoveToY(DrawHeight - scan_gap, default_beat_length)
                        .Then()
                        .MoveToY(scan_gap, default_beat_length)
                        .Loop();
            });
        }
    }

    public enum ScanLineDirection
    {
        Up,
        Down
    }
}
