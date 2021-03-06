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
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using QAliber.Logger;
using QAliber.RemotingModel;
using System.Globalization;

namespace QAliber.Logger.Controls
{
	public partial class LogViewerControl : UserControl
	{
		public LogViewerControl()
		{
			InitializeComponent();

			#region Set Toolstrip Images
			toolStripMain.ImageList = imageTree;
			infoFilter.ImageKey = "Info";
			errorFilter.ImageKey = "Error";
			warningFilter.ImageKey = "Warning";
			pictureFilter.ImageKey = "Picture";
			linkFilter.ImageKey = "Link";
			normalFilter.ImageKey = "Normal";
			debugFilter.ImageKey = "Debug";
			internalFilter.ImageKey = "Internal";
			criticalFilter.ImageKey = "Critical";
			#endregion
		}

		public string Filename
		{
			get { return filename; }
			set
			{
				if (value != null)
				{
					filename = value;
					if (processFilter.Checked)
					{
						processFilter.Checked = false;
						processFilter_Click(null, EventArgs.Empty);
					}
					FillTree();
				}
			}
		}

		private bool LogFilterTypeChecked
		{
			get { return infoFilter.Checked || warningFilter.Checked || errorFilter.Checked || linkFilter.Checked || pictureFilter.Checked; }
		}

		private bool LogFilterVerbosityChecked
		{
			get { return normalFilter.Checked || criticalFilter.Checked || debugFilter.Checked || internalFilter.Checked; }
		}


		public void SelectNodeByDate(DateTime date)
		{
			foreach (TreeNode node in logTree.Nodes)
			{
				TreeNode res = SelectNodeByDateRec(node, date);
				if (res != null)
				{
					logTree.SelectedNode = res;
					res.EnsureVisible();
					break;
				}
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.Down))
			{
				jumpNextErrorToolStripMenuItem_Click(this, EventArgs.Empty);
				return true;
			}
			else if (keyData == (Keys.Control | Keys.Up))
			{
				jumpPrevErrorToolStripMenuItem_Click(this, EventArgs.Empty);
				return true;
			}
			else  if (keyData == (Keys.Control | Keys.C))
			{
				// Don't accept this unless the tree itself has focus
				TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
				if( visibleTree.ContainsFocus && visibleTree.SelectedNode != null ) {
					CopyFromNode( visibleTree.SelectedNode );
					return true;
				}

				// None of our copy commands will apply to it; hopefully the control
				// with focus will know what to do
				return false;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private TreeNode SelectNodeByDateRec(TreeNode node, DateTime time)
		{
			DateTime last = ((LogEntry)node.Tag).Time;
			foreach (TreeNode child in node.Nodes)
			{
				TreeNode res = SelectNodeByDateRec(child, time);
				if (res != null)
					return res;
				if (time >= last && time <= ((LogEntry)child.Tag).Time)
					return child.PrevNode == null ? child : child.PrevNode;
				last = ((LogEntry)child.Tag).Time;
				
			}
			return null;
		}

		#region Events

		private void logTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag != null && e.Node.Tag is LogEntry)
			{
				try
				{
					LogEntry entry = (LogEntry)e.Node.Tag;
					if (entry.Link != string.Empty && entry.Body == BodyType.Picture)
					{
						slideShowControl.SendToBack();
						perfGraphControl.SendToBack();
						pictureBox.ImageFile = entry.Link;
					}
					else
					{
						perfGraphControl.BringToFront();
						slideShowControl.BringToFront();
						pictureBox.Image = null;
					}
					if (slideShowControl.Visible)
					{
						perfGraphControl.SendToBack();
						slideShowControl.SeekClosestFrameByTime(entry.Time);
					}

					richTextBoxRemarks.Text = entry.ExtendedMessage;
					toolStripTime.Text = "Time : " + entry.Time.ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error In Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void countThisMessageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			counter = 0;
			TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
			Point treePoint = visibleTree.PointToClient(new Point(nodeMenuStrip.Left, nodeMenuStrip.Top));
			TreeNode clickedNode = visibleTree.GetNodeAt(treePoint.X, treePoint.Y);
			if (clickedNode != null)
			{
				SumInTreeRec(visibleTree.Nodes, clickedNode.Text, 0);
				toolStripCount.Text = clickedNode.Text + " : " + counter;
			}
		}

		private void countItemsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			counter = 0;
			TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
			Point treePoint = visibleTree.PointToClient(new Point(nodeMenuStrip.Left, nodeMenuStrip.Top));
			TreeNode clickedNode = visibleTree.GetNodeAt(treePoint.X, treePoint.Y);
			if (clickedNode != null)
			{
				SumInTreeRec(visibleTree.Nodes, "", clickedNode.Level);
				toolStripCount.Text = "Level " + clickedNode.Level + " : " + counter;
			}
		}

		private void copyThisMessageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
			Point treePoint = visibleTree.PointToClient(new Point(nodeMenuStrip.Left, nodeMenuStrip.Top));
			TreeNode clickedNode = visibleTree.GetNodeAt(treePoint.X, treePoint.Y);
			CopyFromNode( clickedNode );
		}

		private void CopyFromNode( TreeNode node ) {
			if( node == null )
				return;

			LogEntry entry = node.Tag as LogEntry;
			if (entry != null)
			{
				Clipboard.SetText(string.Format("{0},{1},{2},{3}",
					node.FullPath, entry.Time, entry.Message, entry.ExtendedMessage));
			}
		}

		private void countAllTheChildrenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			counter = 0;
			TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
			Point treePoint = visibleTree.PointToClient(new Point(nodeMenuStrip.Left, nodeMenuStrip.Top));
			TreeNode clickedNode = visibleTree.GetNodeAt(treePoint.X, treePoint.Y);
			if (clickedNode != null)
			{
				SumInTreeRec(clickedNode.Nodes, "", 0);
				toolStripCount.Text = clickedNode.Text + " Children : " + counter;
			}
		}

		private void showLeavesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (showLeavesToolStripMenuItem.Checked)
			{
				TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
				Point treePoint = visibleTree.PointToClient(new Point(nodeMenuStrip.Left, nodeMenuStrip.Top));
				TreeNode clickedNode = visibleTree.GetNodeAt(treePoint.X, treePoint.Y);
				logTreeFlattened.Nodes.Clear();
				FlattenTree(clickedNode.Nodes);
				if (logTreeFlattened.Nodes.Count > 0)
				{
					logTreeFlattened.Visible = true;
					logTreeFlattened.BringToFront();
				}
				else
					showLeavesToolStripMenuItem.Checked = false;
			}
			else
			{
				logTreeFlattened.Visible = false;
				logTreeFlattened.SendToBack();
			}

			
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = openFileDialog.ShowDialog();
			if (dr == DialogResult.OK)
			{
				try
				{
					if (processFilter.Checked)
					{
						processFilter.Checked = false;
						processFilter_Click(null, EventArgs.Empty);
					}
					
					filename = openFileDialog.FileName;
					FillTree();
					Text = "QAliber Log Viewer - " + filename;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error During Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void processFilter_Click(object sender, EventArgs e)
		{
			if (processFilter.Checked)
				FilterTree();
			logTreeFiltered.Visible = processFilter.Checked;
			logTree.Visible = !processFilter.Checked;
		}

		private void zoomPicture_Click(object sender, EventArgs e)
		{
			panPicture.Checked = false;
			if (!zoomPicture.Checked)
			{
				ZoomPanControl.Action = ZoomPanActionType.None;
				pictureBox.Cursor = Cursors.Default;
			}
			else
			{
				ZoomPanControl.Action = ZoomPanActionType.Zoom;
				pictureBox.Cursor = new Cursor(new MemoryStream(Properties.Resources.Zoom, false));
			}
		}

		private void panPicture_Click(object sender, EventArgs e)
		{
			zoomPicture.Checked = false;
			if (!panPicture.Checked)
			{
				ZoomPanControl.Action = ZoomPanActionType.None;
				pictureBox.Cursor = Cursors.Default;
			}
			else
			{
				ZoomPanControl.Action = ZoomPanActionType.Pan;
				pictureBox.Cursor = Cursors.Hand;
			}
		}

		private void refreshToolStripButton_Click(object sender, EventArgs e)
		{
			FillTree();
		}

		private void logTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag != null && e.Node.Tag is LogEntry)
			{
				try
				{
					LogEntry entry = (LogEntry)e.Node.Tag;
					if (!string.IsNullOrEmpty(entry.Link) && entry.Body == BodyType.Link)
						Process.Start(entry.Link);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error Opening Link", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

		}

		private void jumpNextErrorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
			if (visibleTree.Nodes.Count == 0)
				return;
			TreeNode node = GetNextNode(visibleTree.SelectedNode);
			if (node == null)
				node = visibleTree.Nodes[0];
			do
			{
				if (node.Nodes.Count == 0 &&
				   (node.ImageKey == "Error" || node.ImageKey == "Warning"))  
				{
					node.EnsureVisible();
					visibleTree.SelectedNode = node;
					return;
				}
				node = GetNextNode(node);
			} while (node != null);
			//MessageBox.Show("No next match was found", "Test case was not found");
			visibleTree.SelectedNode = visibleTree.Nodes[0];
		}

		private void jumpPrevErrorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeView visibleTree = logTree.Visible ? logTree : logTreeFiltered;
			if (visibleTree.Nodes.Count == 0)
				return;
			TreeNode node = GetPrevNode(visibleTree.SelectedNode);
			if (node == null)
				node = visibleTree.Nodes[0];
			do
			{
				if (node.Nodes.Count == 0 &&
				   (node.ImageKey == "Error" || node.ImageKey == "Warning"))
				{
					node.EnsureVisible();
					visibleTree.SelectedNode = node;
					return;
				}
				node = GetPrevNode(node);
			} while (node != null);
			//MessageBox.Show("No next match was found", "Test case was not found");
			visibleTree.SelectedNode = visibleTree.Nodes[0];
		}

		private void videoPanelToolStripButton_Click(object sender, EventArgs e)
		{
			slideShowControl.BringToFront();
		}

		private void resourcesGraphToolStripButton_Click(object sender, EventArgs e)
		{
			perfGraphControl.BringToFront();
		}

		#endregion

		private void FilterTree()
		{
			MarkFiltersRec(logTree.Nodes);
			FillFilteredTree();
		}

		private void MarkFiltersRec(TreeNodeCollection nodes)
		{

			for (int i = 0; i < nodes.Count; i++)
			{
				TreeNode node = nodes[i];
				if (node.Nodes.Count > 0)
				{
					MarkFiltersRec(node.Nodes);
				}
				if (AllNodesForRemoval(node))
				{
					LogEntry entry = (LogEntry)node.Tag;
					bool shouldStay = true;
					if (LogFilterTypeChecked)
					{
						switch (entry.Type)
						{
							case EntryType.Info:
								shouldStay = infoFilter.Checked;
								break;
							case EntryType.Warning:
								shouldStay = warningFilter.Checked;
								break;
							case EntryType.Error:
								shouldStay = errorFilter.Checked;
								break;
							default:
								break;
						}
						if (!shouldStay)
						{
							switch (entry.Body)
							{
								case BodyType.Link:
									if (linkFilter.Checked)
										shouldStay = true;
									break;
								case BodyType.Picture:
									if (pictureFilter.Checked)
										shouldStay = true;
									break;
								default:
									break;
							}
						}
					}
					if (shouldStay && LogFilterVerbosityChecked)
					{
						switch (entry.Verbosity)
						{
							case EntryVerbosity.Internal:
								shouldStay = internalFilter.Checked;
								break;
							case EntryVerbosity.Debug:
								shouldStay = debugFilter.Checked;
								break;
							case EntryVerbosity.Normal:
								shouldStay = normalFilter.Checked;
								break;
							case EntryVerbosity.Critical:
								shouldStay = criticalFilter.Checked;
								break;
							default:
								break;
						}
					}
					if (!shouldStay)
						node.Name = "Remove";
					else
					{
						if (stringFilter.Text.Length == 0 || Regex.Match(node.Text, stringFilter.Text).Success)
							node.Name = "Stay";
						else
							node.Name = "Remove";
					}
				}
				else
					node.Name = "Stay";

			}

		}

		private void SumInTreeRec(TreeNodeCollection nodes, string text, int levelToSum)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				TreeNode node = nodes[i];
				if (text.Length > 0 && node.Text == text)
				{
					counter++;
				}
				else if (levelToSum != 0 && levelToSum == node.Level)
				{
					counter++;
				}
				else if (levelToSum == 0 && text.Length == 0)
				{
					counter++;
				}
				SumInTreeRec(node.Nodes, text, levelToSum);
			}

		}

		private void RemoveNodesRec(TreeNodeCollection nodes)
		{

			for (int i = 0; i < nodes.Count; i++)
			{
				TreeNode node = nodes[i];
				if (node.Name == "Remove")
				{
					node.Remove();
					i--;
				}
				else
					RemoveNodesRec(node.Nodes);
			}
		}

		private bool AllNodesForRemoval(TreeNode node)
		{
			foreach (TreeNode childNode in node.Nodes)
			{
				if (childNode.Name != "Remove")
					return false;
			}
			return true;
		}

		private void FillFilteredTree()
		{
			logTreeFiltered.Nodes.Clear();
			foreach (TreeNode childNode in logTree.Nodes)
			{
				if (childNode.Name != "Remove")
					logTreeFiltered.Nodes.Add(CloneNode(childNode));
			}
			RemoveNodesRec(logTreeFiltered.Nodes);
		}

		private void FlattenTree(TreeNodeCollection nodes)
		{
			foreach (TreeNode childNode in nodes)
			{
				if (childNode.Nodes.Count == 0)
				{
					logTreeFlattened.Nodes.Add(CloneNode(childNode));
				}
				else
					FlattenTree(childNode.Nodes);
			}
		}

		private void FillTree()
		{
			if (File.Exists(filename))
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(filename);

				logTree.Nodes.Clear();

				foreach (XmlNode node in xmlDoc.ChildNodes)
				{
					if (node.Name == "Log")
					{
						FillTreeRec(logTree.Nodes, node, 0);
					}

				}
				slideShowControl.Init(Path.GetDirectoryName(filename));
				perfGraphControl.Init(Path.GetDirectoryName(filename));
				videoPanelToolStripButton.Enabled = slideShowControl.Visible;
				resourcesGraphToolStripButton.Enabled = perfGraphControl.Visible;
			}
		}

		private void FillTreeRec(TreeNodeCollection tNodes, XmlNode xNode, int resLevel)
		{
			foreach (XmlNode node in xNode.ChildNodes)
			{
				if( node.Name == "LogResult" ) {
					BubbleIconUp((TestCaseResult)Enum.Parse(typeof(TestCaseResult), node.InnerText));
					nodesStack.Pop();
					if (nodesStack.Count >	0)
						currentTestCaseNodes = nodesStack.Peek();
				}
				else if( node.Name == "TestStep" ) {
					LogEntry logEntry = new LogEntry() {
						Message = node.Attributes["name"].Value,
						Time = DateTime.ParseExact( node.Attributes["startTimeUtc"].Value, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ),
						Body = BodyType.Text,
						Verbosity = EntryVerbosity.Normal,
						Type = EntryType.Info
					};

					if( node.Attributes["description"] != null )
						logEntry.ExtendedMessage = node.Attributes["description"].Value;

					TreeNode newNode = new TreeNode( logEntry.Message );
					newNode.Tag = logEntry;
					newNode.SelectedImageKey = newNode.ImageKey = GetImageKeyByEntry(logEntry);
					newNode.ContextMenuStrip = nodeMenuStrip;

					List<TreeNode> newList = new List<TreeNode>();
					currentTestCaseNodes = newList;
					nodesStack.Push(newList);

					tNodes.Add(newNode);

					currentTestCaseNodes.Add(newNode);
					FillTreeRec(newNode.Nodes, node, resLevel + 1);
				}
				else if (node.Name == "Folder")
				{
					LogEntry logEntry = GetEntry(node.FirstChild);
					if (logEntry != null)
					{
						

						TreeNode newNode = new TreeNode(logEntry.Message);
						newNode.Tag = logEntry;
						newNode.SelectedImageKey = newNode.ImageKey = GetImageKeyByEntry(logEntry);
						newNode.ContextMenuStrip = nodeMenuStrip;

						tNodes.Add(newNode);
					   
						currentTestCaseNodes.Add(newNode);
						node.RemoveChild(node.FirstChild);
						FillTreeRec(newNode.Nodes, node, resLevel + 1);
						
					}
				}
				else
				{
					LogEntry logEntry = GetEntry(node);
					if (logEntry != null)
					{
						TreeNode newNode = new TreeNode(logEntry.Message);
						newNode.Tag = logEntry;

						newNode.SelectedImageKey = newNode.ImageKey = GetImageKeyByEntry(logEntry);

						newNode.ContextMenuStrip = nodeMenuStrip;
						tNodes.Add(newNode);

						currentTestCaseNodes.Add(newNode);
					}
				}
			}
		}

		private LogEntry GetEntry(XmlNode node)
		{

			LogEntry result = null;
			try
			{
				if (node.Name == "LogEntry")
				{
					result = new LogEntry();
					result.Message = (node["Message"] != null) ? node["Message"].InnerText : null;
					result.ExtendedMessage = (node["Details"] != null) ? node["Details"].InnerText : null;
					result.Link = (node.Attributes["Link"] != null) ? node.Attributes["link"].Value : null;
					result.Time = DateTime.ParseExact( node.Attributes["timeUtc"].Value, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal );

					if( node.Attributes["bodyType"] != null )
						result.Body = (BodyType)Enum.Parse(typeof(BodyType), node.Attributes["bodyType"].Value);

					if( node.Attributes["verbosity"] != null )
						result.Verbosity = (EntryVerbosity)Enum.Parse(typeof(EntryVerbosity), node.Attributes["verbosity"].Value);

					result.Type = (EntryType)Enum.Parse(typeof(EntryType), node.Attributes["type"].Value);
				}
			}
			catch { }
			return result;
			
			
		}

		private string GetImageKeyByEntry(LogEntry entry)
		{
			if (entry.Body == BodyType.Link)
				return "Link";
			else if (entry.Body == BodyType.Picture)
				return "Picture";
			else
			{
				switch (entry.Type)
				{
					case EntryType.Event:
					case EntryType.Info:
						return "Info";
					case EntryType.Warning:
						return "Warning";
					case EntryType.Error:
						return "Error";
					default:
						return "Info";
				}
			}
		}

		private void BubbleIconUp(TestCaseResult result)
		{
			int indexToSet = 4;//"Passed";
			if (result == TestCaseResult.Failed)
			{
				indexToSet = 7;//"Error";
				TreeNode firstNode = currentTestCaseNodes[0];
				do
				{
					if (firstNode.ImageIndex >= 7)
						break;
					firstNode.SelectedImageIndex = firstNode.ImageIndex = 7;
					firstNode = firstNode.Parent;
				} while (firstNode != null);
			}

			for (int i = 1; i < currentTestCaseNodes.Count; i++)
			{
				int curIndexToSet = indexToSet;
				TreeNode node = currentTestCaseNodes[i];

				if (((LogEntry)node.Tag).Type == EntryType.Error)
					curIndexToSet = Math.Max(6, indexToSet);
				else if (((LogEntry)node.Tag).Type == EntryType.Warning)
					curIndexToSet = 5;
				else
					curIndexToSet = 4;
				while (node.Parent != null)
				{
					node = node.Parent;
					if (node.ImageIndex >= curIndexToSet)
						break;
					node.SelectedImageIndex = node.ImageIndex = curIndexToSet;
				}

			}
			
		}

		private TreeNode GetNextNode(TreeNode node)
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

		private TreeNode GetPrevNode(TreeNode node)
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

		private static TreeNode CloneNode(TreeNode node)
		{
			TreeNode res = (TreeNode)node.Clone();
			res.Tag = node.Tag;
			
			return res;
		}

		private List<TreeNode> currentTestCaseNodes;
		private Stack<List<TreeNode>> nodesStack = new Stack<List<TreeNode>>();
		private int counter;
		private string filename;

		
		
	}

}