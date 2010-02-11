using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;
using QAliber.Builder.Presentation.Commands;
using QAliber.RemotingModel;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using Darwen.Windows.Forms.Controls.TabbedDocuments;

namespace QAliber.Builder.Presentation
{
	public partial class ScenarioControl : UserControl
	{
		public ScenarioControl()
		{
			InitializeComponent();
		}

		public TestScenario TestScenario
		{
			get { return testScenario; }
		}

		public void FillTree(TestScenario scenario)
		{
			scenarioTreeView.EnableComplexCheck = false;
			QAliberTreeNode node = new QAliberTreeNode(scenario.RootTestCase);
			SetIconToNode(node, TestCaseResult.None);
			node.ContextMenuStrip = testCasesMenu;
			scenarioTreeView.Nodes.Add(node);
			FillTreeRecursively(node);
			scenarioTreeView.EnableComplexCheck = true;
			testScenario = scenario;
		}

		public static event EventHandler<ScenarioChangedEventArgs> ScenarioChanged;

		protected void OnScenarioChanged()
		{
			if (ScenarioChanged != null)
				ScenarioChanged(this, new ScenarioChangedEventArgs(testScenario));
		}

		internal void AddTestCaseAfterCurrentNode(TestCase testcase)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				AfterTypeDragEnded(null, new NodeDraggedEventArgs(new QAliberTreeNode(testcase), (QAliberTreeNode)scenarioTreeView.SelectedNode));
			}
		}

		internal void SelectNodeByID(int id)
		{
			TreeNode node = GetNodeByID((QAliberTreeNode)scenarioTreeView.Nodes[0], id);
			if (node != null)
			{
				node.EnsureVisible();
				scenarioTreeView.SelectedNode = node;
			}
		}

		internal void CheckNodeByColor(Color color, bool check)
		{
			CheckNodeByColorRecursively((QAliberTreeNode)scenarioTreeView.Nodes[0], color, check);
		}

		private void FillTreeRecursively(QAliberTreeNode node)
		{
			FolderTestCase folderCase = node.Testcase as FolderTestCase;
			if (folderCase != null)
			{
				foreach (TestCase child in folderCase.Children)
				{
					QAliberTreeNode childNode = AddNode(node, child);
					FillTreeRecursively(childNode);
				}
			}
		}

		private void CheckNodeByColorRecursively(QAliberTreeNode node, Color color, bool check)
		{
			foreach (QAliberTreeNode child in node.Nodes)
			{
				if (child.Testcase.Color == color)
				{
					child.Checked = check;
				}
				CheckNodeByColorRecursively(child, color, check);
			}
		}

		private TreeNode GetNodeByID(QAliberTreeNode node, int id)
		{
			TestCase testcase = node.Testcase as TestCase;
			if (testcase != null)
			{
				if (testcase.ID == id)
					return node;
				else
				{
					foreach (QAliberTreeNode child in node.Nodes)
					{
						TreeNode res = GetNodeByID(child, id);
						if (res != null)
							return res;
					}
				}
			   
			}
			return null;

		}

		//private void SetNodeIDsRec(QAliberTreeNode node)
		//{
		//	  TestCase testcase = node.Testcase as TestCase;
		//	  if (testcase != null)
		//	  {
		//		  string tcTypeName = testcase.GetType().FullName;
		//		  if (testcaseIconsList.Images.ContainsKey(tcTypeName))
		//			  node.ImageKey = node.SelectedImageKey = tcTypeName;
		//		  testcase.ID = nextID;
		//		  nextID++;
		//		  foreach (QAliberTreeNode child in node.Nodes)
		//		  {
		//			  SetNodeIDsRec(child);
		//		  }
		//	  }

		//}

		private QAliberTreeNode AddNode(QAliberTreeNode node, TestCase testCase)
		{
			QAliberTreeNode child = node.LinkChild(testCase);
			SetIconToNode(child, TestCaseResult.None);
			child.ContextMenuStrip = testCasesMenu;

			return child;
		}

		private void UpdateContextMenusAndIconsRec(QAliberTreeNode node)
		{
			string tcTypeName = node.Testcase.GetType().FullName;
			if (!testcaseIconsList.Images.ContainsKey(tcTypeName))
			{
				if (node.Testcase.Icon != null)
					testcaseIconsList.Images.Add(tcTypeName, node.Testcase.Icon);
			}
			if (testcaseIconsList.Images.ContainsKey(tcTypeName))
				node.ImageKey = node.SelectedImageKey = tcTypeName;
			node.ContextMenuStrip = testCasesMenu;
			foreach (TreeNode child in node.Nodes)
			{
				UpdateContextMenusAndIconsRec((QAliberTreeNode)child);
			}
		}

		private TestCase[] SelectedTestCases
		{
			get
			{
				List<TestCase> res = new List<TestCase>();
				foreach (QAliberTreeNode node in scenarioTreeView.SelectedNodes)
				{
					TestCase t = node.Testcase as TestCase;
					if (t != null)
						res.Add(t);
				}
				return res.ToArray();
			}
		}

		private void SetIconToNode(QAliberTreeNode node, TestCaseResult result)
		{
			if (node != null)
			{
				if (node.Testcase != null)
				{
					string type = node.Testcase.GetType().FullName;
					switch (result)
					{
						case TestCaseResult.None:
							if (testcaseIconsList.Images.ContainsKey(type))
							{
								node.SelectedImageKey = node.ImageKey = type;
							}
							else
							{
								if (node.Testcase.Icon == null)
									node.SelectedImageKey = node.ImageKey = "Generic";
								else
								{
									testcaseIconsList.Images.Add(type, node.Testcase.Icon);
									node.SelectedImageKey = node.ImageKey = type;
								}
							}
							return;
						case TestCaseResult.Passed:
						case TestCaseResult.Failed:
							string pfType = type + "---" + result.ToString();
							if (!testcaseIconsList.Images.ContainsKey(pfType))
							{
								Bitmap bitmap = new Bitmap(16, 16);
								using (Graphics graphics = Graphics.FromImage(bitmap))
								{
									if (!testcaseIconsList.Images.ContainsKey(type))
										type = "Generic";
									graphics.DrawImage(testcaseIconsList.Images[type], 0, 0);
									if (result == TestCaseResult.Failed)
										graphics.DrawImage(Properties.Resources.Error, 7, 0);
									else
										graphics.DrawImage(Properties.Resources.Pass, 7, 0);
									testcaseIconsList.Images.Add(pfType, bitmap);
								}
							}
							node.SelectedImageKey = node.ImageKey = pfType;
							break;
						default:
							break;
					}
					
				}
			}
		}

		private void SetIconToNode(QAliberTreeNode node, ExecutionState state)
		{
			if (node != null)
			{
				if (node.Testcase != null)
				{
					switch (state)
					{
						case ExecutionState.NotExecuted:
							break;
						case ExecutionState.Paused:
							node.SelectedImageKey = node.ImageKey = "InBreakpoint";
							break;
						case ExecutionState.InProgress:
							node.SelectedImageKey = node.ImageKey = "InProgress";
							break;
						case ExecutionState.Executed:
							break;
						default:
							break;
					}
				}
			}
		}

		#region Events
		private void AfterTreeNodeSelected(object sender, TreeViewEventArgs e)
		{
			testCasesPG.SelectedObjects = SelectedTestCases;
		}

		private void AfterDragEnded(object sender, NodeDraggedEventArgs e)
		{
			FolderTestCase testcase = e.TargetNode.Testcase as FolderTestCase;
			if (testcase != null)
			{
				ICommand command = new CutCommand(new QAliberTreeNode[] { (QAliberTreeNode)e.SourceNode });
				command.Do();
				command = new PasteCommand(e.TargetNode);
				commandsHistory.Do(command);
				OnScenarioChanged();
			}
			
		}

		internal void AfterTypeDragEnded(object sender, NodeDraggedEventArgs e)
		{
			QAliberTreeNode node = e.SourceNode.Clone() as QAliberTreeNode;
			TreeClipboard.Default.StoreInClipboard(new QAliberTreeNode[] { node }, false);
			if (node != null)
			{
				if (node.Testcase != null)
				{
					string tcTypeName = node.Testcase.GetType().FullName;
					node.Checked = node.Testcase.MarkedForExecution;
					if (!testcaseIconsList.Images.ContainsKey(tcTypeName))
					{
						if (node.Testcase.Icon != null)
							testcaseIconsList.Images.Add(tcTypeName, node.Testcase.Icon);
					}
					if (testcaseIconsList.Images.ContainsKey(tcTypeName))
						node.ImageKey = node.SelectedImageKey = tcTypeName;
					node.ContextMenuStrip = testCasesMenu;

					ICommand command = new PasteCommand(e.TargetNode, true);
					commandsHistory.Do(command);
					OnScenarioChanged();
				}
			}
		}

		private void AfterCheckChanged(object sender, TreeViewEventArgs e)
		{
			TestCase testcase = ((QAliberTreeNode)e.Node).Testcase;
			if (testcase != null)
			{
				testcase.MarkedForExecution = e.Node.Checked;
				OnScenarioChanged();
			}
		}

		private void AfterValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			//if (e.OldValue != null)
			//{
			//	  ICommand command = new PropertyChangedCommand(scenarioTreeView.SelectedNode,
			//		  testCasesPG,
			//		  e.ChangedItem.PropertyDescriptor,
			//		  e.OldValue,
			//		  e.ChangedItem.Value);
			//	  CommandsCollection.Default.AddNewCommand(command);

			//}
			if (e.ChangedItem.PropertyDescriptor.Name == "Name")
			{
				foreach (TreeNode node in scenarioTreeView.SelectedNodes)
				{
					node.Text = e.ChangedItem.Value.ToString();
				}

			}
						
			OnScenarioChanged();
		}

		internal void MenuCutClicked(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				ICommand command = new CutCommand(scenarioTreeView.SelectedNodes);
				command.Do();
			}
		}

		internal void MenuCopyClicked(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				ICommand command = new CopyCommand(scenarioTreeView.SelectedNodes);
				command.Do();

			}
		}

		internal void MenuPasteClicked(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null && TreeClipboard.Default.Nodes != null)
			{
				ICommand command = new PasteCommand((QAliberTreeNode)scenarioTreeView.SelectedNode);
				commandsHistory.Do(command);
				//Go through target recursively nodes and update context menu and icons
				foreach (QAliberTreeNode node in ((PasteCommand)command).insCommand.targetNodes)
				{
					UpdateContextMenusAndIconsRec(node);
				}
				OnScenarioChanged();
			}
		}

		internal void MenuDeleteClicked(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				ICommand command = new RemoveCommand(scenarioTreeView.SelectedNodes);
				commandsHistory.Do(command);
				if (scenarioTreeView.SelectedNode != null)
					scenarioTreeView.SelectedNodes = new QAliberTreeNode[] { (QAliberTreeNode)scenarioTreeView.SelectedNode };
				else
					scenarioTreeView.SelectedNodes = null;
				OnScenarioChanged();
			}
		}

		internal void MenuMoveUpClicked(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				ICommand command = new MoveUpCommand((QAliberTreeNode)scenarioTreeView.SelectedNode);
				commandsHistory.Do(command);
				OnScenarioChanged();
			}
		}

		internal void MenuMoveDownClicked(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				ICommand command = new MoveDownCommand((QAliberTreeNode)scenarioTreeView.SelectedNode);
				commandsHistory.Do(command);
				OnScenarioChanged();
			}
		}

		internal void MenuUndoClicked(object sender, EventArgs e)
		{
			commandsHistory.Undo();
			OnScenarioChanged();
		}

		internal void MenuRedoClicked(object sender, EventArgs e)
		{
			commandsHistory.Redo();
			OnScenarioChanged();
		}

		internal void OnStepStarted(int id)
		{
			TreeNode node = GetNodeByID((QAliberTreeNode)scenarioTreeView.Nodes[0], id);
			if (node != null)
			{
				playingNodes.Push(node);
				node.EnsureVisible();
				scenarioTreeView.SelectedNode = node;
				SetIconToNode((QAliberTreeNode)node, ExecutionState.InProgress);
			}
		}

		internal void OnStepResultArrived(TestCaseResult result)
		{
			if (playingNodes.Count > 0)
			{
				TreeNode currentTreeNode = playingNodes.Pop();
				currentTreeNode.EnsureVisible();
				scenarioTreeView.SelectedNode = currentTreeNode;
				if (result == TestCaseResult.None)
					result = TestCaseResult.Passed;
				SetIconToNode((QAliberTreeNode)currentTreeNode, result);
			}
		}

		internal void OnBreakPointReached()
		{
			if (playingNodes.Count > 0)
			{
				TreeNode currentTreeNode = playingNodes.Peek();
				currentTreeNode.EnsureVisible();
				scenarioTreeView.SelectedNode = currentTreeNode;
				SetIconToNode((QAliberTreeNode)currentTreeNode, ExecutionState.Paused);
			}
		}

		internal void SetBPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				TestCase testcase = ((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase;
				if (testcase != null)
				{
					if (!testcase.HasBreakPoint)
					{
						testcase.HasBreakPoint = true;
						((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = scenarioTreeView.SelectedNode.BackColor = Color.Red;
						((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = scenarioTreeView.SelectedNode.ForeColor = Color.White;
					}
					else
					{
						testcase.HasBreakPoint = false;
						((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = scenarioTreeView.SelectedNode.BackColor = Color.White;
						((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = scenarioTreeView.SelectedNode.ForeColor = testcase.Color;
					}
					scenarioTreeView.SelectedNode = scenarioTreeView.SelectedNode.NextVisibleNode;
				}
			}
		}

		private void playCurrentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null && scenarioTreeView.SelectedNode is QAliberTreeNode)
			{
			   
				//SetNodeIDs();
				TestController.Default.Run(((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase);
			}
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				TestCase testcase = ((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase;
				if (testcase != null)
				{
					//TODO : Find a better way to show help (server side ?)
					try
					{
						string xmlFile = "Help/" + testcase.GetType().Assembly.FullName.Split(',')[0] + ".xml";
						string baseFileDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
						string fullPath = Path.Combine(baseFileDir, xmlFile);
						string typeName = testcase.GetType().FullName.Substring(testcase.GetType().FullName.IndexOf('.') + 1);
						Program.AddStylesheetDeclaration(fullPath);
						Process.Start("file://" + fullPath + "#" + typeName);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Could not show help.\n" + ex.Message, "Error Loading Help", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

			}
		}

		private void toolStripBlue_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				scenarioTreeView.SelectedNode.ForeColor = Color.Blue;
				scenarioTreeView.SelectedNode.BackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = Color.Blue;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase.Color = Color.Blue;
			}
		}

		private void toolStripPurple_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				scenarioTreeView.SelectedNode.ForeColor = Color.Purple;
				scenarioTreeView.SelectedNode.BackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = Color.Purple;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase.Color = Color.Purple;
			}
		}

		private void toolStripGreen_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				scenarioTreeView.SelectedNode.ForeColor = Color.Green;
				scenarioTreeView.SelectedNode.BackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = Color.Green;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase.Color = Color.Green;
			}
		}

		private void toolStripOrange_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				scenarioTreeView.SelectedNode.ForeColor = Color.Orange;
				scenarioTreeView.SelectedNode.BackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = Color.Orange;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase.Color = Color.Orange;
			}
		}

		private void toolStripRed_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				scenarioTreeView.SelectedNode.ForeColor = Color.Red;
				scenarioTreeView.SelectedNode.BackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = Color.Red;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase.Color = Color.Red;
			}
		}

		private void noneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (scenarioTreeView.SelectedNode != null)
			{
				scenarioTreeView.SelectedNode.ForeColor = Color.Black;
				scenarioTreeView.SelectedNode.BackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightForeColor = Color.Black;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).NonHighlightBackColor = Color.White;
				((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase.Color = Color.Black;
			}
		}

		private void variablesWizardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (testCasesPG.SelectedGridItem != null &&
				testCasesPG.SelectedGridItem.PropertyDescriptor.PropertyType.Equals(typeof(string)))
			{
				 try
				 {
					 QAliber.TestModel.TypeEditors.VariablesWizardForm form = new QAliber.TestModel.TypeEditors.VariablesWizardForm(testScenario, (string)testCasesPG.SelectedGridItem.Value);
					 if (form.ShowDialog() == DialogResult.OK)
					 {
						 testCasesPG.SelectedGridItem.PropertyDescriptor.SetValue(
						  ((QAliberTreeNode)scenarioTreeView.SelectedNode).Testcase, form.OutputString);
						 testCasesPG.Refresh();
					 }
				 }
				 catch (Exception ex)
				 {
					 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				 }
				
			}

		}

		#endregion


		private Stack<TreeNode> playingNodes = new Stack<TreeNode>();
		private CommandsCollection commandsHistory = new CommandsCollection();
		private TestScenario testScenario;

	}

	public class ScenarioChangedEventArgs : EventArgs
	{
		public ScenarioChangedEventArgs (TestScenario scenario)
		{
			this.scenario = scenario;
		}

		private TestScenario scenario;

		public TestScenario TestScenario
		{
			get { return scenario;}
			set { scenario = value;}
		}
	}
}
