/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Darwen.Windows.Forms.Controls.Docking;

namespace QAliber.Builder.Presentation
{
	public partial class DockManager : Darwen.Windows.Forms.Controls.Docking.DockingManagerControl
	{
		public DockManager()
		{
			InitializeComponent();

			IDockingPanel rightPanel = Panels[DockingType.Right].InsertPanel(0);
			IDockingPanel bottomPanel = Panels[DockingType.Bottom].InsertPanel(0);
			TestCasesPanel tcPanel = new TestCasesPanel();
			MacrosPanel macroPanel = new MacrosPanel();
			VariablesPanel varsPanel = new VariablesPanel(tabbedScenarioControl);
			rightPanel.DockedControls.Add("Macros Repository", macroPanel);
			rightPanel.DockedControls.Add("Test Cases Repository", tcPanel);
			
			
			rightPanel.Tabbed = true;
			rightPanel.Dimension = 240;
			bottomPanel.DockedControls.Add("Variables and Lists", varsPanel);

			

		}
	}
}
