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
using System.Linq;

namespace QAliber.Builder.Presentation.Commands
{
	public class TreeClipboard
	{
		private TreeClipboard()
		{
		}

		public static TreeClipboard Default
		{
			get
			{
				if (instance == null)
					instance = new TreeClipboard();
				return instance;
			}
		}

		public QAliberTreeNode[] Nodes
		{
			get { return clipNodes; }
			set { clipNodes = value; }
		}

		public bool Cutted
		{
			get { return cutted; }
			set { cutted = value; }
		}


		public void StoreInClipboard(QAliberTreeNode[] nodes, bool makeCopy)
		{
			if (clipNodes != null)
			{
				foreach (TreeNode node in clipNodes)
				{
					if (node.ForeColor == System.Drawing.Color.LightGray)
					{
						node.BackColor = System.Drawing.Color.White;
						node.ForeColor = ((QAliberTreeNode)node).Testcase.Color;
					}
				}
			}
			nodes = FilterDescandants(nodes);
			if (makeCopy)
			{
				List<QAliberTreeNode> resList = new List<QAliberTreeNode>();
				foreach (QAliberTreeNode node in nodes)
				{
					resList.Add((QAliberTreeNode)node.CompleteClone());
				}
				clipNodes = resList.ToArray();
			}
			else
			{
				clipNodes = nodes;
			}
		   
		}

		public QAliberTreeNode[] FilterDescandants(QAliberTreeNode[] nodes)
		{
			List<QAliberTreeNode> nodesList = new List<QAliberTreeNode>();
			nodesList.AddRange(nodes);
			nodesList = nodesList.OrderBy( n => n.Level * 1000 + n.Index ).ToList();
			for (int i = 0; i < nodesList.Count - 1; i++)
			{
				if (nodesList[i].TreeView != null)
				{
					for (int j = i + 1; j < nodesList.Count; j++)
					{
						if (nodesList[j].TreeView != null)
						{
							if (nodesList[j].FullPath.StartsWith(nodesList[i].FullPath) && nodesList[j].FullPath.Length > nodesList[i].FullPath.Length)
							{
								nodesList.RemoveAt(j);
								break;
							}
						}
					}
				}
			}
			return nodesList.OrderBy( n => n.Level * 1000 + n.Index ).ToArray();
		}

		private bool cutted;
		private QAliberTreeNode[] clipNodes;
		private static TreeClipboard instance = new TreeClipboard();
	}
}
