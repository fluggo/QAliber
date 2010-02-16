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