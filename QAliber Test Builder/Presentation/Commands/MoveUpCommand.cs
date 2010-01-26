using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;

namespace QAliber.Builder.Presentation.Commands
{
	public class MoveUpCommand : ICommand
	{
		public MoveUpCommand(QAliberTreeNode sourceNode)
		{
			this.sourceNode = sourceNode;
			treeView = sourceNode.TreeView as QAliberTreeView;
		}

		#region ICommand Members

		public void Do()
		{
			if (sourceNode != null)
			{
				int index = sourceNode.Index;
				QAliberTreeNode parentNode = sourceNode.Parent as QAliberTreeNode;
				if (parentNode != null && index > 0)
				{
					parentNode.RemoveChild(sourceNode);
					parentNode.InsertChild(index - 1, sourceNode);
				}
				if (treeView != null)
				{
					treeView.cancelMultiSelection = true;
					treeView.SelectedNode = sourceNode;
					treeView.cancelMultiSelection = false;
				}
				storedNode = new CommandInfo(sourceNode);
			}
		}

		public void Undo()
		{
			if (storedNode.Indices.Count > 0)
			{
				QAliberTreeNode nodeToFind = treeView.Nodes[storedNode.Indices[0]] as QAliberTreeNode;
				for (int i = 1; i < storedNode.Indices.Count; i++)
				{
					nodeToFind = nodeToFind.Nodes[storedNode.Indices[i]] as QAliberTreeNode;
				}
				if (nodeToFind != null)
				{
					int index = nodeToFind.Index;
					QAliberTreeNode parentNode = nodeToFind.Parent as QAliberTreeNode;
					if (parentNode != null && parentNode.Nodes.Count > index + 1)
					{
						parentNode.RemoveChild(nodeToFind);
						parentNode.InsertChild(index + 1, nodeToFind);
					}

				}
				storedNode = new CommandInfo(nodeToFind);
			}
		}

		public void Redo()
		{
			if (storedNode.Indices.Count > 0)
			{
				QAliberTreeNode nodeToFind = treeView.Nodes[storedNode.Indices[0]] as QAliberTreeNode;
				for (int i = 1; i < storedNode.Indices.Count; i++)
				{
					nodeToFind = nodeToFind.Nodes[storedNode.Indices[i]] as QAliberTreeNode;
				}
				if (nodeToFind != null)
				{
					int index = nodeToFind.Index;
					QAliberTreeNode parentNode = nodeToFind.Parent as QAliberTreeNode;
					if (parentNode != null && index > 0)
					{
						parentNode.RemoveChild(nodeToFind);
						parentNode.InsertChild(index - 1, nodeToFind);
					}

				}
				storedNode = new CommandInfo(nodeToFind);
			}
		}

		#endregion

		private QAliberTreeView treeView;
		private CommandInfo storedNode;
		private QAliberTreeNode sourceNode;
	}
}
