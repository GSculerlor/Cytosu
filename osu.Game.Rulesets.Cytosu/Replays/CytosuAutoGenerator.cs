// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Cytosu.Replays
{
    public class CytosuAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<CytosuHitObject> Beatmap => (Beatmap<CytosuHitObject>)base.Beatmap;

        public CytosuAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            Frames.Add(new CytosuReplayFrame());

            foreach (CytosuHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new CytosuReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                    // todo: add required inputs and extra frames.
                });
            }

            return Replay;
        }
    }
}
