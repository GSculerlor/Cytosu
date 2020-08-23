// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays.Legacy;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Types;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Replays
{
    public class CytosuReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public Vector2 Position;
        public List<CytosuAction> Actions = new List<CytosuAction>();

        public CytosuReplayFrame()
        {
        }

        public CytosuReplayFrame(double time, Vector2 position, params CytosuAction[] actions)
            : base(time)
        {
            Position = position;
            Actions.AddRange(actions);
        }

        public void FromLegacy(LegacyReplayFrame currentFrame, IBeatmap beatmap, ReplayFrame lastFrame = null)
        {
            Position = currentFrame.Position;
            if (currentFrame.MouseLeft) Actions.Add(CytosuAction.Action1);
            if (currentFrame.MouseRight) Actions.Add(CytosuAction.Action2);
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            ReplayButtonState state = ReplayButtonState.None;

            if (Actions.Contains(CytosuAction.Action1))
                state |= ReplayButtonState.Left1;
            if (Actions.Contains(CytosuAction.Action2))
                state |= ReplayButtonState.Right1;

            return new LegacyReplayFrame(Time, Position.X, Position.Y, state);
        }
    }
}
