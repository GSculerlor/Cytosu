// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI
{
    public class PlayfieldBorder : CompositeDrawable
    {
        public PlayfieldBorder()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new Line(Direction.Horizontal)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                },
                new Line(Direction.Horizontal)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                },
                new Line(Direction.Horizontal)
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                },
                new Line(Direction.Horizontal)
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                },
                new Line(Direction.Vertical)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                },
                new Line(Direction.Vertical)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                },
                new Line(Direction.Vertical)
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                },
                new Line(Direction.Vertical)
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                }
            };
        }

        private class Line : Box
        {
            private readonly Direction direction;

            public Line(Direction direction)
            {
                this.direction = direction;

                Colour = Color4.White;

                switch (direction)
                {
                    case Direction.Horizontal:
                        Size = new Vector2(25, 2);
                        break;

                    case Direction.Vertical:
                        Size = new Vector2(2, 25);
                        break;
                }
            }
        }
    }
}
