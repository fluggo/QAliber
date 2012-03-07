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
using System.ComponentModel;
using System.Xml.Serialization;
using QAliber.Logger;
using System.Drawing;
using QAliber.TestModel.Attributes;
using QAliber.Recorder.MacroRecorder;
using QAliber.TestModel;

namespace QAliber.Repository.CommonTestCases.Macros
{
	/// <summary>
	/// Play a macro (low-level recording) file recroded by QAliber
	/// </summary>
	[Serializable]
	[VisualPath(@"Macros")]
	[XmlType(TypeName="PlayMacro", Namespace=Util.XmlNamespace)]
	public class PlayMacroTestCase : TestCase
	{
		public PlayMacroTestCase() : base( "Play Macro" )
		{
			Icon = Properties.Resources.Macro;
		}

		private string filename = string.Empty;

		/// <summary>
		/// The name of the stored macro file
		/// </summary>
		[Category("Macro")]
		[DisplayName("Macro File")]
		[Description("A file representing a macro recording")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		public override void Body()
		{
			MacroRecorder recorder = new MacroRecorder();
			recorder.Load(filename);
			recorder.Play();
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Playing macro '" + filename + "'";
			}
		}
	
	
	}
	
}
