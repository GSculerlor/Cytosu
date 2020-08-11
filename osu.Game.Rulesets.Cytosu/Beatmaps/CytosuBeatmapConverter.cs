// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Beatmaps
{
    public class CytosuBeatmapConverter : BeatmapConverter<CytosuHitObject>
    {
        public CytosuBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasXPosition);

        protected override Beatmap<CytosuHitObject> CreateBeatmap() => new CytosuBeatmap();

        protected override IEnumerable<CytosuHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            var prop = getHitObjectYPos(beatmap, original.StartTime);

            //TODO: make it more readable (I mean wtf prop Item1 * 384)
            float x = ((IHasXPosition)original).X;
            float y = prop.Item1 * 384;

            yield return new HitCircle
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Position = new Vector2(x, y),
                Direction = prop.Item2
            };
        }

        private Tuple<float, HitObjectDirection> getHitObjectYPos(IBeatmap beatmap, double time)
        {
            var timingPoint = beatmap.ControlPointInfo.TimingPointAt(time);
            var timeSinceTimingPoint = time - timingPoint.Time;
            var beatProgression = timeSinceTimingPoint % timingPoint.BeatLength / timingPoint.BeatLength;
            double beatIndex = Math.Round((timeSinceTimingPoint - timeSinceTimingPoint % timingPoint.BeatLength) / timingPoint.BeatLength);

            if (beatIndex < 0)
            {
                beatIndex = Math.Abs(beatIndex) - 1;
                beatProgression = 1f - beatProgression;
            }

            bool direction = (int)beatIndex / 4 % 2 == 1;
            float yProgression = ((int)beatIndex % 4 + (float)beatProgression) / 4;

            var hitObjectDirection = direction ? HitObjectDirection.Up : HitObjectDirection.Down;

            return new Tuple<float, HitObjectDirection>(direction ? 1f - yProgression : yProgression, hitObjectDirection);
        }
    }
}
