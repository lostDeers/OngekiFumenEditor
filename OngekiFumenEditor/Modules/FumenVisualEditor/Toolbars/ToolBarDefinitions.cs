﻿using Gemini.Framework.ToolBars;
using OngekiFumenEditor.Modules.FumenVisualEditor.Commands.BrushModeSwitch;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenVisualEditor.Toolbars
{
    public static class ToolBarDefinitions
    {
        [Export]
        public static ToolBarDefinition EditorToolBar = new ToolBarDefinition(8, "Editor");

        [Export]
        public static ToolBarItemGroupDefinition EditorStatusToolBarGroup = new ToolBarItemGroupDefinition(EditorToolBar, 0);

        [Export]
        public static ToolBarItemDefinition BrushModeSwitchToolBarItem = new CommandToolBarItemDefinition<BrushModeSwitchCommandDefinition>(
            EditorStatusToolBarGroup, 0);
    }
}
