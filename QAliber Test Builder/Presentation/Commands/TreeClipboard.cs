using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;

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
					resList.Add((QAliberTreeNode)node.Clone());
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
			nodesList.Sort(new NodeLevelSorter());
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
			nodesList.Sort(new NodeLevelSorter());
			return nodesList.ToArray();
		}

		private bool cutted;
		private QAliberTreeNode[] clipNodes;
		private static TreeClipboard instance = new TreeClipboard();
	}

	class NodeLevelSorter : IComparer<QAliberTreeNode>
	{


		#region IComparer<TreeNode> Members

		public int Compare(QAliberTreeNode x, QAliberTreeNode y)
		{
			return x.Level * 1000 - y.Level * 1000 + x.Index - y.Index;
		}

		#endregion
	}
}
