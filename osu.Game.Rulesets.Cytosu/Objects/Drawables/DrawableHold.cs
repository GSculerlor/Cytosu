// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Cytosu.Objects.Drawables.Piece;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Cytosu.Objects.Drawables
{
    public class DrawableHold : DrawableCytosuHitObject
    {
        public Drawable RingPiece;
        public Drawable BodyPiece;
        public HoldRingProgressPiece RingProgressPiece;

        private readonly IBindable<Vector2> positionBindable = new Bindable<Vector2>();

        public DrawableHold(CytosuHitObject hitObject)
            : base(hitObject) { }

        [BackgroundDependencyLoader]
        private void load()
        {
            Origin = Anchor.Centre;
            Position = HitObject.Position;

            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Scale = new Vector2(0.75f),
                    Children = new[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Margin = new MarginPadding(Piece.RingPiece.RING_THICKNESS),
                            Child = BodyPiece = new HoldBodyPiece
                            {
                                Alpha = 0,
                                Scale = Vector2.Zero
                            },
                        },
                        RingProgressPiece = new HoldRingProgressPiece
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                        RingPiece = new RingPiece()
                    }
                }
            });

            Size = new Vector2(CytosuHitObject.CIRCLE_RADIUS * 2);

            positionBindable.BindValueChanged(_ => Position = HitObject.Position);
            positionBindable.BindTo(HitObject.PositionBindable);
        }

        private readonly Bindable<bool> isActivated = new BindableBool();
        private double holdDuration;

        private CytosuInputManager InputManager => GetContainingInputManager() as CytosuInputManager;

        protected override void Update()
        {
            base.Update();

            isActivated.Value = Time.Current >= HitObject.StartTime
                                && Time.Current <= ((IHasDuration)HitObject)?.EndTime
                                && (InputManager.PressedActions.Any() && IsHovered || ShouldPerfectlyJudged);

            if (Result.HasResult) return;

            if (Time.Current >= HitObject.StartTime && Time.Current <= ((IHasDuration)HitObject)?.EndTime)
            {
                if (isActivated.Value)
                {
                    double progression = holdDuration / ((IHasDuration)HitObject).Duration;
                    holdDuration += Time.Elapsed;

                    RingProgressPiece.ScaleTo(2, HitObject.TimePreempt, Easing.OutQuint);
                    RingProgressPiece.Progress.Current.Value = progression;
                }
                else
                {
                    RingProgressPiece.ScaleTo(1, HitObject.TimePreempt, Easing.OutQuint);
                }
            }
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            double progression = holdDuration / ((IHasDuration)HitObject).Duration;

            if (Time.Current < HitObject.StartTime) return;

            if (userTriggered || Time.Current < ((IHasDuration)HitObject)?.EndTime)
                return;

            ApplyResult(result =>
            {
                if (progression >= .9)
                    result.Type = HitResult.Great;
                else if (progression >= .75)
                    result.Type = HitResult.Good;
                else if (progression >= .5)
                    result.Type = HitResult.Meh;
                else if (Time.Current >= ((IHasDuration)HitObject)?.EndTime)
                    result.Type = HitResult.Miss;
            });
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

                RingProgressPiece.AutoProgress.FillTo(1, Math.Min(HitObject.TimeFadeIn * 2, HitObject.TimePreempt));
            }
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            base.UpdateHitStateTransforms(state);

            Debug.Assert(HitObject.HitWindows != null);

            switch (state)
            {
                case ArmedState.Miss:
                    this.FadeOut(100);
                    break;

                case ArmedState.Hit:
                    RingPiece
                        .ScaleTo(1.5f, 200, Easing.InCubic)
                        .FadeOut(200);
                    BodyPiece.FadeOut(200);
                    RingProgressPiece.FadeOut();
                    this.Delay(200).Expire();

                    break;
            }
        }
    }
}
