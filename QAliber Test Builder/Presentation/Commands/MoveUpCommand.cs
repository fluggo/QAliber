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
