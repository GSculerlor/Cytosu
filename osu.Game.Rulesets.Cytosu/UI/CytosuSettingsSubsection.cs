// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Cytosu.Configurations;

namespace osu.Game.Rulesets.Cytosu.UI
{
    public class CytosuSettingsSubsection : RulesetSettingsSubsection
    {
        private readonly Ruleset ruleset;

        protected override string Header => ruleset.ShortName;

        public CytosuSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
            this.ruleset = ruleset;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (CytosuRulesetConfigManager)Config;

            if (config == null)
                return;

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Show Cursor",
                    Current = config.GetBindable<bool>(CytosuRulesetSetting.ShowCursor)
                },
            };
        }
    }
}
