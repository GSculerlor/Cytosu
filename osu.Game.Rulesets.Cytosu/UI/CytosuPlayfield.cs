// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Cytosu.UI.HUD;
using osu.Game.Rulesets.UI;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI
{
    [Cached]
    public class CytosuPlayfield : Playfield
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new PlayfieldBorder
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        new ScanLine
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Width = 5000f,
                            LineColour = Color4.White,
                        },
                        HitObjectContainer
                    }
                },
            });
        }
    }
}
