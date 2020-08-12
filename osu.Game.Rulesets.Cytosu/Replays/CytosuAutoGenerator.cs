// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Cytosu.Beatmaps;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Replays
{
    public class CytosuAutoGenerator : AutoGenerator
    {
        protected Replay Replay;

        public new CytosuBeatmap Beatmap => (CytosuBeatmap)base.Beatmap;

        public CytosuAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        public override Replay Generate()
        {
            Replay = new Replay();
            List<CytosuAction> activeActions = new List<CytosuAction>();

            double lastCheckedTime = -10000;

            void press(double time, Vector2 position)
            {
                releaseBefore(time, position);

                activeActions.Add(CytosuAction.Button1);
                Replay.Frames.Add(new CytosuReplayFrame(time, position, activeActions.ToList()));
                lastCheckedTime = time;
            }

            void release(double time, Vector2 position)
            {
                activeActions.Clear();

                Replay.Frames.Add(new CytosuReplayFrame(time, position, activeActions.ToList()));
                lastCheckedTime = time;
            }

            void releaseBefore(double time, Vector2 position)
            {
                if (activeActions.Count > 0)
                {
                    activeActions.Clear();

                    if (time > lastCheckedTime + KEY_UP_DELAY)
                    {
                        release(lastCheckedTime + KEY_UP_DELAY, position);
                    }
                }
            }

            foreach (var hitObject in Beatmap.HitObjects.OrderBy(x => x.StartTime))
            {
                switch (hitObject)
                {
                    case HitCircle tap:
                        press(tap.StartTime, tap.Position);

                        break;
                }
            }

            releaseBefore(lastCheckedTime + 1000, Vector2.Zero);

            return Replay;
        }
    }
}
