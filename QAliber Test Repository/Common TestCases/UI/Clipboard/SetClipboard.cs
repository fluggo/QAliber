using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QAliber.TestModel;
using System.ComponentModel;
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Patterns;
using System.Windows.Automation;
using QAliber.Logger;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using QAliber.Engine;
using System.Reflection;
using System.Xml.Serialization;
using System.Data;
using System.Globalization;

namespace QAliber.Repository.CommonTestCases.UI.Clipboard {
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Clipboard")]
	[XmlType("SetClipboard", Namespace=Util.XmlNamespace)]
	public class SetClipboard : TestCase
	{
		public SetClipboard() : base( "Set text on clipboard" )
		{
			Icon = Properties.Resources.Clipboard;
		}

		private string _text = string.Empty;

		[Category("Behavior")]
		[DisplayName("Text")]
		[Description("Text to set on the clipboard.")]
		[DefaultValue("")]
		public string Text {
			get { return _text; }
			set {
				OnDefaultNameChanged();
				_text = value;
			}
		}

		protected override string DefaultName {
			get {
				if( string.IsNullOrEmpty( _text ) )
					return base.DefaultName;

				return string.Format( "Put \"{0}\" on clipboard", _text );
			}
		}

		public override void Body( TestRun run ) {
			ActualResult = TestCaseResult.Failed;

			System.Windows.Forms.Clipboard.SetText( _text );

			ActualResult = TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Setting text on clipboard";
			}
		}
	}
}
