// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Cytosu.UI.HUD
{
    public class PlayfieldBorder : CompositeDrawable
    {
        private readonly IBindable<WorkingBeatmap> beatmap = new Bindable<WorkingBeatmap>();

        [BackgroundDependencyLoader]
        private void load(IBindable<WorkingBeatmap> working)
        {
            beatmap.BindTo(working);

            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Name = "Borders Container",
                    RelativeSizeAxes = Axes.Both,
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
    }
}
