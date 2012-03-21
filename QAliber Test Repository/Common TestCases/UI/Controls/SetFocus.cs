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
using QAliber.RemotingModel;

namespace QAliber.Repository.CommonTestCases.UI.Controls {
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("SetFocus", Namespace=Util.XmlNamespace)]
	public class SetFocus : TestCase
	{
		public SetFocus() : base( "Set Focus" )
		{
			Icon = Properties.Resources.Keyboard;
		}

		private string _control = "";

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("Control to set focus on.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		public override void Body() {
			ActualResult = TestCaseResult.Failed;

			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( c == null || !c.Exists ) {
				Log.Default.Error( "Control not found" );
				return;
			}

			c.SetFocus();
			ActualResult = TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Setting focus";
			}
		}

	}
}
