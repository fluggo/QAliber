using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;
using QAliber.TestModel.Attributes;
using QAliber.RemotingModel;

namespace QAliber.Builder.Presentation
{
	public partial class TestCasesPanel : UserControl
	{
		public TestCasesPanel()
		{
			InitializeComponent();
		}

		private void FillTree()
		{
			typesTreeView.Nodes.Clear();
			foreach (Type type in TestController.Default.SupportedTypes)
			{
				string path = "Misc";
				TestCase testcase = Activator.CreateInstance(type) as TestCase;
				if (testcase != null)
				{
					VisualPathAttribute[] attrs = type.GetCustomAttributes(typeof(VisualPathAttribute), true) as VisualPathAttribute[];
					if (attrs != null && attrs.Length > 0)
					{
						path = attrs[0].Path;
					}
					string[] folders = path.Split('\\');
					TreeNodeCollection nodes = typesTreeView.Nodes;
					foreach (string folder in folders)
					{
						TreeNode node = FindByName(nodes, folder);
						if (node == null)
						{
							node = new TreeNode(folder);
							nodes.Add(node);
						}
						nodes = node.Nodes;
					}
					TreeNode leafNode = new TreeNode(testcase.Name);
					leafNode.Tag = testcase;
					leafNode.ContextMenuStrip = leafContextMenuStrip;
					if (!typesImageList.Images.ContainsKey(testcase.Name))
					{
						if (testcase.Icon != null)
							typesImageList.Images.Add(testcase.Name, testcase.Icon);
					}
					if (typesImageList.Images.ContainsKey(testcase.Name))
						leafNode.ImageKey = leafNode.SelectedImageKey = testcase.Name;
					else
						leafNode.ImageKey = leafNode.SelectedImageKey = "GeneralTestCase";
					nodes.Add(leafNode);
					
				}
			}
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
			//MessageBox.Show("No next match was found", "Test case was not found");
			typesTreeView.SelectedNode = typesTreeView.Nodes[0];
		}

		private void AfterDragStarted(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void addDllsToolStripButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = openFileDialog.ShowDialog();
			if (dr == DialogResult.OK)
			{
				TestController.Default.UserFiles = openFileDialog.FileNames;
				TestController.Default.RetrieveSupportedTypes();
				FillTree();

			}
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (typesTreeView.SelectedNode != null)
			{
				TestCase testcase = typesTreeView.SelectedNode.Tag as TestCase;
				if (testcase != null)
				{
					//TODO : Find a better way to show help (server side ?)
					try
					{
						string xmlFile = "Help/" + testcase.GetType().Assembly.FullName.Split(',')[0] + ".xml";
						string baseFileDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
						string fullPath = System.IO.Path.Combine(baseFileDir, xmlFile);
						string typeName = testcase.GetType().FullName;
						Program.AddStylesheetDeclaration(fullPath);
						System.Diagnostics.Process.Start("file://" + fullPath + "#" + typeName);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Could not show help.\n" + ex.Message, "Error Loading Help", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

			}
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

		private void refreshToolStripButton_Click(object sender, EventArgs e)
		{
			new AssembliesRetriever(false).CopyRemoteToLocal();
			TestController.Default.RetrieveSupportedTypes();
			FillTree();
		}

		private void searchToolStripTextBox_Click(object sender, EventArgs e)
		{
			searchToolStripTextBox.SelectAll();
		}

		private void TestCasesPanel_Load(object sender, EventArgs e)
		{
			FillTree();
		}
		#endregion

		

		

	   

		

		


		
	}
}
