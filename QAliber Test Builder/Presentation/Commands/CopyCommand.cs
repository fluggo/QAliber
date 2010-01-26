using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;

namespace QAliber.Builder.Presentation.Commands
{
	public class CopyCommand : ICommand
	{
		public CopyCommand(QAliberTreeNode[] sourceNodes)
		{
			this.sourceNodes = sourceNodes;
		}

		#region ICommand Members

		public void Do()
		{
			TreeClipboard.Default.StoreInClipboard(sourceNodes, true);
			TreeClipboard.Default.Cutted = false;
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
