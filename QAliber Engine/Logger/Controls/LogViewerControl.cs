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
			else  if (keyData == (Keys.Control | Keys.C))
			{
				copyThisMessageToolStripMenuItem_Click(this, EventArgs.Empty);
				return true;
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

		private void logTree_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				TreeNode node = logTree.GetNodeAt(e.X, e.Y);
				if (node != null && node.Tag != null && node.Tag is LogEntry)
				{
					LogEntry entry = (LogEntry)node.Tag;
					DateTime start = DateTime.Parse(toolStripTime.Text.Replace("Time : ", ""));
					DateTime end = entry.Time;
					toolStripSpan.Text = "Diff : " + ((TimeSpan)(end - start)).ToString();
				}
			}
			catch
			{

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
			if (clickedNode != null)
			{
				LogEntry entry = clickedNode.Tag as LogEntry;
				if (entry != null)
				{
					Clipboard.SetText(string.Format("{0},{1},{2},{3}",
						clickedNode.FullPath, entry.Time, entry.Message, entry.ExtendedMessage));
				}
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
			logTree.BackColor = Color.White;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(filename);
			}
			catch
			{
				PartialLog partLog = new PartialLog(filename);
				if (partLog.TryToFix())
				{
					xmlDoc.Load(partLog.FixedPath);
					logTree.BackColor = Color.LightYellow;
				}
				else 
					throw;
			}

			logTree.Nodes.Clear();

			foreach (XmlNode node in xmlDoc.ChildNodes)
			{
				if (node.Name == "LogEntries")
				{
					FillTreeRec(logTree.Nodes, node);
				}
				
			}
			slideShowControl.Init(Path.GetDirectoryName(filename));
			perfGraphControl.Init(Path.GetDirectoryName(filename));
			videoPanelToolStripButton.Enabled = slideShowControl.Visible;
			resourcesGraphToolStripButton.Enabled = perfGraphControl.Visible;
		}

		private void FillTreeRec(TreeNodeCollection tNodes, XmlNode xNode)
		{
			TreeNode newNode = null;
			LogEntry logEntry;
			bool hasWarnings = false;
			bool hasErrors = false;
			foreach (XmlNode node in xNode.ChildNodes)
			{
				
				if (node.Name == "ChildEntries")
				{
					logEntry = GetEntry(node.FirstChild);
					if (logEntry != null)
					{
						if (logEntry.Type == EntryType.Warning)
							hasWarnings = true;
						else if (logEntry.Type == EntryType.Error)
							hasErrors = true;

						newNode = new TreeNode(logEntry.Message);
						newNode.Tag = logEntry;
						newNode.BackColor = logEntry.Style.BackgroundColor;
						newNode.ForeColor = logEntry.Style.ForegroundColor;
						newNode.NodeFont = new Font(logTree.Font.Name, logTree.Font.Size, logEntry.Style.FontStyle);
						newNode.SelectedImageKey = newNode.ImageKey = GetImageKeyByEntry(logEntry);
						newNode.ContextMenuStrip = nodeMenuStrip;
						tNodes.Add(newNode);
						lastCreatedNode = newNode;
						node.RemoveChild(node.FirstChild);
						FillTreeRec(newNode.Nodes, node);
						
					}
				}
				else if (node.Name == "LogResult")
				{
					BubbleIconUp(lastCreatedNode, (TestCaseResult)Enum.Parse(typeof(TestCaseResult), node.InnerText), hasWarnings, hasErrors);
				}
				else
				{
					logEntry = GetEntry(node);
					if (logEntry != null)
					{
						if (logEntry.Type == EntryType.Warning)
							hasWarnings = true;
						else if (logEntry.Type == EntryType.Error)
							hasErrors = true;

						newNode = new TreeNode(logEntry.Message);
						newNode.BackColor = logEntry.Style.BackgroundColor;
						newNode.ForeColor = logEntry.Style.ForegroundColor;
						newNode.NodeFont = new Font(logTree.Font.Name, logTree.Font.Size, logEntry.Style.FontStyle);
						newNode.Tag = logEntry;

						newNode.SelectedImageKey = newNode.ImageKey = GetImageKeyByEntry(logEntry);

						newNode.ContextMenuStrip = nodeMenuStrip;
						tNodes.Add(newNode);
						lastCreatedNode = newNode;
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
					result.Message = node["Message"].InnerText;
					result.ExtendedMessage = node["ExtendedMessage"].InnerText;
					result.Link = node["Link"].InnerText;
					result.Time = DateTime.Parse(node["Time"].InnerText);
					result.Body = (BodyType)Enum.Parse(typeof(BodyType), node["Body"].InnerText);
					result.Verbosity = (EntryVerbosity)Enum.Parse(typeof(EntryVerbosity), node["Verbosity"].InnerText);
					result.Type = (EntryType)Enum.Parse(typeof(EntryType), node["Type"].InnerText);
					result.Style = new EntryStyle();
					result.Style.FGColorVal = int.Parse(node["Style"]["FGColorVal"].InnerText);
					result.Style.BGColorVal = int.Parse(node["Style"]["BGColorVal"].InnerText);
					result.Style.FontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), node["Style"]["FontStyle"].InnerText);
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

		private void BubbleIconUp(TreeNode node, TestCaseResult result, bool hasWarnings, bool hasErrors)
		{
			int indexToSet = 4;//"Passed";
			if (result == TestCaseResult.Failed)
				indexToSet = 7;//"Error";
			else if (hasErrors)
				indexToSet = 6;//"PassedErrors";
			else if (hasWarnings)
				indexToSet = 5;//"PassedWarnings";

			while (node.Parent != null)
			{
				node = node.Parent;
				if (node.ImageIndex >= indexToSet)
					break;
				node.SelectedImageIndex = node.ImageIndex = indexToSet;
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

		private Font BuildFont(string name, float size, int style)
		{
			FontStyle fontStyle = FontStyle.Regular;
			
			int bold = style & 1;
			int italic = style & 2;
			int underline = style & 4;
			int strikeout = style & 8;

			if (bold > 0)
				fontStyle |= FontStyle.Bold;
			if (italic > 0)
				fontStyle |= FontStyle.Italic;
			if (underline > 0)
				fontStyle |= FontStyle.Underline;
			if (strikeout > 0)
				fontStyle |= FontStyle.Strikeout;

			return new Font(name, size, fontStyle);
		   
		}

		private static TreeNode CloneNode(TreeNode node)
		{
			TreeNode res = (TreeNode)node.Clone();
			res.Tag = node.Tag;
			
			return res;
		}

		
		
		private int counter;
		private string filename;
		private TreeNode lastCreatedNode;

		

		

		

		

		

		
		
		
	}

}