using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;

namespace QAliber.Builder.Presentation.Commands
{
	public class PasteCommand : ICommand
	{
		public PasteCommand(QAliberTreeNode sourceNode) : this (sourceNode, false)
		{
			
		}

		public PasteCommand(QAliberTreeNode sourceNode, bool insOnly)
		{
			FilterRecursiveInsert(sourceNode);
			insCommand = new InsertCommand(sourceNode, TreeClipboard.Default.Nodes);
			if (TreeClipboard.Default.Cutted && !insOnly)
			{
				remCommand = new RemoveCommand(TreeClipboard.Default.Nodes);
				TreeClipboard.Default.Cutted = false;
				TreeClipboard.Default.Nodes = null;
			}
			else
			{
				TreeClipboard.Default.StoreInClipboard(TreeClipboard.Default.Nodes, true);
			}
		}

		#region ICommand Members

		public void Do()
		{
			if (remCommand != null)
				remCommand.Do();
			insCommand.Do();
		}

		public void Undo()
		{
			insCommand.Undo();
			if (remCommand != null)
				remCommand.Undo();
		}

		public void Redo()
		{
			if (remCommand != null)
				remCommand.Redo();
			insCommand.Redo();
		}

		

		#endregion

		private void FilterRecursiveInsert(QAliberTreeNode sourceNode)
		{
			List<QAliberTreeNode> nodes = new List<QAliberTreeNode>();
			nodes.AddRange(TreeClipboard.Default.Nodes);
			for (int i = 0; i < nodes.Count; i++)
			{
				if (IsAncestor(nodes[i], sourceNode)
					|| nodes[i] == sourceNode
					|| sourceNode == nodes[i].Parent)
				{
					if (nodes[i].ForeColor == System.Drawing.Color.LightGray)
						nodes[i].ForeColor = nodes[i].Testcase.Color;
					nodes.RemoveAt(i);
					i--;
				}
			}
			TreeClipboard.Default.Nodes = nodes.ToArray();
		}

		private bool IsAncestor(TreeNode parent, TreeNode child)
		{
			while (child.Parent != null)
			{
				child = child.Parent;
				if (child.Equals(parent))
					return true;
			}
			return false;
		}

		private RemoveCommand remCommand;
		internal InsertCommand insCommand;
	}
}
