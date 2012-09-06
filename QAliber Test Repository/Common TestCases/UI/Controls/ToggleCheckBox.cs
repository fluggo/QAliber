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
		string _targetName = null;

		[Category("Control")]
		[DisplayName("Control")]
		[Description("The control must be of type HTMLInput or be a toggleable control of some kind.")]
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

		private ToggleAction _action = ToggleAction.Toggle;

		[Category("Control")]
		[DisplayName("Action")]
		[Description("Whether to turn the check box On, Off, or Toggle it.")]
		[DefaultValue(ToggleAction.Toggle)]
		public ToggleAction Action {
			get { return _action; }
			set { _action = value; OnDefaultNameChanged(); }
		}

		protected override string DefaultName {
			get {
				if( _targetName != null ) {
					string name;

					switch( _action ) {
						case ToggleAction.Off:
							name = "Uncheck";
							break;

						case ToggleAction.On:
							name = "Check";
							break;

						default:
						case ToggleAction.Toggle:
							name = "Toggle";
							break;
					}

					return string.Format( "{0} \"{1}\"", name, _targetName );
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
							Log.Error( "Tried three times, but couldn't set the checkbox to On." );
							return;
						}

						break;

					case ToggleAction.Off:
						for( int i = 0; i < 3; i++ ) {
							if( toggle.ToggleState != ToggleState.Off )
								toggle.Toggle();
						}

						if( toggle.ToggleState != ToggleState.Off ) {
							Log.Error( "Tried three times, but couldn't set the checkbox to Off." );
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
					Log.Error( "The target control only supports invoke operations. Try toggle action instead." );
					return;
				}

				switch( _action ) {
					case ToggleAction.On:
					case ToggleAction.Off:
						Log.Error( "The target control only supports invoke operations. Try toggle action instead." );
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
				Log.Error( "Couldn't find an appropriate way to toggle the control." );
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
