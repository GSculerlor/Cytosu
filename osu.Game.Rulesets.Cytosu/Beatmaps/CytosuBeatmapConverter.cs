// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Cytosu.UI;
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

        protected override IEnumerable<CytosuHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            var progression = CytosuUtils.GetProgressionFromBeatmap(beatmap, original.StartTime);

            float x = ((IHasXPosition)original).X;
            float y = progression * 384;

            switch (original)
            {
                case IHasPathWithRepeats pathdata:
                    return new Hold
                    {
                        StartTime = original.StartTime,
                        Samples = original.Samples,
                        EndTime = pathdata.EndTime,
                        Position = new Vector2(x, y)
                    }.Yield();

                case IHasDuration endTimeData:
                    return new Hold
                    {
                        StartTime = original.StartTime,
                        Samples = original.Samples,
                        EndTime = endTimeData.EndTime,
                        Position = new Vector2(256, 192)
                    }.Yield();

                default:
                    return new HitCircle
                    {
                        Samples = original.Samples,
                        StartTime = original.StartTime,
                        Position = new Vector2(x, y)
                    }.Yield();
            }
        }
    }
}
