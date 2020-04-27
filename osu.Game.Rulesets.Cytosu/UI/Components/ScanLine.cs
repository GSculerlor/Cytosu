// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.Containers;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI.Components
{
    public class ScanLine : BeatSyncedContainer
    {
        private const double early_activation = 60;

        private readonly Box scanner;

        public ScanLine()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            EarlyActivationMilliseconds = early_activation;

            AddInternal(scanner = new Box
            {
                Name = "Scan line",
                RelativeSizeAxes = Axes.X,
                Height = 100f,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Color4.White
            });
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
        {
            base.OnNewBeat(beatIndex, timingPoint, effectPoint, amplitudes);

            var beatLength = timingPoint.BeatLength;

            float amplitudeAdjust = Math.Min(1, 0.4f + amplitudes.Maximum);

            scanner.ClearTransforms();
            scanner
                .MoveToY(0)
                .Then()
                .MoveToY(Parent.DrawHeight);

            Logger.Log($"{beatLength} {amplitudeAdjust}");
        }
    }
}
