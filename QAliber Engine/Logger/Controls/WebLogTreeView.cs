using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Xml;

namespace QAliber.Logger.Controls
{
	public class WebLogTreeView : TreeView
	{
		private string filename;
		private XmlDocument xmlDoc;

		//
		public WebLogTreeView()
		{
			iconLevels = new Dictionary<int, string>();
			iconLevels.Add(0, "~/Images/LogIcons/passed.bmp");
			iconLevels.Add(1, "~/Images/LogIcons/passedwarnings.bmp");
			iconLevels.Add(2, "~/Images/LogIcons/passederrors.bmp");
			iconLevels.Add(3, "~/Images/LogIcons/error.bmp"); 
		}

		public string LogURL
		{
			get { return filename; }
			set
			{
				if (value != null)
				{
					filename = value;
					FillTree();
				}
			}
		}

		private void FillTree()
		{
			xmlDoc = new XmlDocument();
			xmlDoc.Load(filename);
			BackColor = Color.White;
			Nodes.Clear();
			foreach (XmlNode node in xmlDoc.ChildNodes)
			{
				
				if (node.Name == "LogEntries")
				{
					FillTreeRec(Nodes, node, 0);
				}

			}
		}

		private void FillTreeRec(TreeNodeCollection tNodes, XmlNode xNode, int resLevel)
		{
			TreeNode newNode = null;
			LogEntry logEntry;
			foreach (XmlNode node in xNode.ChildNodes)
			{

				if (node.Name == "ChildEntries")
				{
					logEntry = GetEntry(node.FirstChild);
					if (logEntry != null)
					{


						newNode = new TreeNode();
						newNode.Text = logEntry.Message;
						newNode.ImageUrl = GetImageURLByEntry(logEntry);
						newNode.Target = logEntry.ExtendedMessage + ";" + logEntry.Link;
						tNodes.Add(newNode);

						currentTestCaseNodes.Add(newNode);
						node.RemoveChild(node.FirstChild);
						FillTreeRec(newNode.ChildNodes, node, resLevel + 1);

					}
				}
				else if (node.Name == "LogResult")
				{

					BubbleIconUp((TestCaseResult)Enum.Parse(typeof(TestCaseResult), node.InnerText));
					nodesStack.Pop();
					if (nodesStack.Count > 0)
						currentTestCaseNodes = nodesStack.Peek();
				}
				else if (node.Name == "TestCase")
				{

					List<TreeNode> newList = new List<TreeNode>();
					currentTestCaseNodes = newList;
					nodesStack.Push(newList);
				}
				else
				{
					logEntry = GetEntry(node);
					if (logEntry != null)
					{
						newNode = new TreeNode();
						newNode.Text = logEntry.Message;
						newNode.Target = logEntry.ExtendedMessage + ";" + logEntry.Link;
						newNode.ImageUrl = GetImageURLByEntry(logEntry);
						tNodes.Add(newNode);

						currentTestCaseNodes.Add(newNode);
					}
				}
			}
		}

		private void BubbleIconUp(TestCaseResult result)
		{
			int indexToSet = 0;//"Passed";
			if (result == TestCaseResult.Failed)
			{
				indexToSet = 3;//"Error";
				TreeNode firstNode = currentTestCaseNodes[0];
				do
				{
					if ( FindByValue(firstNode.ImageUrl) >= 3)
						break;
					firstNode.ImageUrl = iconLevels[3];
					firstNode = firstNode.Parent;
				} while (firstNode != null);
			}

			for (int i = 1; i < currentTestCaseNodes.Count; i++)
			{
				int curIndexToSet = indexToSet;
				TreeNode node = currentTestCaseNodes[i];

				if (node.ImageUrl == "~/Images/LogIcons/error.bmp")
					curIndexToSet = Math.Max(2, indexToSet);
				else if (node.ImageUrl == "~/Images/LogIcons/warning.bmp")
					curIndexToSet = 1;
				else
					curIndexToSet = 0;
				while (node.Parent != null)
				{
					node = node.Parent;
					if (FindByValue(node.ImageUrl) >= curIndexToSet)
						break;
					node.ImageUrl = iconLevels[curIndexToSet];
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
				}
			}
			catch { }
			return result;


		}

		private string GetImageURLByEntry(LogEntry entry)
		{
			if (entry.Body == BodyType.Link)
				return "~/Images/LogIcons/link.png";
			else if (entry.Body == BodyType.Picture)
				return "~/Images/LogIcons/picture.bmp";
			else
			{
				switch (entry.Type)
				{
					case EntryType.Event:
					case EntryType.Info:
						return "~/Images/LogIcons/info.bmp";
					case EntryType.Warning:
						return "~/Images/LogIcons/warning.bmp";
					case EntryType.Error:
						return "~/Images/LogIcons/error.bmp";
					default:
						return "~/Images/LogIcons/info.bmp";
				}
			}
		}

		private int FindByValue(string str)
		{
			switch (str)
			{
				case "~/Images/LogIcons/passed.bmp":
					return 0;
				case "~/Images/LogIcons/passedwarnings.bmp":
					return 1;
				case "~/Images/LogIcons/passederrors.bmp":
					return 2;
				case "~/Images/LogIcons/error.bmp":
					return 3;
				default:
					return -1;
			}
		}

		private Dictionary<int, string> iconLevels;
		private List<TreeNode> currentTestCaseNodes;
		private Stack<List<TreeNode>> nodesStack = new Stack<List<TreeNode>>();
	}
}
