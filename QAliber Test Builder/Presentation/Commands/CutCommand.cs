using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Builder.Presentation.Commands
{
	public class CutCommand : ICommand
	{
		public CutCommand(QAliberTreeNode[] sourceNodes)
		{
			this.sourceNodes = sourceNodes;
		}

		#region ICommand Members

		public void Do()
		{
			TreeClipboard.Default.StoreInClipboard(sourceNodes, false);
			foreach (TreeNode node in sourceNodes)
			{
				((QAliberTreeNode)node).NonHighlightForeColor = node.ForeColor = System.Drawing.Color.LightGray;
			}
			TreeClipboard.Default.Cutted = true;
		}

		public void Undo()
		{
			return;
		}

		public void Redo()
		{
			return;
		}

		#endregion

		private QAliberTreeNode[] sourceNodes;
	}
}
