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

		private int _timeout = 1000;

		[Category("Behavior")]
		[DisplayName("Timeout")]
		[Description("The timeout in milliseconds to wait for the control.")]
		[DefaultValue(1000)]
		public int Timeout {
			get { return _timeout; }
			set { _timeout = value; }
		}

		protected override string DefaultName {
			get {
				if( _targetName == null )
					return base.DefaultName;

				return string.Format( "Set focus on \"{0}\"", _targetName );
			}
		}

		public override void Body( TestRun run ) {
			ActualResult = TestCaseResult.Failed;

			Stopwatch watch = new Stopwatch();
			string lastException = null;
			UIControlBase c = null;

			watch.Start();
			while (watch.ElapsedMilliseconds < _timeout + 10)
			{
				try
				{
					c = UIControlBase.FindControlByPath( _control );

					if( c.Exists )
						break;
				}
				catch( Exception ex ) {
					lastException = ex.ToString();
				}
			}

			if( c == null || !c.Exists ) {
				Log.Error( "Control not found after " + _timeout + " milliseconds", _control );

				if( lastException != null ) {
					Log.Warning( "Exception caught", lastException, EntryVerbosity.Debug );
				}

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
