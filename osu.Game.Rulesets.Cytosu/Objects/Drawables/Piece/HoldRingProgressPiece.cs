// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables.Piece
{
    public class HoldRingProgressPiece : CircularContainer
    {
        public CircularProgress AutoProgress { get; private set; }
        public CircularProgress Progress { get; private set; }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1f);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.White,
            };

            AddRangeInternal(new Drawable[]
            {
                AutoProgress = new CircularProgress
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    InnerRadius = 2f,
                    Size = new Vector2(35),
                    Current = { Value = 0 },
                    Colour = Color4.Black,
                    Alpha = 0.45f
                },
                Progress = new CircularProgress
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    InnerRadius = 2f,
                    Size = new Vector2(35),
                    Current = { Value = 0 },
                    Colour = colour.YellowDark
                }
            });
        }
    }
}
