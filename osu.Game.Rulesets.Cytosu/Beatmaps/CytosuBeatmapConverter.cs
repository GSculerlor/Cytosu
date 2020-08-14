// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Cytosu.Utils;
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
            var progression = CytosuUtils.GetYProgressionFromBeatmap(beatmap, original.StartTime);

            float x = ((IHasXPosition)original).X;
            float y = progression.yPosition * 384;

            switch (original)
            {
                case IHasDuration endTimeData:
                    return new Hold
                    {
                        Samples = original.Samples,
                        StartTime = original.StartTime,
                        EndTime = endTimeData.EndTime,
                        Position = new Vector2(x, y),
                        Direction = progression.direction
                    }.Yield();

                default:
                    return new HitCircle
                    {
                        Samples = original.Samples,
                        StartTime = original.StartTime,
                        Position = new Vector2(x, y),
                        Direction = progression.direction
                    }.Yield();
            }
        }
    }
}
