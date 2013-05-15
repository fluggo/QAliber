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

namespace QAliber.Repository.CommonTestCases.UI.Controls {
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("VerifyGrid", Namespace=Util.XmlNamespace)]
	public class VerifyGrid : TestCase
	{
		public VerifyGrid() : base( "Verify Grid from Variable" )
		{
			Icon = Properties.Resources.DataGridView;
		}

		private string _control = string.Empty;

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("Grid control to capture.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		private string _variable = string.Empty;

		[Category("Behavior")]
		[DisplayName("Source Table")]
		[Description("Table variable with the expected values for the grid.")]
		[TypeConverter(typeof(TableVariableNameTypeConverter))]
		public string Variable {
			get { return _variable; }
			set { _variable = value; }
		}

		string[] _verifyColumns = new string[0];
		int[] _verifyRows = new int[0];

		[Category("Behavior")]
		[DisplayName("Verify Columns")]
		[Description("Optional list of columns from the source table to expect in the control's grid. If empty, all columns from the source table are expected.")]
		[Editor(typeof(MultipleSelectionTypeEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(DisplayListConverter))]
		public string[] VerifyColumns {
			get { return _verifyColumns; }
			set { _verifyColumns = value; }
		}

		public string[] GetVerifyColumnsOptions() {
			ScenarioVariable<DataTable> testTableVar = Scenario.Tables[_variable];

			if( testTableVar == null )
				return new string[0];

			return testTableVar.Value.Columns.Cast<DataColumn>().Select( col => col.ColumnName ).ToArray();
		}

		/// <summary>
		/// Determines whether <see cref="VerifyColumns"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeVerifyColumns() {
			return VerifyColumns.Length != 0;
		}

		/// <summary>
		/// Resets <see cref="VerifyColumns"/> to its default value.
		/// </summary>
		public void ResetVerifyColumns() {
			VerifyColumns = new string[0];
		}

		[Category("Behavior")]
		[DisplayName("Verify Rows")]
		[Description("Optional list of rows from the source table to expect in the control's grid. If empty, all rows from the source table are expected.")]
		[Editor(typeof(MultipleSelectionIndexTypeEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(DisplayListConverter))]
		public int[] VerifyRows {
			get { return _verifyRows; }
			set { _verifyRows = value; }
		}

		public string[] GetVerifyRowsOptions() {
			ScenarioVariable<DataTable> testTableVar = Scenario.Tables[_variable];

			if( testTableVar == null )
				return new string[0];

			return testTableVar.Value.Rows.Cast<DataRow>().Select( row =>
				string.Join( ", ", row.ItemArray.Select( item => (item as string) ?? string.Empty ) ) ).ToArray();
		}

		/// <summary>
		/// Determines whether <see cref="VerifyRows"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeVerifyRows() {
			return VerifyRows.Length != 0;
		}

		/// <summary>
		/// Resets <see cref="VerifyRows"/> to its default value.
		/// </summary>
		public void ResetVerifyRows() {
			VerifyRows = new int[0];
		}

		bool _allowExtraRows = false, _allowExtraColumns = false;
		bool _verifyRowOrder = true, _verifyColumnOrder = true;
		bool _requireHeaders = true;

		[Category("Behavior")]
		[DisplayName("Allow Extra Rows")]
		[Description("True to allow extra rows to appear besides the ones given in the table variable.")]
		[DefaultValue(false)]
		public bool AllowExtraRows {
			get { return _allowExtraRows; }
			set { _allowExtraRows = value; }
		}

		[Category("Behavior")]
		[DisplayName("Allow Extra Columns")]
		[Description("True to allow extra columns to appear besides the ones given in the table variable.")]
		[DefaultValue(false)]
		public bool AllowExtraColumns {
			get { return _allowExtraColumns; }
			set { _allowExtraColumns = value; }
		}

		[Category("Behavior")]
		[DisplayName("Verify Row Order")]
		[Description("Set to true to require that rows in the grid appear in the same order as the ones in the table variable. If extra rows are allowed, they are ignored when comparing order.")]
		[DefaultValue(true)]
		public bool VerifyRowOrder {
			get { return _verifyRowOrder; }
			set { _verifyRowOrder = value; }
		}

		[Category("Behavior")]
		[DisplayName("Verify Column Order")]
		[Description("Set to true to require that columns in the grid appear in the same order as the ones in the table variable. If extra columns are allowed, they are ignored when comparing order. If the target grid doesn't have headers, the columns are verified in-order anyways.")]
		[DefaultValue(true)]
		public bool VerifyColumnOrder {
			get { return _verifyColumnOrder; }
			set { _verifyColumnOrder = value; }
		}

		[Category("Behavior")]
		[DisplayName("Require Headers")]
		[Description("True to require the control's grid to have headers. If the grid doesn't have headers, the test table is matched by index, not column.")]
		[DefaultValue(true)]
		public bool RequireHeaders {
			get { return _requireHeaders; }
			set { _requireHeaders = value; }
		}

		DataTable _resultTable;

		[Category("Results")]
		[DisplayName("Result Table")]
		[Description("Table that was captured.")]
		[XmlIgnore]
		public DataTable ResultTable {
			get { return _resultTable; }
		}

		public override void Body( TestRun run ) {
			ActualResult = TestCaseResult.Failed;
			_resultTable = null;

			// Get the variable we're testing against
			ScenarioVariable<DataTable> testTableVar = Scenario.Tables[_variable];

			if( testTableVar == null ) {
				Log.Error( "Could not find variable \"" + _variable + "\"" );
				return;
			}

			// Filter the test table down to what the user wants us to find
			DataTable testTable = testTableVar.Value.Clone();

			if( _verifyRows.Length == 0 ) {
				// Grab all rows
				testTable.Merge( testTableVar.Value );
			}
			else {
				foreach( int rowIndex in _verifyRows ) {
					if( rowIndex >= testTableVar.Value.Rows.Count ) {
						Log.Error( "Verify Rows refers to an invalid row",
							"The Verify Rows property refers to a row that's not in the source table." );
						return;
					}

					testTable.Rows.Add( testTableVar.Value.Rows[rowIndex].ItemArray );
				}
			}

			if( _verifyColumns.Length != 0 ) {
				HashSet<string> verifyColumns = new HashSet<string>( _verifyColumns );

				string[] verifyColumnsNotFound =
					verifyColumns.Except( testTable.Columns.Cast<DataColumn>().Select( col => col.ColumnName ) ).ToArray();

				if( verifyColumnsNotFound.Length != 0 ) {
					Log.Error( "Verify Columns refers to an invalid column",
						"The Verify Columns property refers to one or more invalid columns: " +
							string.Join( ", ", verifyColumnsNotFound ) );
					return;
				}

				foreach( DataColumn column in testTable.Columns.Cast<DataColumn>().ToArray() ) {
					if( !verifyColumns.Contains( column.ColumnName ) )
						testTable.Columns.Remove( column );
				}
			}

			// Capture the grid
			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( c == null || !c.Exists ) {
				Log.Error( "Control not found", _control );
				return;
			}

			IGridPattern grid = c.GetControlInterface<IGridPattern>();

			if( grid == null ) {
				Log.Error( "Can't capture grid", "Couldn't find an appropriate way to get at grid data." );
				return;
			}

			string[] headers;
			string[][] rows = grid.CaptureGrid( out headers );

			if( _requireHeaders && headers == null ) {
				LogFailedByExpectedResult( "No headers in target grid", "Target grid didn't have any headers, and Require Headers was set." );
				return;
			}

			bool columnsUnique = true;

			if( headers != null && headers.Distinct().Count() != headers.Length ) {
				columnsUnique = false;
				Log.Warning( "Duplicate column names", "Could not produce an output table because there are two columns with the same name." );
			}

			// Create the grid result table
			if( columnsUnique ) {
				DataTable table = new DataTable( "Captured grid" );

				if( headers != null ) {
					headers = Array.ConvertAll( headers, header => XPath.EscapeLiteral( header ) );

					foreach( string header in headers )
						table.Columns.Add( header, typeof(string) );
				}
				else {
					int columnCount = grid.ColumnCount;

					for( int i = 0; i < columnCount; i++ )
						table.Columns.Add( "Column " + i.ToString( CultureInfo.CurrentUICulture ), typeof(string) );
				}

				rows = Array.ConvertAll( rows, row =>
					Array.ConvertAll( row, cell => XPath.EscapeLiteral( cell ?? string.Empty ) ) );

				foreach( string[] row in rows )
					table.Rows.Add( row );

				_resultTable = table;
			}

			// Go ahead and log the capture
			string rowText = string.Join( "\r\n", rows.Select( row => "[" + string.Join( ", ", row ) + "]" ) );

			if( headers != null )
				rowText = "[" + string.Join( ", ", headers ) + "]\r\n" + rowText;

			Log.Info( "Captured grid", rowText );

			// Shortcut out on some special cases
			if( rows.Length < testTable.Rows.Count ) {
				LogFailedByExpectedResult( "Not enough rows in target grid",
					string.Format( "Target grid had {0} rows, expected at least {1}.", rows.Length, testTable.Rows.Count ) );
				return;
			}

			if( !_allowExtraRows && rows.Length > testTable.Rows.Count ) {
				LogFailedByExpectedResult( "Too many rows in target grid",
					string.Format( "Target grid had {0} rows, expected {1}.", rows.Length, testTable.Rows.Count ) );
				return;
			}

			if( rows.Length != 0 ) {
				if( rows[0].Length < testTable.Columns.Count ) {
					LogFailedByExpectedResult( "Not enough columns in target grid",
						string.Format( "Target grid had {0} columns, expected at least {1}.", rows[0].Length, testTable.Columns.Count ) );
					return;
				}

				if( !_allowExtraColumns && rows[0].Length > testTable.Columns.Count ) {
					LogFailedByExpectedResult( "Too many columns in target grid",
						string.Format( "Target grid had {0} columns, expected {1}.", rows[0].Length, testTable.Columns.Count ) );
					return;
				}
			}

			// Filter out any columns we don't care about
			if( headers != null ) {
				// Filter by column name
				List<string> testColumns = testTable.Columns.Cast<DataColumn>()
					.Select( col => col.ColumnName ).ToList();

				int[] keptColumns = new int[testColumns.Count];

				for( int index = 0; index < headers.Length; index++ ) {
					int foundIndex = testColumns.IndexOf( headers[index] );

					if( foundIndex != -1 ) {
						testColumns[foundIndex] = null;
						keptColumns[foundIndex] = index;
					}
					else if( !_allowExtraColumns ) {
						LogFailedByExpectedResult( "Found extra column \"" + headers[index] + "\" in grid", string.Empty );
						return;
					}
				}

				// keptColumns as created will reorder the columns into the expected order;
				// if the user wants us to verify the column order as well, we have to re-sort
				// into the original order as found in the target grid
				if( _verifyColumnOrder )
					Array.Sort<int>( keptColumns );

				headers = keptColumns.Select( i => headers[i] ).ToArray();
				rows = rows.Select( row => keptColumns.Select( i => row[i] ).ToArray() ).ToArray();
			}

			// Finally, verify rows
			if( _verifyRowOrder ) {
				List<HashedRow>	extraRows = new List<HashedRow>();
				HashedRow[] targetRows = rows.Select( row => new HashedRow( row ) ).ToArray();
				var testRows = testTable.Rows.Cast<DataRow>()
					.Select( row => new HashedRow( row.ItemArray.Cast<string>().ToArray() ) );
				int targetRowIndex = 0;

				foreach( var testRow in testRows ) {
					// We must find this row in the target to proceed
					bool matched = false;
					int expectedPosition = targetRowIndex;

					while( targetRowIndex < targetRows.Length ) {
						if( targetRows[targetRowIndex].Equals( testRow ) ) {
							// Row found, stop here
							targetRowIndex++;
							matched = true;
							break;
						}

						// Target row which does not match
						extraRows.Add( targetRows[targetRowIndex] );
						targetRowIndex++;
					}

					if( matched )
						continue;

					// Made it to the end of the target rows without finding it
					if( extraRows.Contains( testRow ) ) {
						LogFailedByExpectedResult( "Row in wrong place", "This row was in the wrong position while \"Verify Row Order\" was on: [" +
							string.Join( ", ", testRow ) + "]\r\n\r\nIt was expected at or after row " + (expectedPosition + 1).ToString() + "." );
					}
					else {
						LogFailedByExpectedResult( "Expected row missing", "Could not find this row in the target grid: [" +
							string.Join( ", ", testRow ) + "]\r\n\r\nIt was expected at or after row " + (expectedPosition + 1).ToString() + "." );

						if( extraRows.Count != 0 && !_allowExtraRows ) {
							LogFailedByExpectedResult( "Found extra row(s)", "Found these rows in the target grid while \"Allow Extra Rows\" was off:\r\n\r\n" +
								string.Join( "\r\n",
									extraRows.Select( row => "[" + string.Join( ", ", row ) + "]" ) ) );
						}
					}

					return;
				}

				if( extraRows.Count != 0 && !_allowExtraRows ) {
					LogFailedByExpectedResult( "Found extra row(s)", "Found these rows in the target grid while \"Allow Extra Rows\" was off:\r\n\r\n" +
						string.Join( "\r\n",
							extraRows.Select( row => "[" + string.Join( ", ", row ) + "]" ) ) );
					return;
				}
			}
			else {
				// Unordered find; make sure there are the right number of each kind of row
				List<HashedRow>	missingRows = new List<HashedRow>();
				List<HashedRow> targetRows = rows.Select( row => new HashedRow( row ) ).ToList();
				var testRows = testTable.Rows.Cast<DataRow>()
					.Select( row => new HashedRow( row.ItemArray.Cast<string>().ToArray() ) );

				foreach( var testRow in testRows ) {
					int index = targetRows.IndexOf( testRow );

					if( index == -1 )
						missingRows.Add( testRow );
					else
						targetRows.RemoveAt( index );
				}

				if( missingRows.Count != 0 ) {
					LogFailedByExpectedResult( "Expected row(s) missing", "These rows were expected but not found in the target grid:\r\n\r\n" +
						string.Join( "\r\n",
							missingRows.Select( row => "[" + string.Join( ", ", row ) + "]" ) ) );
					return;
				}

				// Make sure there are none left if we aren't supposed to allow extra rows
				if( !_allowExtraRows && targetRows.Count != 0 ) {
					LogFailedByExpectedResult( "Found extra row(s)", "Found these rows in the target grid while \"Allow Extra Rows\" was off:\r\n\r\n" +
						string.Join( "\r\n",
							targetRows.Select( row => "[" + string.Join( ", ", row ) + "]" ) ) );
					return;
				}
			}

			ActualResult = TestCaseResult.Passed;
		}

		class HashedRow : IEquatable<HashedRow>, IEnumerable<string> {
			string[] _data;
			int _hash;

			public HashedRow( string[] data ) {
				_data = data;
				_hash = data.Aggregate( 0, (accum, str) => unchecked(accum * str.GetHashCode()) );
			}

			public string this[int index] {
				get { return _data[index]; }
			}

			public bool Equals( HashedRow other ) {
				if( other == null )
					return false;

				if( _hash != other._hash )
					return false;

				if( _data.Length != other._data.Length )
					return false;

				return _data.Zip( other._data, (a, b) => new { A = a, B = b } ).All( t => t.A == t.B );
			}

			public override bool Equals( object obj ) {
				return this.Equals( obj as HashedRow );
			}

			public override int GetHashCode() {
				return _hash;
			}

			public IEnumerator<string> GetEnumerator() {
				return _data.Cast<string>().GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
				return _data.GetEnumerator();
			}
		}

		public override string Description {
			get {
				return "Verifying grid from '" + _control + "'";
			}
		}

		public override object Clone() {
			VerifyGrid result = (VerifyGrid) base.Clone();

			result._resultTable = null;

			return result;
		}
	}
}
