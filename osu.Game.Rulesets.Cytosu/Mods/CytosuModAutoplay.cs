// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Cytosu.Objects.Drawables;
using osu.Game.Rulesets.Cytosu.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Scoring;
using osu.Game.Users;

namespace osu.Game.Rulesets.Cytosu.Mods
{
    public class CytosuModAutoplay : ModAutoplay, IApplicableToDrawableHitObject
    {
        public override Score CreateReplayScore(IBeatmap beatmap, IReadOnlyList<Mod> mods) => new Score
        {
            ScoreInfo = new ScoreInfo
            {
                User = new User { Username = "Nora" },
            },
            Replay = new CytosuAutoGenerator(beatmap).Generate(),
        };

        public void ApplyToDrawableHitObject(DrawableHitObject drawable)
        {
            if (drawable is not DrawableCytosuHitObject drawableCytosu)
                return;

            drawableCytosu.ShouldPerfectlyJudged = true;
        }
    }
}
