using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Builder.Presentation.Commands
{
	public class CommandsCollection
	{
		private TreeNode clipboardNode;

		public TreeNode ClipboardNode
		{
			get { return clipboardNode; }
			set { clipboardNode = value; }
		}

		private bool isLastCut;

		public bool IsLastCut
		{
			get { return isLastCut; }
			set { isLastCut = value; }
		}

		//private static CommandsCollection instance;

		//public static CommandsCollection Default
		//{
		//	  get 
		//	  {
		//		  if (instance == null)
		//			  instance = new CommandsCollection();
		//		  return instance;
		//	  }
		//}

		public void Do(ICommand command)
		{
			command.Do();
			undoStack.Push(command);
			redoStack.Clear();
		}

		public void Undo()
		{
			if (undoStack.Count > 0)
			{
				ICommand command = undoStack.Pop();
				command.Undo();
				redoStack.Push(command);
			}
		}

		public void Redo()
		{
			if (redoStack.Count > 0)
			{
				ICommand command = redoStack.Pop();
				command.Redo();
				undoStack.Push(command);
			}
		}

		private Stack<ICommand> undoStack = new Stack<ICommand>();
		private Stack<ICommand> redoStack = new Stack<ICommand>();

		private const int maxHistory = 20;

	}

	public class CommandInfo
	{
		public CommandInfo(QAliberTreeNode node)
		{
			CloneAndStore(node);
		}

		private void CloneAndStore(QAliberTreeNode node)
		{
			QAliberTreeNode parent = node;
			while (parent != null)
			{
				indices.Insert(0, parent.Index);
				parent = parent.Parent as QAliberTreeNode;
			}
			this.node = node.Clone() as QAliberTreeNode;
			
		}

		public void UpdateIndices()
		{
			QAliberTreeNode parent = node;
			indices.Clear();
			while (parent != null)
			{
				indices.Insert(0, parent.Index);
				parent = parent.Parent as QAliberTreeNode;
			}
		}

		public static void DecreaseIndices(List<CommandInfo> infos)
		{
			if (infos.Count > 0)
			{
				int index = 0;
				List<CommandInfo> list = new List<CommandInfo>();
				do
				{
					list = GetSameLevelList(infos, index, out index);
					for (int i = 1; i < list.Count; i++)
					{
						list[i].Indices[list[i].Indices.Count - 1] -= i;
					}

				} while (index < infos.Count);
				
				
			}
			//if (a.Indices.Count > b.Indices.Count)
			//	  return;
			//else 
			//{
			//	  if (a.Indices[a.Indices.Count - 1] < b.Indices[a.Indices.Count - 1])
			//		  b.Indices[a.Indices.Count - 1]--;
			//}
		}

		public static void IncreaseIndices(List<CommandInfo> infos)
		{
			if (infos.Count > 0)
			{
				int index = 0;
				List<CommandInfo> list = new List<CommandInfo>();
				do
				{
					list = GetSameLevelList(infos, index, out index);
					for (int i = 1; i < list.Count; i++)
					{
						list[i].Indices[list[i].Indices.Count - 1] += i;
					}

				} while (index < infos.Count);


			}
			//if (a.Indices.Count > b.Indices.Count)
			//	  return;
			//else 
			//{
			//	  if (a.Indices[a.Indices.Count - 1] < b.Indices[a.Indices.Count - 1])
			//		  b.Indices[a.Indices.Count - 1]--;
			//}
		}

		private static List<CommandInfo> GetSameLevelList(List<CommandInfo> infos, int startIndex, out int endIndex)
		{
			List<CommandInfo> res = new List<CommandInfo>();
			int level = infos[startIndex].Indices.Count;
			int i = startIndex + 1;
			res.Add(infos[startIndex]);
			for (; i < infos.Count; i++)
			{
				if (infos[i].Indices.Count != level)
					break;
				res.Add(infos[i]);
			}
			endIndex = i;
			return res;
		}

		private List<int> indices = new List<int>();

		public List<int> Indices
		{
			get { return indices; }
			set { indices = value; }
		}

		private QAliberTreeNode node;

		public QAliberTreeNode Node
		{
			get { return node; }
			set { node = value; }
		}
	}
}

