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
using System.Globalization;

namespace QAliber.Repository.CommonTestCases.UI.Controls {
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("VerifyPropertyFromControl", Namespace=Util.XmlNamespace)]
	public class VerifyPropertyFromControl : TestCase
	{
		public VerifyPropertyFromControl() : base( "Verify Property from Control" ) {
			Icon = Properties.Resources.Window;
		}

		private string _control = string.Empty;

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("The path to the control to test.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		private string _property = string.Empty;

		[Category("Behavior")]
		[DisplayName("Property")]
		[Description("Name of the property to retrieve.")]
		[DefaultValue("")]
		[TypeConverter(typeof(StandardPropertiesListConverter))]
		public string Property {
			get { return _property; }
			set { _property = value; }
		}

		private string _expectedValue = string.Empty;

		[Category("Behavior")]
		[DisplayName("Expected Value")]
		[Description("Value to expect from the property.")]
		[DefaultValue("")]
		public string ExpectedValue {
			get { return _expectedValue; }
			set { _expectedValue = value; }
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
		[Description("Whether the Value field represents a regular expression.")]
		[DefaultValue(false)]
		public bool UseRegex {
			get { return _useRegex; }
			set { _useRegex = value; }
		}

		private string _foundValue = string.Empty;

		[Category("Results")]
		[DisplayName("Found Value")]
		[Description("The value that was found in the control.")]
		[DefaultValue("")]
		[XmlIgnore]
		public string FoundValue {
			get { return _foundValue; }
			set { _foundValue = value; }
		}

		public override void Body( TestRun run ) {
			ActualResult = TestCaseResult.Failed;
			_foundValue = string.Empty;

			// Build the regex if we have one
			Regex regex = null;

			if( _useRegex ) {
				regex = new Regex( _expectedValue, RegexOptions.Singleline | (_caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase) );
			}

			// Find the control
			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( c == null || !c.Exists ) {
				ActualResult = TestCaseResult.Failed;
				Log.Error( "Control not found", "Could not find control " + _control );
				return;
			}

			// Find the property
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties( c );
			PropertyDescriptor prop = properties.Find( _property, false );

			Log.Info( "Properties found were...", "Properties found were: " +
				string.Join( ", ", properties.Cast<PropertyDescriptor>().Select( p => p.Name ) ) );

			if( prop == null ) {
				ActualResult = TestCaseResult.Failed;
				Log.Warning( _property + " property not found",
					"Could not find property " + _property + " on control " + _control );

				return;
			}

			// Read the value
			_foundValue = Convert.ToString( prop.GetValue( c ), CultureInfo.CurrentCulture );

			Log.Info( "Found value: \"" + _foundValue + "\"" );

			// Verify it
			if( regex != null ) {
				if( !regex.IsMatch( _foundValue ) ) {
					ActualResult = TestCaseResult.Failed;
					throw new ArgumentException( "The property's value didn't match the regular expression." );
				}
			}
			else if( _caseSensitive && _foundValue != _expectedValue ) {
				ActualResult = TestCaseResult.Failed;
				throw new ArgumentException( "The property's value didn't match the specified text." );
			}
			else if( !_caseSensitive && StringComparer.CurrentCultureIgnoreCase.Compare( _foundValue, _expectedValue ) != 0 ) {
				ActualResult = TestCaseResult.Failed;
				throw new ArgumentException( "The property's value didn't match the specified text." );
			}

			ActualResult = TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Verifying " + _property + " value in control";
			}
		}

		public override object Clone() {
			VerifyPropertyFromControl result = (VerifyPropertyFromControl) base.Clone();

			result._foundValue = string.Empty;

			return result;
		}
	}

	/// <summary>
	/// When applied as a type converter to a property, provides a drop-down list with typical properties
	/// to find on controls.
	/// </summary>
	public class StandardPropertiesListConverter : StringConverter {
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			return new StandardValuesCollection( new string[] {
				"AlwaysOnTop",
				"Enabled",
				"IsSelected",
				"Name",
				"ToggleState",
				"Visible",
			} );
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
			return false;
		}
	}
}
