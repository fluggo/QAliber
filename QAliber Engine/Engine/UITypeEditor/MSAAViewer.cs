using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ManagedWinapi.Accessibility;

namespace QAliber.Engine.UITypeEditor
{
	public partial class MSAAViewer : Form
	{
		public MSAAViewer(string startCodePath, SystemAccessibleObject rootObj)
		{
			InitializeComponent();
			this.startCodePath = startCodePath;
			this.rootObj = rootObj;
			rootObj.CodePath = startCodePath;
			FillTree();

		}

		private void FillTree()
		{
			TreeNode node = new TreeNode(rootObj.VisibleName);
			node.Tag = rootObj;
			treeView.Nodes.Add(node);
			int i = 0;
			foreach (SystemAccessibleObject child in rootObj.Children)
			{
				child.CodePath = rootObj.CodePath + ".Children[" + i + "]";
				TreeNode newNode = new TreeNode(child.VisibleName);
				newNode.Tag = child;
				node.Nodes.Add(newNode);
				i++;
			}

		}

		private void FillTreeRec(TreeNode node, int depth)
		{
			node.Nodes.Clear();
			TreeNode newNode = null;
			SystemAccessibleObject control = node.Tag as SystemAccessibleObject;
			if (depth > 0)
			{
				int i = 0;
				foreach (SystemAccessibleObject child in control.Children)
				{
					if (child != null)
					{
						child.CodePath = control.CodePath + ".Children[" + i + "]";
						newNode = new TreeNode(child.VisibleName);
						newNode.Tag = child;
						node.Nodes.Add(newNode);
						FillTreeRec(newNode, depth - 1);
					}
					i++;
				}
			}
		}

		#region Events
		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			SystemAccessibleObject control = e.Node.Tag as SystemAccessibleObject;
			if (control != null)
			{
				propertyGrid.SelectedObject = control;
			}
		}

		private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			FillTreeRec(e.Node, 2);
		}
		#endregion

		private string startCodePath;
		private SystemAccessibleObject rootObj;

		
	}
}