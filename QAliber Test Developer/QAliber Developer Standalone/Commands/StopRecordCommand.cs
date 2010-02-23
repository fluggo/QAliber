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
using QAliber.Recorder;
using System.Windows.Forms;

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
				Statics.SpyControl.notifyIcon.ShowBalloonTip(0);
				Statics.SpyControl.notifyIcon.Visible = false;
			}
		}

		
		private void EnterTextToActiveDocument(string text)
		{
			Statics.SpyControl.richTextBox.Text = text;
		}

	  
	}
}
