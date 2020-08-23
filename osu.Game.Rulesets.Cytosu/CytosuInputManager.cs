// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cytosu
{
    public class CytosuInputManager : RulesetInputManager<CytosuAction>
    {
        public IEnumerable<CytosuAction> PressedActions => KeyBindingContainer.PressedActions;

        public bool AllowUserPresses
        {
            set => ((CytosuKeyBindingContainer)KeyBindingContainer).AllowUserPresses = value;
        }

        protected override KeyBindingContainer<CytosuAction> CreateKeyBindingContainer(RulesetInfo ruleset, int variant, SimultaneousBindingMode unique)
            => new CytosuKeyBindingContainer(ruleset, variant, unique);

        public CytosuInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }

        public bool AllowUserCursorMovement { get; set; } = true;

        protected override bool Handle(UIEvent e)
        {
            if (e is MouseMoveEvent && !AllowUserCursorMovement) return false;

            return base.Handle(e);
        }

        private class CytosuKeyBindingContainer : RulesetKeyBindingContainer
        {
            public bool AllowUserPresses = true;

            public CytosuKeyBindingContainer(RulesetInfo ruleset, int variant, SimultaneousBindingMode unique)
                : base(ruleset, variant, unique)
            {
            }

            protected override bool Handle(UIEvent e)
            {
                if (!AllowUserPresses) return false;

                return base.Handle(e);
            }
        }
    }

    public enum CytosuAction
    {
        [Description("Action 1")]
        Action1,

        [Description("Action 2")]
        Action2,
    }
}
