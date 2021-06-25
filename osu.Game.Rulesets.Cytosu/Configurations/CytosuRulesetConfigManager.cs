// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Cytosu.Configurations
{
    public class CytosuRulesetConfigManager : RulesetConfigManager<CytosuRulesetSetting>
    {
        public CytosuRulesetConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();
            SetDefault(CytosuRulesetSetting.ShowCursor, true);
        }
    }

    public enum CytosuRulesetSetting
    {
        ShowCursor,
    }
}
