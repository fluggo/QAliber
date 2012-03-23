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
	public class InsertCommand : ICommand
	{
		public InsertCommand(QAliberTreeNode sourceNode, QAliberTreeNode[] nodesToInsert)
		{
			treeView = sourceNode.TreeView as QAliberTreeView;
			
			storedSourceNode = new CommandInfo(sourceNode);
			nodesToInsert = TreeClipboard.Default.FilterDescandants(nodesToInsert);

		   

			this.sourceNode = sourceNode;
			this.targetNodes = nodesToInsert;

		}

		#region ICommand Members

		public void Do()
		{
			bool addAsChild = false;
			treeView.SelectedNodes = null;
			if (sourceNode != null)
			{
				if (sourceNode.Testcase is FolderTestCase)
				{
					addAsChild = true;
				}

				int insertIndex = sourceNode.Index + 1;

				foreach (QAliberTreeNode target in targetNodes)
				{
					if (target.ForeColor == System.Drawing.Color.LightGray)
						target.NonHighlightForeColor = target.ForeColor = target.Testcase.Color;
					if (addAsChild)
						sourceNode.AddChild(target);
					else //add after
					{
						if (sourceNode.Parent != null)
							((QAliberTreeNode)sourceNode.Parent).InsertChild( insertIndex++, target);
					}
					
					target.EnsureVisible();
					treeView.SelectedNode = target;
					storedTargetNodes.Add(new CommandInfo(target));
				}
			}


		}

		public void Undo()
		{
			foreach (CommandInfo info in storedTargetNodes)
			{
				if (info.Indices.Count > 0)
				{
					QAliberTreeNode nodeToFind = treeView.Nodes[info.Indices[0]] as QAliberTreeNode;
					for (int i = 1; i < info.Indices.Count; i++)
					{
						int index = info.Indices[i] >= nodeToFind.Nodes.Count ? nodeToFind.Nodes.Count - 1 : info.Indices[i];
						nodeToFind = nodeToFind.Nodes[index] as QAliberTreeNode;
					}
					if (nodeToFind.Parent != null)
					{
						((QAliberTreeNode)nodeToFind.Parent).RemoveChild(nodeToFind);
					}
				}
			}
		}

		public void Redo()
		{
			QAliberTreeNode nodeToFind = null;
			bool addAsChild = false;
			if (storedSourceNode.Indices.Count > 0)
			{
				nodeToFind = treeView.Nodes[storedSourceNode.Indices[0]] as QAliberTreeNode;
				for (int i = 1; i < storedSourceNode.Indices.Count; i++)
				{
					int index = storedSourceNode.Indices[i] >= nodeToFind.Nodes.Count ? nodeToFind.Nodes.Count - 1 : storedSourceNode.Indices[i];
					nodeToFind = nodeToFind.Nodes[index] as QAliberTreeNode;
				}
				if (nodeToFind.Testcase is FolderTestCase)
				{
					addAsChild = true;
				}
			}
			if (nodeToFind != null)
			{
				for (int i =0; i < storedTargetNodes.Count; i++)
				{
					if (addAsChild)
						nodeToFind.AddChild(storedTargetNodes[i].Node);
					else //add after
					{
						if (nodeToFind.Parent != null)
							((QAliberTreeNode)nodeToFind.Parent).InsertChild(nodeToFind.Index + 1, storedTargetNodes[i].Node);
					}
					storedTargetNodes[i] = new CommandInfo(storedTargetNodes[i].Node);
				}
			}
		}

		#endregion

		

		private QAliberTreeView treeView;
		private QAliberTreeNode sourceNode;
		internal QAliberTreeNode[] targetNodes;
		private CommandInfo storedSourceNode;
		private List<CommandInfo> storedTargetNodes = new List<CommandInfo>();

	}
}
