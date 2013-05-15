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
	[XmlType("ClearClipboard", Namespace=Util.XmlNamespace)]
	public class ClearClipboard : TestCase
	{
		public ClearClipboard() : base( "Clear clipboard" )
		{
			Icon = Properties.Resources.ClipboardClear;
		}

		public override void Body( TestRun run ) {
			ActualResult = TestCaseResult.Failed;

			System.Windows.Forms.Clipboard.Clear();

			ActualResult = TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Clearing clipboard";
			}
		}
	}
}
