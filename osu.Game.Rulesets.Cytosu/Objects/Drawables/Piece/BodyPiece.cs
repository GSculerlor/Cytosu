// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables.Piece
{
    public class BodyPiece : CompositeDrawable
    {
        private readonly Box innerBox;

        public BodyPiece()
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            AddInternal(new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Child = innerBox = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    AlwaysPresent = true
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            innerBox.Colour = colour.BlueDarker;
        }
    }
}
