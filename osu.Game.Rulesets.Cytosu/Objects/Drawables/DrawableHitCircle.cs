// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Cytosu.Objects.Drawables.Piece;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables
{
    public class DrawableHitCircle : DrawableCytosuHitObject
    {
        public readonly HitReceptor HitArea;
        public readonly Drawable RingPiece;
        public readonly Drawable BodyPiece;

        public override double LifetimeStart
        {
            get => base.LifetimeStart;
            set
            {
                base.LifetimeStart = value;
                BodyPiece.LifetimeStart = value;
            }
        }

        public override double LifetimeEnd
        {
            get => base.LifetimeEnd;
            set
            {
                base.LifetimeEnd = value;
                BodyPiece.LifetimeEnd = value;
            }
        }

        private readonly IBindable<Vector2> positionBindable = new Bindable<Vector2>();
        private readonly IBindable<float> scaleBindable = new BindableFloat();
        private readonly Container scaleContainer;

        public DrawableHitCircle(CytosuHitObject hitObject)
            : base(hitObject)
        {
            Origin = Anchor.Centre;
            Position = HitObject.Position;
            AlwaysPresent = true;

            InternalChildren = new Drawable[]
            {
                scaleContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Children = new[]
                    {
                        HitArea = new HitReceptor
                        {
                            Hit = () =>
                            {
                                if (AllJudged)
                                    return false;

                                UpdateResult(true);
                                return true;
                            },
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Margin = new MarginPadding(Piece.RingPiece.RING_THICKNESS),
                            Child = BodyPiece = new BodyPiece
                            {
                                Direction = HitObject.Direction,
                                Alpha = 0,
                                Scale = Vector2.Zero
                            },
                        },
                        RingPiece = new RingPiece()
                    }
                }
            };

            Size = HitArea.DrawSize;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            positionBindable.BindValueChanged(_ => Position = HitObject.Position);
            scaleBindable.BindValueChanged(scale => scaleContainer.Scale = new Vector2(scale.NewValue), true);

            positionBindable.BindTo(HitObject.PositionBindable);
            scaleBindable.BindTo(HitObject.ScaleBindable);
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            Debug.Assert(HitObject.HitWindows != null);

            if (!userTriggered)
            {
                if (ShouldPerfectlyJudged && timeOffset > 0)
                    ApplyResult(r => r.Type = HitResult.Great);

                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(r => r.Type = HitResult.Miss);

                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);
            if (result == HitResult.None || result == HitResult.Miss && Time.Current < HitObject.StartTime)
                return;

            ApplyResult(r => r.Type = result);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            using (BeginAbsoluteSequence(HitObject.StartTime - HitObject.TimePreempt))
            {
                this.ScaleTo(0.5f).Then().ScaleTo(1, HitObject.TimePreempt, Easing.OutSine);
                RingPiece.FadeInFromZero(HitObject.TimePreempt / 2);

                BodyPiece.FadeIn(Math.Min(HitObject.TimeFadeIn * 2, HitObject.TimePreempt));
                BodyPiece.ScaleTo(1f, HitObject.TimePreempt);
                BodyPiece.Expire(true);
            }
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            base.UpdateHitStateTransforms(state);

            Debug.Assert(HitObject.HitWindows != null);

            switch (state)
            {
                case ArmedState.Idle:
                    this.Delay(HitObject.TimePreempt).FadeOut(500);

                    Expire(true);

                    HitArea.HitAction = null;
                    break;

                case ArmedState.Miss:
                    this.FadeOut(100);
                    break;

                case ArmedState.Hit:
                    RingPiece
                        .ScaleTo(1.5f, 200, Easing.InCubic)
                        .FadeOut(200);
                    BodyPiece.FadeOut(200);

                    this.Delay(800).FadeOut();
                    break;
            }
        }

        //TODO: Add Cytosu judgement if available

        public class HitReceptor : CompositeDrawable, IKeyBindingHandler<CytosuAction>
        {
            public override bool HandlePositionalInput => true;

            public Func<bool> Hit;

            public CytosuAction? HitAction;

            public HitReceptor()
            {
                Size = new Vector2(CytosuHitObject.CIRCLE_RADIUS * 2);

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                CornerRadius = CytosuHitObject.CIRCLE_RADIUS;
                CornerExponent = 2;
            }

            public bool OnPressed(CytosuAction action)
            {
                switch (action)
                {
                    case CytosuAction.Action1:
                    case CytosuAction.Action2:
                        if (IsHovered && (Hit?.Invoke() ?? false))
                        {
                            HitAction = action;
                            return true;
                        }

                        break;
                }

                return false;
            }

            public void OnReleased(CytosuAction action)
            {
            }
        }
    }
}
