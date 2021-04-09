// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cytosu.Objects;

namespace osu.Game.Rulesets.Cytosu.Utils
{
    public static class CytosuUtils
    {
        public static float GetProgressionFromBeatmap(IBeatmap beatmap, double time)
        {
            var timingPoint = beatmap.ControlPointInfo.TimingPointAt(time);
            var timeSinceTimingPoint = time - timingPoint.Time;

            float beatProgression = (float)(timeSinceTimingPoint % timingPoint.BeatLength / timingPoint.BeatLength);
            int beatIndex = (int)Math.Round((timeSinceTimingPoint - timeSinceTimingPoint % timingPoint.BeatLength) / timingPoint.BeatLength);

            return GetProgressionFromBeat(beatIndex, beatProgression);
        }

        public static float GetProgressionFromBeat(int beatIndex, float beatProgression)
        {
            if (beatIndex < 0)
            {
                beatIndex = Math.Abs(beatIndex) - 1;
                beatProgression = 1f - beatProgression;
            }

            bool direction = beatIndex / 4 % 2 == 1;
            float yProgression = (beatIndex % 4 + beatProgression) / 4;

            //If direction is going up then we subtract it
            return direction ? 1f - yProgression : yProgression;
        }
    }
}
