// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;

namespace osu.Game.Rulesets.Cytosu.Objects
{
    public class HitCircle : CytosuHitObject
    {
        //TODO: Change to Cytosu judgement if available
        public override Judgement CreateJudgement() => new Judgement();
    }
}
