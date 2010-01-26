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
	/// Play a macro file recrded by QAliber
	/// </summary>
	[Serializable]
	[VisualPath(@"Macros")]
	public class PlayMacroTestCase : TestCase
	{
		public PlayMacroTestCase()
		{
			name = "Play Macro";
			icon = Properties.Resources.Macro;
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
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Playing macro '" + filename + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	}
	
}
