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
using QAliber.Logger;
using System.Threading;
using System.Xml.Serialization;
using QAliber.RemotingModel;

namespace QAliber.Repository.CommonTestCases.UI.Controls {
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("VerifyCheckBox", Namespace=Util.XmlNamespace)]
	public class VerifyCheckBox : TestCase
	{
		public VerifyCheckBox() : base( "Verify Check Box" )
		{
			Icon = Properties.Resources.CheckBox;
		}

		private string _control = "";
		string _targetName = null;

		[Category("Control")]
		[DisplayName("Control")]
		[Description("The control must be a toggleable control of some kind, such as a check box or radio button.")]
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

		private ToggleState _state = ToggleState.Off;

		[Category("Control")]
		[DisplayName("Expected Value")]
		[Description("Whether the check box or radio button should be On, Off, or Indeterminate.")]
		[DefaultValue(ToggleState.Off)]
		public ToggleState Action {
			get { return _state; }
			set { _state = value; OnDefaultNameChanged(); }
		}

		protected override string DefaultName {
			get {
				if( _targetName != null ) {
					string name;

					switch( _state ) {
						case ToggleState.Off:
							name = "off";
							break;

						case ToggleState.On:
							name = "on";
							break;

						default:
						case ToggleState.Indeterminate:
							name = "indeterminate";
							break;
					}

					return string.Format( "Verify \"{1}\" is {0}", name, _targetName );
				}

				return base.DefaultName;
			}
		}

		public override void Body() {
			ActualResult = TestCaseResult.Failed;

			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( !c.Exists ) {
				Log.Error( "Control not found" );
				return;
			}

			if( !c.Enabled ) {
				Log.Error( "Control not enabled" );
				return;
			}

			ITogglePattern toggle = c.GetControlInterface<ITogglePattern>();

			if( toggle != null ) {
				ToggleState currentState = toggle.ToggleState;

				if( currentState != _state ) {
					LogFailedByExpectedResult( "Did not match",
						string.Format( "The control's state was \"{0}\" instead of \"{1}\".",
							currentState, _state ) );
					return;
				}

				ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			}
			else {
				Log.Error( "The target control doesn't support toggle operations." );
				return;
			}
		}

		public override string Description {
			get {
				return "Verifying '" + _control + "' is " + _state.ToString();
			}
		}

	}
}
