// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Cytosu.Utils;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI.HUD
{
    public class ScanLine : BeatSyncedContainer
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

        //Make sure not fucked up the input (idk should I proxy the hit object?)
        public override bool HandlePositionalInput => false;

        public ScanLine()
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(scanLine = new Box
            {
                Name = "Scan line",
                RelativeSizeAxes = Axes.X,
                RelativePositionAxes = Axes.Y,
                Height = 2f
            });
        }

        private void updateScanLineColour(Color4 colour)
        {
            scanLine.Colour = colour;
        }

        private int beatIndex = -1;

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, ChannelAmplitudes amplitudes)
        {
            this.beatIndex = beatIndex;
        }

        protected override void Update()
        {
            base.Update();

            scanLine.Y = updateScanLinePosition();
        }

        private float updateScanLinePosition()
        {
            float beatProgression = (float)(TimeSinceLastBeat / (TimeSinceLastBeat + TimeUntilNextBeat));

            //Handle case where beat index below zero. Not sure about this approach
            return CytosuUtils.GetYProgression(beatIndex, beatProgression).yPosition;
        }
    }
}
