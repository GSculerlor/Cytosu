// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Replays
{
    public class CytosuFramedReplayInputHandler : FramedReplayInputHandler<CytosuReplayFrame>
    {
        protected Vector2 Position
        {
            get
            {
                var frame = CurrentFrame;

                if (frame == null)
                    return Vector2.Zero;

                Debug.Assert(CurrentTime != null);

                return Interpolation.ValueAt(CurrentTime.Value, frame.Position, NextFrame.Position, frame.Time, NextFrame.Time);
            }
        }

        public CytosuFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        public override void CollectPendingInputs(List<IInput> inputs)
        {
            inputs.Add(new MousePositionAbsoluteInput
            {
                Position = GamefieldToScreenSpace(Position),
            });
            inputs.Add(new ReplayState<CytosuAction>
            {
                PressedActions = CurrentFrame?.Actions ?? new List<CytosuAction>(),
            });
        }

        protected override bool IsImportant(CytosuReplayFrame frame) => frame.Actions.Any();
    }
}
