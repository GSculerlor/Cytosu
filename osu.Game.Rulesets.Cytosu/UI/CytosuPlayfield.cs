// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Cytosu.Objects.Drawables;
using osu.Game.Rulesets.Cytosu.UI.HUD;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Cytosu.UI
{
    [Cached]
    public class CytosuPlayfield : Playfield
    {
        private readonly JudgementContainer<DrawableCytosuJudgement> judgementLayer;

        public CytosuPlayfield()
        {
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new PlayfieldBorder
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        new ScanLine
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Width = 5000f,
                            LineColour = Color4.White,
                        },
                        judgementLayer = new JudgementContainer<DrawableCytosuJudgement>
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                        HitObjectContainer
                    }
                },
            };
        }

        public override void Add(DrawableHitObject h)
        {
            h.OnNewResult += onNewResult;
            h.OnLoadComplete += d =>
            {
            };

            base.Add(h);
        }

        private void onNewResult(DrawableHitObject judgedObject, JudgementResult result)
        {
            if (!judgedObject.DisplayResult || !DisplayJudgements.Value)
                return;

            DrawableCytosuJudgement explosion = new DrawableCytosuJudgement(result, (DrawableCytosuHitObject)judgedObject)
            {
                Position = judgedObject.Position,
                Scale = judgedObject.Scale * 0.75f,
            };

            judgementLayer.Add(explosion);
        }
    }
}
