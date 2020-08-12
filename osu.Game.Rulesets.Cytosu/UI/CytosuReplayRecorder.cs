// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Replays;
using osu.Game.Rulesets.Cytosu.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.UI
{
    public class CytosuReplayRecorder : ReplayRecorder<CytosuAction>
    {
        public CytosuReplayRecorder(Replay target)
            : base(target)
        {
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<CytosuAction> actions, ReplayFrame previousFrame)
            => new CytosuReplayFrame(Time.Current, mousePosition, actions);
    }
}
