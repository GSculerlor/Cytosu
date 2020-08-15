// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Cytosu.UI.HUD
{
    public class PlayfieldBorder : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load()
        {
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
