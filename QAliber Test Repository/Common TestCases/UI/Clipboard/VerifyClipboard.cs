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
	[XmlType("VerifyClipboard", Namespace=Util.XmlNamespace)]
	public class VerifyClipboard : TestCase
	{
		public VerifyClipboard() : base( "Verify text on clipboard" )
		{
			Icon = Properties.Resources.ClipboardVerify;
		}

		private string _expectedText = string.Empty;

		[Category("Behavior")]
		[DisplayName("Expected Text")]
		[Description("Text to expect on the clipboard.")]
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
		[Description("Whether the ExpectedText field represents a regular expression.")]
		[DefaultValue(false)]
		public bool UseRegex {
			get { return _useRegex; }
			set { _useRegex = value; }
		}

		private string _foundText = string.Empty;

		[Category("Results")]
		[DisplayName("Found Text")]
		[Description("The text that was found on the clipboard.")]
		[DefaultValue("")]
		[XmlIgnore]
		public string FoundText {
			get { return _foundText; }
			set { _foundText = value; }
		}

		private static string GetText( bool logErrors ) {
			if( !System.Windows.Forms.Clipboard.ContainsText() ) {
				if( logErrors )
					Log.Error( "The clipboard does not have text on it." );

				return null;
			}

			return System.Windows.Forms.Clipboard.GetText();
		}

		public override void Body( TestRun run ) {
			ActualResult = TestCaseResult.Failed;

			_foundText = GetText( true );

			if( _foundText == null )
				return;

			Log.Info( "Found text", _foundText );

			if( _useRegex ) {
				RegexOptions options = RegexOptions.Singleline;

				if( !_caseSensitive )
					options |= RegexOptions.IgnoreCase;

				if( !Regex.IsMatch( _foundText, _expectedText, options ) ) {
					LogFailedByExpectedResult( "Did not match regex",
						string.Format( "The control's text didn't match the regular expression. Expected \"{0}\", but saw \"{1}\".",
							_expectedText, _foundText ) );
					return;
				}
			}
			else if( _caseSensitive && !StringComparer.CurrentCulture.Equals( _foundText, _expectedText ) ) {
				LogFailedByExpectedResult( "Did not match",
					string.Format( "The control's text didn't match in a case-sensitive comparison. Expected \"{0}\", but saw \"{1}\".",
						_expectedText, _foundText ) );
				return;
			}
			else if( !_caseSensitive && !StringComparer.CurrentCultureIgnoreCase.Equals( _foundText, _expectedText ) ) {
				LogFailedByExpectedResult( "Did not match",
					string.Format( "The control's text didn't match in a case-insensitive comparison. Expected \"{0}\", but saw \"{1}\".",
						_expectedText, _foundText ) );
				return;
			}

			ActualResult = TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Verifying text on clipboard";
			}
		}

		public override object Clone() {
			VerifyClipboard result = (VerifyClipboard) base.Clone();

			result._foundText = string.Empty;

			return result;
		}
	}
}
