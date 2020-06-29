// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI.HUD
{
    public class ScanLine : CompositeDrawable
    {
        private readonly Box scanLine;

        private Color4 lineColour;

        public Color4 LineColour
        {
            set
            {
                lineColour = value;
                updateScanLineColour(lineColour);
            }
        }

        public ScanLine()
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(scanLine = new Box
            {
                Name = "Scan line",
                RelativeSizeAxes = Axes.X,
                Height = 2f
            });
        }

        private void updateScanLineColour(Color4 colour)
        {
            scanLine.Colour = colour;
        }
    }
}
