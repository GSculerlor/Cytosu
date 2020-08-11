// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Rulesets.Cytosu.Objects
{
    public enum HitObjectDirection
    {
        [Description("Hit object activated when scanline move to top")]
        Up,

        [Description("Hit object activated when scanline move to bottom")]
        Down
    }
}
