using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;
using QAliber.Logger.Controls;

namespace QAliber.Builder.Presentation
{
	public partial class TabbedScenarioControl : UserControl
	{
		public TabbedScenarioControl()
		{
			InitializeComponent();
			
			ScenarioControl.ScenarioChanged += new EventHandler<ScenarioChangedEventArgs>(scenarioControl_ScenarioChanged);
		}

		public string CurrentScenarioFile
		{
			get
			{
				ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
				if (currentScenarioControl != null && currentScenarioControl.TestScenario != null)
					return currentScenarioControl.TestScenario.Filename;
				return null;
			}
		}

		internal void LoadScenario(string filename)
		{
			try
			{
				if (System.IO.Path.GetExtension(filename).ToLower() == ".scn")
				{
					ScenarioControl newScenarioControl = new ScenarioControl();
					TestScenario loadedScenario = TestScenario.Load(filename);
					newScenarioControl.FillTree(loadedScenario);
					tabbedDocumentControl.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filename), newScenarioControl);
					tabbedDocumentControl.SelectedControl = tabbedDocumentControl.Items[tabbedDocumentControl.Items.Count - 1];
				}
				else if (System.IO.Path.GetExtension(filename).ToLower() == ".qlog")
				{
					LogViewerControl logViewerControl = new LogViewerControl();
					logViewerControl.Filename = filename;
					tabbedDocumentControl.Items.Add("Log - " + System.IO.Path.GetFileNameWithoutExtension(filename), logViewerControl);
					tabbedDocumentControl.SelectedControl = tabbedDocumentControl.Items[tabbedDocumentControl.Items.Count - 1];
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not load '" + filename + "'\n" + ex.Message, "Error While Loading");
			}
		}


		#region Events
		internal void newToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl newScenarioControl = new ScenarioControl();
			TestScenario newTestScenario = new TestScenario();
			newScenarioControl.FillTree(newTestScenario);
			tabbedDocumentControl.Items.Add("Unknown Scenario", newScenarioControl);

		}

		internal void openToolStripButton_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = openFileDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				foreach (string filename in openFileDialog.FileNames)
				{
					try
					{
						if (System.IO.Path.GetExtension(filename).ToLower() == ".scn")
						{
							ScenarioControl newScenarioControl = new ScenarioControl();
							TestScenario loadedScenario = TestScenario.Load(filename);
							newScenarioControl.FillTree(loadedScenario);
							tabbedDocumentControl.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filename), newScenarioControl);
							tabbedDocumentControl.SelectedControl = tabbedDocumentControl.Items[tabbedDocumentControl.Items.Count - 1];
						}
						else if (System.IO.Path.GetExtension(filename).ToLower() == ".qlog")
						{
							LogViewerControl logViewerControl = new LogViewerControl();
							logViewerControl.Filename = filename;
							tabbedDocumentControl.Items.Add("Log - " + System.IO.Path.GetFileNameWithoutExtension(filename), logViewerControl);
							tabbedDocumentControl.SelectedControl = tabbedDocumentControl.Items[tabbedDocumentControl.Items.Count - 1];
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Could not load '" + filename + "'\n" + ex.Message, "Error While Loading");
					}
				}
			}


		}

		internal void saveToolStripButton_Click(object sender, EventArgs e)
		{
			try
			{
				ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
				
				SaveItem(currentScenarioControl);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not save!\n" + ex.Message, "Error While Saving");
			}
		}

		internal void saveAllToolStripButton_Click(object sender, EventArgs e)
		{
			foreach (Control c in tabbedDocumentControl.Items)
			{
				ScenarioControl sc = c as ScenarioControl;
				if (sc != null)
				{
					try
					{
						SaveItem(sc);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Could not save '" + sc.TestScenario.Filename + "'\n" + ex.Message, "Error While Saving");
					}
				}
			}
		}

		internal void cutToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuCutClicked(this, EventArgs.Empty);
			}
		}

		internal void copyToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuCopyClicked(this, EventArgs.Empty);
			}
		}

		internal void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuPasteClicked(this, EventArgs.Empty);
			}
		}

		internal void deleteToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuDeleteClicked(this, EventArgs.Empty);
			}
		}

		internal void moveUpToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuMoveUpClicked(this, EventArgs.Empty);
			}
		}

		internal void moveDownToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuMoveDownClicked(this, EventArgs.Empty);
			}
		}

		internal void undoToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuUndoClicked(this, EventArgs.Empty);
			}
		}

		internal void redoToolStripButton_Click(object sender, EventArgs e)
		{
			ScenarioControl currentScenarioControl = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentScenarioControl != null)
			{
				currentScenarioControl.MenuRedoClicked(this, EventArgs.Empty);
			}
		}

		private void scenarioControl_ScenarioChanged(object sender, ScenarioChangedEventArgs e)
		{
			ScenarioControl s = sender as ScenarioControl;
			if (s != null)
			{
				string title = tabbedDocumentControl.ItemTitles[s];
				if (!string.IsNullOrEmpty(title) && !title.StartsWith("*"))
					tabbedDocumentControl.ItemTitles[s] = "*" + title;
			}
		}

		private void tabbedDocumentControl_BeforeDocumentRemovedByUser(Darwen.Windows.Forms.Controls.TabbedDocuments.TabbedDocumentControl documentControl, Control control)
		{
			ScenarioControl sc = control as ScenarioControl;
			if (sc != null)
			{
				if (tabbedDocumentControl.ItemTitles[control].StartsWith("*"))
				{
					DialogResult dr = MessageBox.Show("Do you want to save the file before closing ?", "Save ?", MessageBoxButtons.YesNo);
					switch (dr)
					{
						case DialogResult.No:
							break;
						case DialogResult.Yes:
							SaveItem(sc);
							break;
						default:
							break;
					}
				}
			}
		}
		#endregion


		internal void SaveItem(ScenarioControl control)
		{
			if (control != null && control.TestScenario != null)
			{
				//control.SetNodeIDs();
				if (control.TestScenario.Filename == "Unknown Scenario.scn")
				{
					DialogResult dr = saveFileDialog.ShowDialog();
					if (dr == DialogResult.OK)
					{
						if (!string.IsNullOrEmpty(saveFileDialog.FileName))
						{
							control.TestScenario.Filename = saveFileDialog.FileName;
							control.TestScenario.Save();
							tabbedDocumentControl.ItemTitles[control] = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
						}
					}
				}
				else
				{
					control.TestScenario.Save();
					tabbedDocumentControl.ItemTitles[control] = tabbedDocumentControl.ItemTitles[control].TrimStart('*');
				}
			}

		}

		internal void SaveAsItem()
		{
			ScenarioControl control = tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (control != null && control.TestScenario != null)
			{
				//control.SetNodeIDs();
				
				DialogResult dr = saveFileDialog.ShowDialog();
				if (dr == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(saveFileDialog.FileName))
					{
						control.TestScenario.Filename = saveFileDialog.FileName;
						control.TestScenario.Save();
						tabbedDocumentControl.ItemTitles[control] = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
					}
				}
				
			}

		}

		

		
		
	}
}
