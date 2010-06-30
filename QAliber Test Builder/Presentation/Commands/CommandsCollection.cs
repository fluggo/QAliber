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

		private CyclicStack<ICommand> undoStack = new CyclicStack<ICommand>(maxHistory);
		private CyclicStack<ICommand> redoStack = new CyclicStack<ICommand>(maxHistory);

		private const int maxHistory = 10;

	}

	public class CommandInfo
	{
		public CommandInfo(QAliberTreeNode node, bool shouldClone)
		{
			CloneAndStore(node, shouldClone);
		}

		public CommandInfo(QAliberTreeNode node) : this(node, true)
		{
			
		}

		private void CloneAndStore(QAliberTreeNode node, bool shouldClone)
		{
			QAliberTreeNode parent = node;
			while (parent != null)
			{
				indices.Insert(0, parent.Index);
				parent = parent.Parent as QAliberTreeNode;
			}
			if (shouldClone)
				this.node = node.CompleteClone() as QAliberTreeNode;
			
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

	class CyclicStack<T>
	{
		public CyclicStack(int capacity)
		{
			this.capacity = capacity;
			collection = new List<T>();
		}

		public void Push(T item)
		{
			if (collection.Count > capacity)
			{
				collection.RemoveAt(0);
			
			}
			collection.Add(item);
		}

		public T Pop()
		{
			if (collection.Count == 0)
			{
				return default(T);
			}
			T res = collection[collection.Count - 1];
			collection.RemoveAt(collection.Count - 1);
			return res;
		}

		public int Capacity
		{
			get { return capacity; }
		}

		public int Count
		{
			get { return collection.Count; }
		}

		public void Clear()
		{
			collection.Clear();
		}

		private int capacity;
		private List<T> collection;
	}

}

