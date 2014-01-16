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
using QAliber.TestModel.Variables;
using QAliber.TestModel.TypeEditors;
using System.Drawing.Design;
using QAliber.Repository.CommonTestCases.UITypeEditors;
using QAliber.RemotingModel;
using System.ComponentModel.Design;

namespace QAliber.Repository.CommonTestCases.UI.Controls {
	public abstract class VerifyListBase : TestCase
	{
		public VerifyListBase( string name ) : base( name )
		{
			Icon = Properties.Resources.Combobox;
		}

		private string _control = string.Empty;

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("List or combo box control to capture.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		bool _allowExtraItems = false;
		bool _verifyItemOrder = true;

		[Category("Behavior")]
		[DisplayName("Allow Extra Items")]
		[Description("True to allow extra items to appear besides the ones given in the list variable.")]
		[DefaultValue(false)]
		public bool AllowExtraItems {
			get { return _allowExtraItems; }
			set { _allowExtraItems = value; }
		}

		[Category("Behavior")]
		[DisplayName("Verify Item Order")]
		[Description("Set to true to require that items in the list appear in the same order as the ones in the list variable. If extra items are allowed, they are ignored when comparing order.")]
		[DefaultValue(true)]
		public bool VerifyItemOrder {
			get { return _verifyItemOrder; }
			set { _verifyItemOrder = value; }
		}

		string[] _resultList;

		[Category("Results")]
		[DisplayName("Result List")]
		[Description("List that was captured.")]
		[XmlIgnore]
		public string[] ResultList {
			get { return _resultList; }
		}

		protected abstract string[] GetTestList();

		public static string[] CaptureList( string control, bool logErrors ) {
			// Capture the list
			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( c == null || !c.Exists ) {
				if( logErrors )
					Log.Error( "Control not found", control );

				return null;
			}

			IListPattern list = c.GetControlInterface<IListPattern>();

			if( list != null )
				return list.CaptureList();

			ISelector selector = c.GetControlInterface<ISelector>();

			if( selector != null )
				return selector.Items;

			// Try one control up; we could be on the combo box's edit or button controls
			c = c.Parent;

			if( c != null ) {
				list = c.GetControlInterface<IListPattern>();

				if( list != null )
					return list.CaptureList();

				selector = c.GetControlInterface<ISelector>();

				if( selector != null )
					return selector.Items;
			}

			if( logErrors )
				Log.Error( "Couldn't find a way to read the list." );

			return null;
		}

		public override void Body() {
			ActualResult = TestCaseResult.Failed;
			_resultList = null;

			string[] testList = GetTestList();

			if( testList == null )
				return;

			string[] captureList = CaptureList( _control, true );

			if( captureList == null )
				return;

			_resultList = Array.ConvertAll( captureList, str => XPath.EscapeLiteral( str ) );

			string[] items = _resultList;

			// Go ahead and log the capture
			Log.Info( "Captured list", string.Join( "\r\n", _resultList ) );

			// Shortcut out on some special cases
			if( items.Length < testList.Length ) {
				LogFailedByExpectedResult( "Not enough items in target list",
					string.Format( "Target list had {0} items, expected at least {1}.", items.Length, testList.Length ) );
				return;
			}

			if( !_allowExtraItems && items.Length > testList.Length ) {
				LogFailedByExpectedResult( "Too many items in target list",
					string.Format( "Target list had {0} items, expected {1}.", items.Length, testList.Length ) );
				return;
			}

			// Finally, verify items
			if( _verifyItemOrder ) {
				List<string> extraItems = new List<string>();
				int targetItemIndex = 0;

				foreach( var testItem in testList ) {
					// We must find this item in the target to proceed
					bool matched = false;

					while( targetItemIndex < _resultList.Length ) {
						if( _resultList[targetItemIndex].Equals( testItem ) ) {
							// Item found, stop here
							targetItemIndex++;
							matched = true;
							break;
						}

						// Target item which does not match
						extraItems.Add( _resultList[targetItemIndex] );
						targetItemIndex++;
					}

					if( matched )
						continue;

					// Made it to the end of the target items without finding it
					LogFailedByExpectedResult( "Expected item missing", "Could not find \"" + testItem + "\" in the target list." );
					return;
				}

				if( extraItems.Count != 0 && !_allowExtraItems ) {
					LogFailedByExpectedResult( "Found extra item(s)", "Found these items in the target list while \"Allow Extra Items\" was off:\r\n\r\n" +
						string.Join( "\r\n", extraItems ) );
					return;
				}
			}
			else {
				// Unordered find; make sure there are the right number of each kind of item
				List<string> missingItems = new List<string>();
				List<string> targetItems = new List<string>( items );

				foreach( var testItem in testList ) {
					int index = targetItems.IndexOf( testItem );

					if( index == -1 )
						missingItems.Add( testItem );
					else
						targetItems.RemoveAt( index );
				}

				if( missingItems.Count != 0 ) {
					LogFailedByExpectedResult( "Expected item(s) missing", "These items were expected but not found in the target list:\r\n\r\n" +
						string.Join( "\r\n", missingItems ) );
					return;
				}

				// Make sure there are none left if we aren't supposed to allow extra items
				if( !_allowExtraItems && targetItems.Count != 0 ) {
					LogFailedByExpectedResult( "Found extra item(s)", "Found these items in the target list while \"Allow Extra Items\" was off:\r\n\r\n" +
						string.Join( "\r\n", targetItems ) );
					return;
				}
			}

			ActualResult = TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Verifying list from '" + _control + "'";
			}
		}

		public override object Clone() {
			VerifyListBase result = (VerifyListBase) base.Clone();

			result._resultList = null;

			return result;
		}
	}

	/// <summary>
	/// Verify list test step that uses a variable.
	/// </summary>
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("VerifyList", Namespace=Util.XmlNamespace)]
	public class VerifyListVariable : VerifyListBase {
		public VerifyListVariable() : base( "Verify List from Variable" ) {
		}

		private string _variable = string.Empty;

		[Category("Behavior")]
		[DisplayName("Source List")]
		[Description("List variable with the expected values for the list.")]
		[TypeConverter(typeof(ListVariableNameTypeConverter))]
		public string Variable {
			get { return _variable; }
			set { _variable = value; }
		}

		int[] _verifyItems = new int[0];

		[Category("Behavior")]
		[DisplayName("Verify Items")]
		[Description("Optional list of items from the source list to expect in the control. If empty, all items from the source list are expected.")]
		[Editor(typeof(MultipleSelectionIndexTypeEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(DisplayListConverter))]
		public int[] VerifyItems {
			get { return _verifyItems; }
			set { _verifyItems = value; }
		}

		public string[] GetVerifyItemsOptions() {
			ScenarioVariable<string[]> testListVar = Scenario.Lists[_variable];

			if( testListVar == null )
				return new string[0];

			return testListVar.Value;
		}

		/// <summary>
		/// Determines whether <see cref="VerifyItems"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeVerifyItems() {
			return VerifyItems.Length != 0;
		}

		/// <summary>
		/// Resets <see cref="VerifyItems"/> to its default value.
		/// </summary>
		public void ResetVerifyItems() {
			VerifyItems = new int[0];
		}

		protected override string[] GetTestList() {
			// Get the variable we're testing against
			ScenarioVariable<string[]> testListVar = Scenario.Lists[_variable];

			if( testListVar == null ) {
				Log.Error( "Could not find variable \"" + _variable + "\"" );
				return null;
			}

			// Filter the test list down to what the user wants us to find
			List<string> testList = new List<string>();

			if( _verifyItems.Length != 0 ) {
				foreach( int index in _verifyItems ) {
					if( index >= _verifyItems.Length ) {
						Log.Error( "Verify Items refers to an invalid item",
							"The Verify Items property refers to an item that's not in the source list." );
						return null;
					}

					testList.Add( testListVar.Value[index] );
				}
			}

			return testList.ToArray();
		}
	}

	/// <summary>
	/// Verify list test step that uses a manually-specified list.
	/// </summary>
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("VerifyListManual", Namespace=Util.XmlNamespace)]
	public class VerifyList : VerifyListBase {
		public VerifyList() : base( "Verify List" ) {
		}

		private string _list = string.Empty;

		[Category("Behavior")]
		[DisplayName("Verify Items")]
		[Description("List of items to expect in the control.")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(MultilineStringListConverter))]
		public string VerifyItems {
			get { return _list; }
			set { _list = value; }
		}

		public void ControlPropertyChanging( string control ) {
			if( string.IsNullOrWhiteSpace( _list ) ) {
				try {
					_list = string.Join( "\r\n", CaptureList( control, false ) );
				}
				catch {
				}
			}
		}

		protected override string[] GetTestList() {
			// Test list could either have \r\n line endings (from UI) or \n endings (from XML)
			return Array.ConvertAll( _list.TrimEnd().Split( '\n' ), str => str.TrimEnd( '\r' ) );
		}
	}
}
