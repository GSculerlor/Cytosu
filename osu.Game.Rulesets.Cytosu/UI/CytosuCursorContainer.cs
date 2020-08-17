// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Cytosu.Configurations;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Cytosu.UI
{
    public class CytosuCursorContainer : GameplayCursorContainer
    {
        public override bool HandlePositionalInput => true;

        private readonly Bindable<bool> showCursor = new Bindable<bool>(true);

        [BackgroundDependencyLoader(true)]
        private void load(CytosuRulesetConfigManager rulesetConfig)
        {
            rulesetConfig?.BindWith(CytosuRulesetSetting.ShowCursor, showCursor);
        }

        protected override Drawable CreateCursor() => new CytosuCursor();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            showCursor.BindValueChanged(v =>
            {
                if (v.NewValue)
                {
                    ActiveCursor.Show();
                }
                else ActiveCursor.Hide();
            }, true);
        }
    }
}
