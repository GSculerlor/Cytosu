// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables
{
    public class DrawableCytosuHitObject : DrawableHitObject<CytosuHitObject>
    {
        private readonly Container container;

        public DrawableCytosuHitObject(CytosuHitObject hitObject)
            : base(hitObject)
        {
            base.AddInternal(container = new Container
            {
                RelativeSizeAxes = Axes.Both
            });

            Alpha = 0;
        }

        protected override void AddInternal(Drawable drawable) => container.Add(drawable);
        protected override void ClearInternal(bool disposeChildren = true) => container.Clear(disposeChildren);
        protected override bool RemoveInternal(Drawable drawable) => container.Remove(drawable);

        protected sealed override double InitialLifetimeOffset => HitObject.TimePreempt;

        protected override void UpdateStateTransforms(ArmedState state)
        {
            base.UpdateStateTransforms(state);

            switch (state)
            {
                case ArmedState.Idle:
                    LifetimeStart = HitObject.StartTime - HitObject.TimePreempt;
                    break;
            }
        }

        //TODO: Change to Cytosu judgement result if available
        protected override JudgementResult CreateResult(Judgement judgement) => new JudgementResult(HitObject, judgement);
    }
}
