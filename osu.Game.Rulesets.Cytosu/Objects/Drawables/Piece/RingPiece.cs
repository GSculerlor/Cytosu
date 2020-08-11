// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables.Piece
{
    public class RingPiece : CompositeDrawable
    {
        public const float RING_THICKNESS = 16f;

        public RingPiece()
        {
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1f);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            AddInternal(new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                BorderThickness = RING_THICKNESS,
                BorderColour = Color4.White,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    AlwaysPresent = true
                }
            });
        }
    }
}
