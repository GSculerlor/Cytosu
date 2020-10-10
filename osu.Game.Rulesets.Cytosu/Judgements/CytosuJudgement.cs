// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Cytosu.Judgements
{
    public class CytosuJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.Great;

        protected new int ToNumericResult(HitResult result)
        {
            return result switch
            {
                HitResult.Meh => 50,
                HitResult.Good => 100,
                HitResult.Great => 300,
                _ => 0
            };
        }
    }
}
