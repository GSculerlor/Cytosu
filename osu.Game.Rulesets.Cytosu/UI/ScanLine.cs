// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Cytosu.Utils;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI
{
    public class ScanLine : BeatSyncedContainer
    {
        private readonly Box scanLine;

        public override bool HandlePositionalInput => false;

        public ScanLine()
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(scanLine = new Box
            {
                Name = "Scan line",
                RelativeSizeAxes = Axes.X,
                RelativePositionAxes = Axes.Y,
                Height = 2f,
                Colour = Color4.White
            });
        }

        private int beatIndex = 0;

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

            return CytosuUtils.GetYProgression(beatIndex, beatProgression);
        }
    }
}
