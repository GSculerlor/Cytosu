// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables
{
    public class DrawableCytosuHitObject : DrawableHitObject<CytosuHitObject>
    {
        public override bool HandlePositionalInput => true;

        public bool ShouldPerfectlyJudged { get; set; }

        public DrawableCytosuHitObject(CytosuHitObject hitObject)
            : base(hitObject) { }

        protected sealed override double InitialLifetimeOffset => HitObject.TimePreempt;

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            base.UpdateHitStateTransforms(state);

            switch (state)
            {
                case ArmedState.Idle:
                    LifetimeStart = HitObject.StartTime - HitObject.TimePreempt;
                    break;
            }
        }
    }
}
