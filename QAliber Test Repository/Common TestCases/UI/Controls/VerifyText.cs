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
	[XmlType("VerifyText", Namespace=Util.XmlNamespace)]
	public class VerifyText : TestCase
	{
		public VerifyText() : base( "Verify Text" ) {
			Icon = Properties.Resources.Keyboard;
		}

		private string _control = "";

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("The control must be of type HTMLInput or be a toggleable control of some kind.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		public void ControlPropertyChanging( string value ) {
			if( string.IsNullOrWhiteSpace( _expectedText ) && _caseSensitive && !_useRegex ) {
				try {
					string text = GetText( value, false );

					if( text != null )
						_expectedText = text;
				}
				catch {
				}
			}
		}

		private string _expectedText = string.Empty;

		[Category("Behavior")]
		[DisplayName("Expected Text")]
		[Description("Text to expect in the control.")]
		[DefaultValue("")]
		public string ExpectedText {
			get { return _expectedText; }
			set { _expectedText = value; }
		}

		private bool _caseSensitive = true;

		[Category("Behavior")]
		[DisplayName("Case Sensitive")]
		[Description("Whether the match needs to be case sensitive.")]
		[DefaultValue(true)]
		public bool CaseSensitive {
			get { return _caseSensitive; }
			set { _caseSensitive = value; }
		}

		private bool _useRegex = false;

		[Category("Behavior")]
		[DisplayName("Use Regular Expressions")]
		[Description("Whether the Text field represents a regular expression.")]
		[DefaultValue(false)]
		public bool UseRegex {
			get { return _useRegex; }
			set { _useRegex = value; }
		}

		private string _foundText = string.Empty;

		[Category("Results")]
		[DisplayName("Found Text")]
		[Description("The text that was found in the control.")]
		[DefaultValue("")]
		[XmlIgnore]
		public string FoundText {
			get { return _foundText; }
			set { _foundText = value; }
		}

		private static string GetText( string control, bool logErrors ) {
			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( c == null || !c.Exists ) {
				if( logErrors )
					Log.Default.Error( "Control not found" );

				return null;
			}

			IText text = c.GetControlInterface<IText>();

			if( text == null ) {
				// TODO: Add support for HTML controls (or REALLY do generic implementations on the interfaces)
				if( logErrors )
					Log.Default.Error( "Couldn't find an appropriate way to get text from the control." );

				return null;
			}

			return text.Text;
		}

		public override void Body() {
			ActualResult = TestCaseResult.Failed;
			_foundText = string.Empty;
			Regex regex = null;

			if( _useRegex ) {
				regex = new Regex( _expectedText, RegexOptions.Singleline | (_caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase) );
			}

			_foundText = GetText( _control, true );

			if( _foundText == null )
				return;

			Log.Default.Info( "Control's text: \"" + _foundText + "\"" );

			if( regex != null ) {
				if( !regex.IsMatch( _foundText ) ) {
					ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					throw new ArgumentException( "The control's text didn't match the regular expression." );
				}
			}
			else if( _caseSensitive && _foundText != _expectedText ) {
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new ArgumentException( "The control's text didn't match the specified text." );
			}
			else if( !_caseSensitive && StringComparer.CurrentCultureIgnoreCase.Compare( _foundText, _expectedText ) != 0 ) {
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new ArgumentException( "The control's text didn't match the specified text." );
			}

			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Verifying text in control";
			}
		}

		public override object Clone() {
			VerifyText result = (VerifyText) base.Clone();

			result._foundText = string.Empty;

			return result;
		}
	}
}
