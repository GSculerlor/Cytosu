// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Cytosu.Beatmaps;
using osu.Game.Rulesets.Cytosu.Mods;
using osu.Game.Rulesets.Cytosu.Replays;
using osu.Game.Rulesets.Cytosu.Scoring;
using osu.Game.Rulesets.Cytosu.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Replays.Types;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cytosu
{
    public class CytosuRuleset : Ruleset
    {
        public override string Description => "Cytosu";

        public override string PlayingVerb => "Scanning...";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableCytosuRuleset(this, beatmap, mods);

        public override ScoreProcessor CreateScoreProcessor() => new CytosuScoreProcessor();

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new CytosuBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) =>
            new CytosuDifficultyCalculator(this, beatmap);

        public override IConvertibleReplayFrame CreateConvertibleReplayFrame() => new CytosuReplayFrame();

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new[] { new CytosuModNoFail() };

                case ModType.Automation:
                    return new[] { new CytosuModAutoplay() };

                default:
                    return new Mod[] { null };
            }
        }

        public override string ShortName => "cytosu";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Z, CytosuAction.Button1),
            new KeyBinding(InputKey.X, CytosuAction.Button2),
        };

        public override Drawable CreateIcon() => new CytosuIcon();
    }
}
