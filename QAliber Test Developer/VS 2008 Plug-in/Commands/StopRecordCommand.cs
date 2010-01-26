using System;
using System.Collections.Generic;
using System.Text;
using QAliber.Recorder;
using System.Windows.Forms;
using EnvDTE;

namespace QAliber.VS2005.Plugin.Commands
{
	public class StopRecordCommand : Command
	{

		public override void Invoke()
		{
			string text = null;
			try
			{
				OnInvoke();
				Statics.Recorder.Stop();
				LLRecordsAnalyzer analyzer = new LLRecordsAnalyzer(Statics.Recorder);
				analyzer.Analyze();
				RecordsDisplayer disp = new RecordsDisplayer(analyzer.AnalyzedLLEntries);
				disp.ReplaceVars();
				if (Statics.Language == ProjectLanguage.VB)
				{
					text = disp.PrintVBCode();
				}
				else
					text = disp.PrintCSharpCode();
				EnterTextToActiveDocument(text);

			}
			catch (Exception ex)
			{
				if (text != null)
					Clipboard.SetText(text);
				MessageBox.Show(ex.Message + "\nGenerated code was copied to clipboard", "Error while stopping", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				spyToolWin.control.notifyIcon.ShowBalloonTip(0);
				spyToolWin.control.notifyIcon.Visible = false;
				Statics.DTE.MainWindow.WindowState = vsWindowState.vsWindowStateNormal;
			}
		}

		public SpyToolWindow SpyToolWin
		{
			get { return spyToolWin; }
			set { spyToolWin = value; }
		}

		private void EnterTextToActiveDocument(string text)
		{
			if (Statics.DTE.ActiveDocument == null)
			{
				MessageBox.Show("No active document is visible to write the generated code in, copying to clipboard instead (press ctrl-v on the document you want to place the code into)", "No Active Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Clipboard.Clear();
				if (!string.IsNullOrEmpty(text))
					Clipboard.SetText(text);
				return;
			}
			TextSelection sel = (TextSelection)Statics.DTE.ActiveDocument.Selection;
			TextRanges dummy = null;
			if (Statics.Language == ProjectLanguage.CSharp)
			{
			   
				bool isRegionExists = false;

				sel.StartOfDocument(true);
				if (sel.FindPattern("#region Temporary Recording", (int)vsFindOptions.vsFindOptionsMatchInHiddenText, ref dummy))
					isRegionExists = true;

				sel.EndOfDocument(true);

				sel.FindPattern("}", (int)(vsFindOptions.vsFindOptionsBackwards | vsFindOptions.vsFindOptionsMatchInHiddenText), ref dummy);
				sel.FindPattern("}", (int)(vsFindOptions.vsFindOptionsBackwards | vsFindOptions.vsFindOptionsMatchInHiddenText), ref dummy);
				if (isRegionExists)
					sel.FindPattern("#endregion", (int)(vsFindOptions.vsFindOptionsBackwards | vsFindOptions.vsFindOptionsMatchInHiddenText), ref dummy);
				sel.LineUp(true, 1);
				if (!isRegionExists)
					sel.Insert("\t\t#region Temporary Recording Code\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert("\t\tprivate void " + GetAvailableMethodName() + "()\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert("\t\t{\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert(text, (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert("\r\n\t\t}\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				if (!isRegionExists)
				{
					sel.Insert("\t\t#endregion\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
					sel.Insert("\t}\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				}

				
			}
			else if (Statics.Language == ProjectLanguage.VB)
			{
				
				sel.EndOfDocument(true);

				sel.FindPattern("End Class", (int)(vsFindOptions.vsFindOptionsBackwards | vsFindOptions.vsFindOptionsMatchInHiddenText), ref dummy);
				//sel.LineUp(true, 1);
				sel.Insert("\tPrivate Sub " + GetAvailableMethodName() + "()\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert(text, (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert("\tEnd Sub\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
				sel.Insert("End Class\r\n", (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
			}


			//sel.SelectAll();
			Statics.DTE.ActiveDocument.Activate();
			System.Threading.Thread.Sleep(200);
			Statics.DTE.ExecuteCommand("Edit.FormatDocument", string.Empty); 

		}

		private string GetAvailableMethodName()
		{
			string res = "RecordedTest";
			List<int> usedIndices = new List<int>();
			try
			{
				foreach (CodeElement element in Statics.DTE.ActiveDocument.ProjectItem.FileCodeModel.CodeElements)
				{
					if (element.Kind == vsCMElement.vsCMElementNamespace)
					{
						foreach (CodeElement nsChildElement in element.Children)
						{
							if (nsChildElement.Kind == vsCMElement.vsCMElementClass)
							{
								usedIndices = LookInClass(nsChildElement);
								break;
							}
						}
					}
					else if (element.Kind == vsCMElement.vsCMElementClass)
					{
						usedIndices = LookInClass(element);
						break;
					}

				}
				int i = 1;
				while (usedIndices.Contains(i))
				{
					i++;
				}
				res += i.ToString();
			}
			catch
			{
			}
			return res;
		}

		private List<int> LookInClass(CodeElement classElement)
		{
			List<int> usedIndices = new List<int>();
			foreach (CodeElement classChildElement in classElement.Children)
			{
				if (classChildElement.Kind == vsCMElement.vsCMElementFunction)
				{
					if (classChildElement.Name.StartsWith("RecordedTest"))
					{
						int index = 0;
						int.TryParse(classChildElement.Name.Substring(12), out index);
						usedIndices.Add(index);
					}

				}
			}
			return usedIndices;
		}

		protected SpyToolWindow spyToolWin;

	}
}
