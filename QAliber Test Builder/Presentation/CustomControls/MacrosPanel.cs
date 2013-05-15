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
using QAliber.TestModel;
using QAliber.TestModel.Attributes;
using System.IO;
using System.Diagnostics;

namespace QAliber.Builder.Presentation
{
	public partial class MacrosPanel : UserControl
	{
		public MacrosPanel()
		{
			InitializeComponent();
		}

		private void FillTree()
		{
			typesTreeView.Nodes.Clear();
			string path = TestController.LocalAssemblyPath + @"\\Macros";
			if (!Directory.Exists(path))
			{
				try
				{
					Directory.CreateDirectory(path);
				}
				catch (Exception)
				{
				}
			}

			CopyMacros();

			
			watch.Reset();
			watch.Start();
			try
			{
				FillTreeRec(path, typesTreeView.Nodes, null);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("Problem retrieving macros, some directories were inaccessible", "Unauthorized Access");
			}
			catch (TimeoutException)
			{
				MessageBox.Show("Retrieving macros operation took too long, please don't set your macro location as root folders", "Operation timed out");
			}
			finally
			{
				watch.Stop();
			}

		}

		private void FillTreeRec(string path, TreeNodeCollection nodes, TreeNode parentNode)
		{
			bool foundDir = false;
			bool foundFile = false;
			if (watch.ElapsedMilliseconds > 10000)
				throw new TimeoutException();
			foreach (string dir in Directory.GetDirectories(path))
			{
				foundDir = true;
				TreeNode node = new TreeNode();
				node.SelectedImageKey = node.ImageKey = "Folder";
				node.Name = node.Text = dir.Remove(0,dir.LastIndexOf('\\') + 1);
				
				nodes.Add(node);
				FillTreeRec(dir, node.Nodes, node);

			}
			foreach (string file in Directory.GetFiles(path, "*.macro"))
			{
				foundFile = true;
				TreeNode node = new TreeNode();
				node.SelectedImageKey = node.ImageKey = "Macro";
				node.Name = node.Text = Path.GetFileNameWithoutExtension(file);
				node.Tag = file;

				nodes.Add(node);
			}
			if (!foundFile && !foundDir && parentNode != null)
				parentNode.Remove();
			

		}

		private TreeNode FindByName(TreeNodeCollection nodes, string name)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Text == name)
					return node;
			}
			return null;
		}

		

		private TreeNode GetNextNode(TreeNode node)
		{
			if (node == null)
				return null;
			if (node.Nodes.Count > 0)
				return node.Nodes[0];
			if (node.NextNode != null)
				return node.NextNode;
			while (node.Parent != null && node.Parent.NextNode == null)
				node = node.Parent;
			if (node.Parent != null)
				return node.Parent.NextNode;
			return null;
		}

		private void CopyMacros()
		{
			ProcessStartInfo psi = new ProcessStartInfo("xcopy",
			  string.Format("\"{0}\\*.macro\" \"{1}\\Macros\\\" /c /i /s /y", TestController.RemoteAssemblyDirectory, TestController.LocalAssemblyPath));
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			Process.Start(psi).WaitForExit(10000);
		}

		

		#region Events

		private void AfterSearchSubmitted(object sender, EventArgs e)
		{
			if (typesTreeView.Nodes.Count == 0)
				return;
			TreeNode node = GetNextNode(typesTreeView.SelectedNode);
			if (node == null)
				node = typesTreeView.Nodes[0];
			do
			{
				if (node.Nodes.Count == 0 &&
					node.Text.ToLower().Contains(searchToolStripTextBox.Text.ToLower()))
				{
					node.EnsureVisible();
					typesTreeView.SelectedNode = node;
					return;
				}
				node = GetNextNode(node);
			} while (node != null);
			typesTreeView.SelectedNode = typesTreeView.Nodes[0];
		}

		private void AfterDragStarted(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void typesTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TreeNode targetNode = typesTreeView.GetNodeAt(new Point(e.X, e.Y));
				if (targetNode != null)
				{
					typesTreeView.SelectedNode = targetNode;
				}
			}
		}

		private void addDllsToolStripButton_Click(object sender, EventArgs e)
		{
			QAliber.Builder.Presentation.SubForms.MacroRecorderForm form = new QAliber.Builder.Presentation.SubForms.MacroRecorderForm();
			form.ShowDialog();
		}

		private void refreshToolStripButton_Click(object sender, EventArgs e)
		{
			FillTree();
		}

		private void searchToolStripTextBox_Click(object sender, EventArgs e)
		{
			searchToolStripTextBox.SelectAll();
		}

		private void MacrosPanel_Load(object sender, EventArgs e)
		{
			FillTree();
		}
		#endregion

		private Stopwatch watch = new Stopwatch();

		

		

		
	}
}
