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
		public SetFocus() : base( "Set focus" )
		{
			Icon = Properties.Resources.Keyboard;
		}

		private string _control = "";
		string _targetName = null;

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("Control to set focus on.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control {
			get { return _control; }
			set {
				try {
					_targetName = Util.GetControlNameFromXPath( value );
				}
				catch {
					_targetName = null;
				}

				OnDefaultNameChanged();
				_control = value;
			}
		}

		protected override string DefaultName {
			get {
				if( _targetName == null )
					return base.DefaultName;

				return string.Format( "Set focus on \"{0}\"", _targetName );
			}
		}

		public override void Body() {
			ActualResult = TestCaseResult.Failed;

			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( c == null || !c.Exists ) {
				Log.Default.Error( "Control not found" );
				return;
			}

			UIAControl uia = c as UIAControl;

			if( uia != null && uia.XPathElementName == "titlebar" ) {
				// Go with the parent
				c = uia.Parent;
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
