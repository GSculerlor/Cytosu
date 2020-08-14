// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Cytosu.Objects.Drawables;
using osu.Game.Rulesets.Cytosu.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cytosu.UI
{
    [Cached]
    public class DrawableCytosuRuleset : DrawableRuleset<CytosuHitObject>
    {
        public DrawableCytosuRuleset(CytosuRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        public override DrawableHitObject<CytosuHitObject> CreateDrawableRepresentation(CytosuHitObject h)
        {
            switch (h)
            {
                case HitCircle basic:
                    return new DrawableHitCircle(basic);

                case Hold hold:
                    return new DrawableHold(hold);
            }

            return null;
        }

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new CytosuPlayfieldAdjustmentContainer();

        protected override Playfield CreatePlayfield() => new CytosuPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new CytosuFramedReplayInputHandler(replay);

        protected override ReplayRecorder CreateReplayRecorder(Replay replay) => new CytosuReplayRecorder(replay);

        protected override PassThroughInputManager CreateInputManager() => new CytosuInputManager(Ruleset?.RulesetInfo);
    }
}
