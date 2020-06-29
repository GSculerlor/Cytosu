// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI.HUD
{
    public class Border : CompositeDrawable
    {
        private readonly FillFlowContainer boxContainer;

        public Border()
        {
            Name = "Border";
            AutoSizeAxes = Axes.X;
            RelativeSizeAxes = Axes.Y;
            InternalChild = boxContainer = new FillFlowContainer
            {
                Name = "Border Box Container",
                Masking = true,
                AutoSizeAxes = Axes.Both
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            const int box_count = 100;

            for (int i = 0; i < box_count; i++)
            {
                boxContainer.Add(new Box
                {
                    Colour = Color4.Wheat,
                    Size = new Vector2(4f, 10f),
                    Margin = new MarginPadding { Right = 80 },
                });
            }
        }
    }

    public class Borders : CompositeDrawable
    {
        private readonly FillFlowContainer borderContainer;

        private readonly bool reverse;

        public Borders(bool reverse = false)
        {
            this.reverse = reverse;

            Name = "Borders";
            RelativeSizeAxes = Axes.X;
            Height = 10f;

            InternalChild = borderContainer = new FillFlowContainer
            {
                AutoSizeAxes = Axes.X,
                RelativeSizeAxes = Axes.Y,
                Direction = FillDirection.Horizontal,
                Anchor = reverse ? Anchor.TopRight : Anchor.TopLeft,
                Origin = reverse ? Anchor.TopRight : Anchor.TopLeft,
                Children = new Drawable[]
                {
                    new Border
                    {
                        Name = "Initial border"
                    },
                    new Border
                    {
                        Name = "Follow-up border"
                    },
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ScheduleAfterChildren(() =>
            {
                var containerWidth = borderContainer.Width / 2;

                borderContainer.MoveToX(reverse ? containerWidth : -containerWidth, containerWidth / 0.1f)
                               .Then()
                               .MoveToX(0f)
                               .Loop(1f);
            });
        }
    }
}
