using osu.Game.Rulesets.Cytosu.Objects;
using osu.Game.Rulesets.Cytosu.Objects.Drawables;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Play;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static osu.Game.Input.Handlers.ReplayInputHandler;

namespace osu.Game.Rulesets.Cytosu.Mods
{
    public class CytosuModRelax : ModRelax, IUpdatableByPlayfield, IApplicableToDrawableRuleset<CytosuHitObject>, IApplicableToPlayer
    {
        private CytosuInputManager inputManager;
        private ReplayState<CytosuAction> state;

        private bool hasReplay;
        private bool isDownState;

        public void ApplyToDrawableRuleset(DrawableRuleset<CytosuHitObject> drawableRuleset)
        {
            inputManager = (CytosuInputManager)drawableRuleset.KeyBindingInputManager;
        }

        public void ApplyToPlayer(Player player)
        {
            if (inputManager.ReplayInputHandler != null)
            {
                hasReplay = true;
                return;
            }

            inputManager.AllowUserPresses = false;
        }

        public void Update(Playfield playfield)
        {
            if (hasReplay)
                return;

            bool requiresHold = false;
            bool requiresHit = false;

            double time = playfield.Clock.CurrentTime;

            foreach (var h in playfield.HitObjectContainer.AliveObjects.OfType<DrawableCytosuHitObject>())
            {
                if (time < h.HitObject.StartTime)
                    break;

                if (h.IsHit || (h.HitObject is IHasDuration hasEnd && time > hasEnd.EndTime))
                    continue;

                switch (h)
                {
                    case DrawableHitCircle circle:
                        handleHitCircle(circle);
                        break;

                    case DrawableHold _:
                        requiresHold = true;
                        break;
                }
            }

            if (requiresHit)
            {
                changeState(false);
                changeState(true);
            }

            if (requiresHold)
                changeState(true);
            else if (isDownState)
                changeState(false);

            void handleHitCircle(DrawableHitCircle circle)
            {
                if (!circle.HitArea.IsHovered)
                    return;

                Debug.Assert(circle.HitObject.HitWindows != null);
                requiresHit |= circle.HitObject.HitWindows.CanBeHit(time - circle.HitObject.StartTime);
            }

            void changeState(bool down)
            {
                if (isDownState == down)
                    return;

                isDownState = down;

                state = new ReplayState<CytosuAction>
                {
                    PressedActions = new List<CytosuAction>()
                };

                if (down)
                    state.PressedActions.Add(CytosuAction.Action1);

                state?.Apply(inputManager.CurrentState, inputManager);
            }
        }
    }
}
