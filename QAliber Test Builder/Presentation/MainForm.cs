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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;
using QAliber.Builder.Presentation;
using Darwen.Windows.Forms.Controls.Docking;
using QAliber.RemotingModel;
using System.Diagnostics;

namespace QAliber.Builder.Presentation
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			
			InitializeComponent();
			dockingWindowListMenuItem1.Initialise(executionContainer.dockManager);
			playToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+F5";
			pauseToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+F6";
			stopToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+F7";
		}

		public void LoadFile(string file)
		{
			executionContainer.dockManager.tabbedScenarioControl.LoadScenario(file);
		}

		#region Events
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.newToolStripButton_Click(sender, e);
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.openToolStripButton_Click(sender, e);
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.saveToolStripButton_Click(sender, e);
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.SaveAsItem();
		}

		private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.saveAllToolStripButton_Click(sender, e);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.undoToolStripButton_Click(sender, e);
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.redoToolStripButton_Click(sender, e);
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.cutToolStripButton_Click(sender, e);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.copyToolStripButton_Click(sender, e);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.pasteToolStripButton_Click(sender, e);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.deleteToolStripButton_Click(sender, e);
		}

		private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.moveUpToolStripButton_Click(sender, e);
		}

		private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			executionContainer.dockManager.tabbedScenarioControl.moveDownToolStripButton_Click(sender, e);
		}

		private void playToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (executionContainer.debugPlayToolStripButton.Enabled)
				executionContainer.debugPlayToolStripButton_Click(sender, e);
		}

		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (executionContainer.pauseToolStripButton.Enabled)
				executionContainer.pauseToolStripButton_Click(sender, e);
		}

		private void stopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (executionContainer.stopToolStripButton.Enabled)
				executionContainer.stopToolStripButton_Click(sender, e);
		}

		private void playFromServiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (executionContainer.debugPlayToolStripButton.Enabled)
				executionContainer.playToolStripButton_Click(sender, e);
		}

		private void toggleBreakpointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null && sc.TestScenario != null)
			{
				sc.SetBPToolStripMenuItem_Click(null, EventArgs.Empty);
			}
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SubForms.BuilderOptionsForm form = new QAliber.Builder.Presentation.SubForms.BuilderOptionsForm();
			form.ShowDialog();
		}

		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool hasChanges = false;
			foreach (string item in executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.ItemTitles)
			{
				if (item.StartsWith("*"))
				{
					hasChanges = true;
					break;
				}

			}
			if (hasChanges)
			{
				DialogResult dr = MessageBox.Show("Some files were not saved.\r\nDo you want to save these files before closing ?", "Save All?", MessageBoxButtons.YesNoCancel);
				switch (dr)
				{
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
					case DialogResult.No:
						break;
					case DialogResult.Yes:
						executionContainer.dockManager.tabbedScenarioControl.saveAllToolStripButton_Click(sender, e);
						break;
					default:
						break;
				}
			}

		}

		private void selectBlueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null)
			{
				sc.CheckNodeByColor(Color.Blue, selectBlueToolStripMenuItem.Checked);
			}
		}

		private void selectGreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null)
			{
				sc.CheckNodeByColor(Color.Green, selectGreenToolStripMenuItem.Checked);
			}
		}

		private void selectRedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null)
			{
				sc.CheckNodeByColor(Color.Red, selectRedToolStripMenuItem.Checked);
			}
		}

		private void selectOrangeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null)
			{
				sc.CheckNodeByColor(Color.Orange, selectOrangeToolStripMenuItem.Checked);
			}
		}

		private void selectPurpleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null)
			{
				sc.CheckNodeByColor(Color.Purple, selectPurpleToolStripMenuItem.Checked);
			}
		}

		#endregion

		#region Help
		private void wikiHowToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://qaliber.net/Wiki/index.php?title=Main_Page");
		}

		private void reportABugToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://sourceforge.net/tracker/?group_id=301143&atid=1269942");
		}

		private void getStartedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://qaliber.net/Wiki/index.php?title=Quick_Start");
		}

		private void supportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://sourceforge.net/projects/qaliber/support");
		}

		private void requestATestCaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://sourceforge.net/projects/qaliber/forums/forum/1085020");
		}

		private void aboutQAliberToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://qaliber.net");
		}
		#endregion

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScenarioControl sc = executionContainer.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null)
			{
				SubForms.FindForm form = new QAliber.Builder.Presentation.SubForms.FindForm(sc);
				form.Show();
			}
			
		}


	}
}