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
        public List<CytosuAction> Actions = new List<CytosuAction>();
        public Vector2 Position;

        public CytosuReplayFrame()
        {
        }

        public CytosuReplayFrame(double time, Vector2 position, List<CytosuAction> actions)
            : base(time)
        {
            Position = position;
            Actions = actions;
        }

        public void FromLegacy(LegacyReplayFrame currentFrame, IBeatmap beatmap, ReplayFrame lastFrame = null)
        {
            Position = currentFrame.Position;

            if (currentFrame.MouseLeft) Actions.Add(CytosuAction.Button1);
            if (currentFrame.MouseRight) Actions.Add(CytosuAction.Button2);
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            ReplayButtonState state = ReplayButtonState.None;

            if (Actions.Contains(CytosuAction.Button1)) state |= ReplayButtonState.Left1;
            if (Actions.Contains(CytosuAction.Button2)) state |= ReplayButtonState.Right1;

            return new LegacyReplayFrame(Time, Position.X, Position.Y, state);
        }
    }
}
