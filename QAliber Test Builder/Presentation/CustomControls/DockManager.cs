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
