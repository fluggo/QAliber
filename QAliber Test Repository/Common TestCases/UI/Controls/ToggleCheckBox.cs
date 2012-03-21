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
	public enum ToggleAction {
		Off,
		On,
		Toggle
	}

	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("SetCheckBox", Namespace=Util.XmlNamespace)]
	public class SetCheckBox : TestCase
	{
		public SetCheckBox() : base( "Set Check Box" )
		{
			Icon = Properties.Resources.CheckBox;
		}

		private string _control = "";


		[Category("Control")]
		[DisplayName("Control")]
		[Description("The control must be of type HTMLInput or be a toggleable control of some kind.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		private ToggleAction _action = ToggleAction.Toggle;

		[Category("Control")]
		[DisplayName("Action")]
		[Description("Whether to turn the check box On, Off, or Toggle it.")]
		[DefaultValue(ToggleAction.Toggle)]
		public ToggleAction Action {
			get { return _action; }
			set { _action = value; }
		}


		public override void Body() {
			ActualResult = TestCaseResult.Failed;

			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( !c.Exists ) {
				Log.Default.Error( "Control not found" );
				return;
			}

			if( !c.Enabled ) {
				Log.Default.Error( "Control not enabled" );
				return;
			}

			ITogglePattern toggle = c.GetControlInterface<ITogglePattern>();
			IInvokePattern invoke = c.GetControlInterface<IInvokePattern>();

			HTMLInput input = c as HTMLInput;

			if( toggle != null ) {
				switch( _action ) {
					case ToggleAction.On:
						for( int i = 0; i < 3; i++ ) {
							if( toggle.ToggleState != ToggleState.On )
								toggle.Toggle();
						}

						if( toggle.ToggleState != ToggleState.On ) {
							Log.Default.Error( "Tried three times, but couldn't set the checkbox to On." );
							return;
						}

						break;

					case ToggleAction.Off:
						for( int i = 0; i < 3; i++ ) {
							if( toggle.ToggleState != ToggleState.Off )
								toggle.Toggle();
						}

						if( toggle.ToggleState != ToggleState.Off ) {
							Log.Default.Error( "Tried three times, but couldn't set the checkbox to Off." );
							return;
						}

						break;

					case ToggleAction.Toggle:
						toggle.Toggle();
						break;
				}

				ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			}
			else if( invoke != null ) {
				if( _action != ToggleAction.Toggle ) {
					Log.Default.Error( "The target control only supports invoke operations. Try toggle action instead." );
					return;
				}

				switch( _action ) {
					case ToggleAction.On:
					case ToggleAction.Off:
						Log.Default.Error( "The target control only supports invoke operations. Try toggle action instead." );
						return;

					case ToggleAction.Toggle:
						invoke.Invoke();
						break;
				}

				ActualResult = TestCaseResult.Passed;
			}
			else if( input != null ) {
				switch( _action ) {
					case ToggleAction.Toggle:
						input.Click();
						break;

					case ToggleAction.On:
						if( !input.Checked )
							input.Click();

						break;

					case ToggleAction.Off:
						if( input.Checked )
							input.Click();

						break;
				}

				ActualResult = TestCaseResult.Passed;
			}
			else {
				Log.Default.Error( "Couldn't find an appropriate way to toggle the control." );
				ActualResult = TestCaseResult.Failed;
			}
		}

		public override string Description {
			get {
				return "Setting control '" + _control + "' to " + _action.ToString();
			}
		}

	}
}
