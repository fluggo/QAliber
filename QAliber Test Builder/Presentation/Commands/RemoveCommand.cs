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
	public class RemoveCommand : ICommand
	{
		public RemoveCommand(QAliberTreeNode[] sourceNodes)
		{
			sourceNodes = TreeClipboard.Default.FilterDescandants(sourceNodes);
			if (sourceNodes != null && sourceNodes.Length > 0)
			{
				treeView = sourceNodes[0].TreeView as QAliberTreeView;
				storedNodes.Add(new CommandInfo(sourceNodes[0]));

				for (int i = 1; i < sourceNodes.Length; i++)
				{
					storedNodes.Add(new CommandInfo(sourceNodes[i]));
				}
										  
			}

		}

		#region ICommand Members

		public void Do()
		{
			CommandInfo.DecreaseIndices(storedNodes);
			foreach (CommandInfo info in storedNodes)
			{
				if (info.Indices.Count > 0 && treeView != null)
				{
					QAliberTreeNode nodeToFind = treeView.Nodes[info.Indices[0]] as QAliberTreeNode;
					for (int i = 1; i < info.Indices.Count; i++)
					{
						int index = info.Indices[i] >= nodeToFind.Nodes.Count ? nodeToFind.Nodes.Count - 1 : info.Indices[i];
						if (index >= 0)
							nodeToFind = nodeToFind.Nodes[index] as QAliberTreeNode;
					}
					if (nodeToFind.Parent != null)
					{
						((QAliberTreeNode)nodeToFind.Parent).RemoveChild(nodeToFind);
						
					}
				}
			}
		}

		public void Undo()
		{
			CommandInfo.IncreaseIndices(storedNodes);
			foreach (CommandInfo info in storedNodes)
			{
				if (info.Indices.Count > 1)
				{
					QAliberTreeNode parentNodeToFind = treeView.Nodes[info.Indices[0]] as QAliberTreeNode;
					for (int i = 1; i < info.Indices.Count - 1; i++)
					{
						int index = info.Indices[i] >= parentNodeToFind.Nodes.Count ? parentNodeToFind.Nodes.Count - 1 : info.Indices[i];
						parentNodeToFind = parentNodeToFind.Nodes[index] as QAliberTreeNode;
					}
					parentNodeToFind.InsertChild(info.Indices[info.Indices.Count - 1], info.Node);
				}
				//else if (info.Indices.Count == 1)
				//{
				//	  treeView.Nodes.Add(info.Node);
				//}
				
			}
		}

		public void Redo()
		{
			Do();
		}

		#endregion

		

		private QAliberTreeView treeView;
		private List<CommandInfo> storedNodes = new List<CommandInfo>();
	}
}
