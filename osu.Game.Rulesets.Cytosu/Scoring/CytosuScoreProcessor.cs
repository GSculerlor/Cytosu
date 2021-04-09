// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Cytosu.Scoring
{
    public class CytosuScoreProcessor : ScoreProcessor
    {
        protected override double DefaultComboPortion => 0.5;
        protected override double DefaultAccuracyPortion => 0.5;
    }
}
