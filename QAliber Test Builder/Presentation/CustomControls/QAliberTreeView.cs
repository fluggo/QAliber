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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;
using System.Diagnostics;

namespace QAliber.Builder.Presentation
{
	public partial class QAliberTreeView : TreeView
	{
		public QAliberTreeView()
		{
			InitializeComponent();
			selectedNodes = new List<QAliberTreeNode>();
		}

		public event EventHandler<NodeDraggedEventArgs> NodeDragged;

		public event EventHandler<NodeDraggedEventArgs> TestCaseDragged;

		public QAliberTreeNode[] SelectedNodes
		{
			get
			{
				return selectedNodes.ToArray();
			}
			set
			{
				foreach (TreeNode node in selectedNodes)
				{
					UnMarkNode(node);
				}
				selectedNodes.Clear();
				if (value != null)
					selectedNodes.AddRange(value);
				foreach (TreeNode node in selectedNodes)
				{
					MarkNode(node);
				}
			}
		}

		public bool EnableComplexCheck
		{
			get { return enableComplexCheck; }
			set { enableComplexCheck = value; }
		}


		#region Overrides
		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			if (!cancelMultiSelection && ModifierKeys == Keys.Control && selectedNodes.Contains((QAliberTreeNode)e.Node))
			{
				e.Cancel = true;
				selectedNodes.Remove((QAliberTreeNode)e.Node);
				UnMarkNode(e.Node);
			}
			base.OnBeforeSelect(e);
		}

		protected override void OnAfterSelect(TreeViewEventArgs e)
		{

			if (ModifierKeys == Keys.Control && !cancelMultiSelection)
			{
				if (selectedNodes.Contains((QAliberTreeNode)e.Node))
				{
					selectedNodes.Remove((QAliberTreeNode)e.Node);
					UnMarkNode(e.Node);
				}
				else
				{
					selectedNodes.Add((QAliberTreeNode)e.Node);
					MarkNode(e.Node);
				}
			}
			else if (ModifierKeys == Keys.Shift && !cancelMultiSelection)
			{
				if (startNode != null && !startNode.Equals(e.Node))
				{
					TreeNode[] nodes = GetNodesOrdered(startNode, e.Node);
					MarkRange(nodes[0], nodes[1]);
				}
			}
			else
			{
				foreach (TreeNode node in selectedNodes)
				{
					UnMarkNode(node);
				}
				selectedNodes.Clear();
				selectedNodes.Add((QAliberTreeNode)e.Node);
				MarkNode(e.Node);

			}
			startNode = e.Node;
			base.OnAfterSelect(e);
		}

		protected override void OnAfterCheck(TreeViewEventArgs e)
		{
			if (enableComplexCheck)
			{
				if (e.Node.Checked == false)
				{
					if (enableDescendantsChecking)
					{
						enableComplexCheck = false;
						SetCheckForDescendants(e.Node, false);
						enableComplexCheck = true;
					}
					if (AreAllSiblingsChecked(e.Node, false))
					{
						if (e.Node.Parent != null)
						{
							enableDescendantsChecking = false;
							e.Node.Parent.Checked = false;
							enableDescendantsChecking = true;
						}
					}
				}
				else
				{
					if (enableDescendantsChecking)
					{
						enableComplexCheck = false;
						SetCheckForDescendants(e.Node, true);
						enableComplexCheck = true;
					}
					if (e.Node.Parent != null)
					{
						enableDescendantsChecking = false;
						e.Node.Parent.Checked = true;
						enableDescendantsChecking = true;
					}
				}
			}
			base.OnAfterCheck(e);
		}

		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			nodeToBeDragged = e.Item as QAliberTreeNode;
			DoDragDrop(e.Item.ToString(), DragDropEffects.Copy | DragDropEffects.Move);
			base.OnItemDrag(e);
		}

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (drgevent.Data.GetDataPresent(DataFormats.Text) || drgevent.Data.GetData(typeof(TreeNode)) != null)
				drgevent.Effect = DragDropEffects.Move;
			else
				drgevent.Effect = DragDropEffects.None;
			base.OnDragEnter(drgevent);
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			QAliberTreeNode targetNode = GetNodeAt(PointToClient(new Point(drgevent.X, drgevent.Y))) as QAliberTreeNode;
			if (lastOverNode != null)
				UnMarkNode(lastOverNode);
			string data = drgevent.Data.GetData(typeof(string)) as string;

			if (data != null)
			{
				if (targetNode != null &&
					!IsAncestor(nodeToBeDragged, targetNode)
					&& targetNode != nodeToBeDragged
					&& targetNode != nodeToBeDragged.Parent)
				{
					OnNodeDragged(targetNode);
				}
			}
			else
			{
				TreeNode typeNode = drgevent.Data.GetData(typeof(TreeNode)) as TreeNode;
				if (typeNode != null)
				{
					if (targetNode != null && targetNode.Testcase != null && typeNode.Tag != null)
					{
						TestCase selectedTest = typeNode.Tag as TestCase;
						if (selectedTest != null)
						{
							QAliberTreeNode QAliberNode = new QAliberTreeNode(selectedTest);
							OnTestCaseDragged(QAliberNode, targetNode);
						}
						else
						{
							string file = typeNode.Tag as string;
							if (file != null)
							{
								foreach (Type type in TestController.Default.SupportedTypes)
								{
									if (type.Name == "PlayMacroTestCase")
									{
										selectedTest = Activator.CreateInstance(type) as TestCase;
										type.GetProperty("Filename").SetValue(selectedTest, file, null);
										selectedTest.Name = System.IO.Path.GetFileNameWithoutExtension(file);
										QAliberTreeNode QAliberNode = new QAliberTreeNode(selectedTest);
										OnTestCaseDragged(QAliberNode, targetNode);
										break;
									}
									
								}
							}
						}
					}
				}
			}
			base.OnDragDrop(drgevent);
		}

		protected override void OnDragOver(DragEventArgs drgevent)
		{
			TreeNode overNode = GetNodeAt(PointToClient(new Point(drgevent.X, drgevent.Y)));
			if (overNode != null)
			{
				if (lastOverNode != null)
					UnMarkNode(lastOverNode);
				MarkNode(overNode);
				lastOverNode = overNode;
			}
			base.OnDragOver(drgevent);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				QAliberTreeNode targetNode = GetNodeAt(new Point(e.X, e.Y)) as QAliberTreeNode;
				if (targetNode != null)
				{
					SelectedNode = targetNode;
					SelectedNodes = new QAliberTreeNode[] { targetNode };
				}
			}
			base.OnMouseDown(e);
		}

		protected virtual void OnNodeDragged(QAliberTreeNode targetNode)
		{
			if (NodeDragged != null)
				NodeDragged(this, new NodeDraggedEventArgs(nodeToBeDragged, targetNode));
		}

		protected virtual void OnTestCaseDragged(QAliberTreeNode typeNode, QAliberTreeNode targetNode)
		{
			if (TestCaseDragged != null)
				TestCaseDragged(this, new NodeDraggedEventArgs(typeNode, targetNode));
		}

		#endregion

		#region Private Methods
		private TreeNode[] GetNodesOrdered(TreeNode node1, TreeNode node2)
		{
			TreeNode node = Nodes[0];
			while (node != null)
			{
				if (node.Equals(node1))
					return new TreeNode[] { node1, node2 };
				if (node.Equals(node2))
					return new TreeNode[] { node2, node1 };
				node = node.NextVisibleNode;
			}
			return null;
		}

		private void MarkRange(TreeNode start, TreeNode end)
		{
			TreeNode node = start;
			foreach (TreeNode nodeToRemove in selectedNodes)
			{
				UnMarkNode(nodeToRemove);
			}
			selectedNodes.Clear();
			while (node != null)
			{

				selectedNodes.Add((QAliberTreeNode)node);
				MarkNode(node);

				if (node.Equals(end))
					break;
				node = node.NextVisibleNode;
			}
		}

		private void MarkNode(TreeNode node)
		{
			node.BackColor = SystemColors.Highlight;
			node.ForeColor = Color.White;
		}

		private void UnMarkNode(TreeNode node)
		{
			QAliberTreeNode qnode = node as QAliberTreeNode;
			if (qnode != null)
			{
				qnode.BackColor = qnode.NonHighlightBackColor;
				qnode.ForeColor = qnode.NonHighlightForeColor;
			}
		}

		private bool IsAncestor(TreeNode parent, TreeNode child)
		{
			while (child.Parent != null)
			{
				child = child.Parent;
				if (child.Equals(parent))
					return true;
			}
			return false;
		}

		private void SetCheckForDescendants(TreeNode node, bool isChecked)
		{
			foreach (TreeNode child in node.Nodes)
			{
				child.Checked = isChecked;
				SetCheckForDescendants(child, isChecked);
			}
		}

		private bool AreAllSiblingsChecked(TreeNode node, bool checkedState)
		{
			TreeNode firstNode = null;
			if (node.Parent != null)
				firstNode = node.Parent.Nodes[0];
			else
				firstNode = Nodes[0];
			while (firstNode != null)
			{
				if (firstNode.Checked != checkedState)
					return false;
				firstNode = firstNode.NextNode;
			}
			return true;

		}
		#endregion

		internal bool cancelMultiSelection = false;
		private bool enableDescendantsChecking = true;
		private bool enableComplexCheck;
		private TreeNode lastOverNode;
		private QAliberTreeNode nodeToBeDragged;
		private TreeNode startNode;
		private List<QAliberTreeNode> selectedNodes;
	}

	[Serializable]
	public class QAliberTreeNode : TreeNode
	{
		public QAliberTreeNode() : base()
		{

		}

		public QAliberTreeNode(TestCase testcase) : base()
		{
			Text = Name = testcase.Name;
			Checked = testcase.MarkedForExecution;
			Testcase = testcase;
			ForeColor = nonHighlightForeColor = testcase.Color;
			nonHighlightBackColor = base.BackColor;
		}

		private TestCase testcase;

		public TestCase Testcase
		{
			get { return testcase; }
			set { testcase = value; }
		}

		private Color nonHighlightForeColor;

		public Color NonHighlightForeColor
		{
			get { return nonHighlightForeColor; }
			set { nonHighlightForeColor = value; }
		}

		private Color nonHighlightBackColor;

		public Color NonHighlightBackColor
		{
			get { return nonHighlightBackColor; }
			set { nonHighlightBackColor = value; }
		}

		public void AddChild(TestCase testCase)
		{
			FolderTestCase folderCase = this.testcase as FolderTestCase;
			if (testCase != null)
			{
				QAliberTreeNode node = new QAliberTreeNode(testCase);
			   
				Nodes.Add(node);
				folderCase.Children.Add(testCase);
				testCase.Scenario = folderCase.Scenario;
				testCase.Parent = folderCase;
			}

		}

		public QAliberTreeNode LinkChild(TestCase testCase)
		{
			FolderTestCase folderCase = this.testcase as FolderTestCase;
			if (testCase != null)
			{
				QAliberTreeNode node = new QAliberTreeNode(testCase);

				Nodes.Add(node);
				testCase.Scenario = folderCase.Scenario;
				testCase.Parent = folderCase;
				return node;
			}
			return null;

		}

		public void AddChild(QAliberTreeNode child)
		{
			FolderTestCase folderCase = this.testcase as FolderTestCase;
			if (folderCase != null)
			{
				Nodes.Add(child);
				folderCase.Children.Add(child.Testcase);
				child.Testcase.Scenario = folderCase.Scenario;
				child.Testcase.Parent = folderCase;
			}

		}

		public void InsertChild(int index, TestCase testCase)
		{
			FolderTestCase folderCase = this.testcase as FolderTestCase;
			if (testCase != null)
			{
				QAliberTreeNode node = new QAliberTreeNode(testCase);
				
				Nodes.Insert(index, node);
				folderCase.Children.Insert(index, testCase);
				testCase.Scenario = folderCase.Scenario;
				testCase.Parent = folderCase;
			}
		}

		public void InsertChild(int index, QAliberTreeNode node)
		{
			FolderTestCase folderCase = this.testcase as FolderTestCase;
			if (folderCase != null)
			{
				Nodes.Insert(index, node);
				folderCase.Children.Insert(index, node.Testcase);
				node.Testcase.Scenario = folderCase.Scenario;
				node.Testcase.Parent = folderCase;
			}
		}

		public void RemoveChild(QAliberTreeNode node)
		{
			FolderTestCase folderCase = this.testcase as FolderTestCase;
			TestCase caseToBeRemoved = node.Testcase;
			if (folderCase != null && caseToBeRemoved != null)
			{
				Nodes.Remove(node);
				folderCase.Children.Remove(caseToBeRemoved);
			}
		}

		public override object Clone()
		{
			QAliberTreeNode newNode = base.Clone() as QAliberTreeNode;
			newNode.Testcase = Testcase.Clone() as TestCase;
			UpdateTags(newNode, newNode.Testcase);
			newNode.NonHighlightForeColor = newNode.ForeColor = newNode.Testcase.Color;
			newNode.NonHighlightBackColor = newNode.BackColor = Color.White;
			return newNode;
		}

		private void UpdateTags(QAliberTreeNode node, TestCase testcase)
		{
			node.Testcase = testcase;
			FolderTestCase folder = testcase as FolderTestCase;
			if (folder != null)
			{
				if (node.Nodes.Count == folder.Children.Count)
				{

					for (int i = 0; i < folder.Children.Count; i++)
					{
						UpdateTags((QAliberTreeNode)node.Nodes[i], folder.Children[i]);
					}
				}
			}
		}
	
	}

	public class NodeDraggedEventArgs : EventArgs
	{
		public NodeDraggedEventArgs(QAliberTreeNode src, QAliberTreeNode target)
		{
			this.src = src;
			this.target = target;
		}

		private QAliberTreeNode src;

		public QAliberTreeNode SourceNode
		{
			get { return src; }
			set { src = value; }
		}

		private QAliberTreeNode target;

		public QAliberTreeNode TargetNode
		{
			get { return target; }
			set { target = value; }
		}


	}

	static class TreenodeExtensions
	{
		public static TreeNode GetNextNode(this TreeNode node)
		{
			if (node == null)
				return null;
			if (node.Nodes.Count > 0)
				return node.Nodes[0];
			if (node.NextNode != null)
				return node.NextNode;
			while (node.Parent != null && node.Parent.NextNode == null)
				node = node.Parent;
			if (node.Parent != null)
				return node.Parent.NextNode;
			return null;
		}

		public static TreeNode GetPrevNode(this TreeNode node)
		{
			if (node == null)
				return null;
			if (node.PrevNode == null)
				return node.Parent;
			else
			{
				node = node.PrevNode;
				while (node.Nodes.Count > 0)
				{
					node = node.Nodes[node.Nodes.Count - 1];
				}
				return node;
			}

		}
	}



}

///Hack to get extension methods work in framework 2.0
namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ExtensionAttribute : Attribute
	{
		public ExtensionAttribute() { }
	}
}
