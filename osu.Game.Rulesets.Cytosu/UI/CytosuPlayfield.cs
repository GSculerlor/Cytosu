// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Cytosu.UI.HUD;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cytosu.UI
{
    [Cached]
    public class CytosuPlayfield : Playfield
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new Container
            {
                Padding = new MarginPadding { Vertical = 50 },
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Scanner
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    HitObjectContainer,
                }
            });
        }
    }
}
