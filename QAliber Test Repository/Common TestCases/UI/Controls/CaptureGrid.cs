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

namespace QAliber.Repository.CommonTestCases.UI.Controls {
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("CaptureGrid", Namespace=Util.XmlNamespace)]
	public class CaptureGrid : TestCase
	{
		public CaptureGrid() : base( "Capture Grid" )
		{
			Icon = Properties.Resources.DataGridView;
		}

		private string _control = "";

		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("Grid control to capture.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control {
			get { return _control; }
			set { _control = value; }
		}

		DataTable _resultTable;

		[Category("Results")]
		[DisplayName("Result Table")]
		[Description("Table that was captured.")]
		[XmlIgnore]
		public DataTable ResultTable {
			get { return _resultTable; }
		}

		public override void Body() {
			ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			_resultTable = null;

			UIControlBase c = UIControlBase.FindControlByPath( _control );

			if( c == null || !c.Exists ) {
				Log.Error( "Control not found", _control );
				return;
			}

			IGridPattern grid = c.GetControlInterface<IGridPattern>();

			if( grid == null ) {
				Log.Error( "Couldn't find an appropriate way to get at grid data." );
				return;
			}

			string[] headers;
			string[][] rows = grid.CaptureGrid( out headers );

			if( headers != null && headers.Distinct().Count() != headers.Length ) {
				Log.Error( "Duplicate column names", "Could not produce an output table because there are two columns with the same name." );
				return;
			}

			DataTable table = new DataTable( "Captured grid" );

			if( headers != null ) {
				foreach( string header in headers )
					table.Columns.Add( XPath.EscapeLiteral( header ), typeof(string) );
			}
			else {
				int columnCount = grid.ColumnCount;

				for( int i = 0; i < columnCount; i++ )
					table.Columns.Add( "Column " + i.ToString( CultureInfo.CurrentUICulture ), typeof(string) );
			}

			foreach( string[] row in rows )
				table.Rows.Add( row.Select( cell => cell ?? string.Empty ).Select( XPath.EscapeLiteral ).ToArray() );

			_resultTable = table;

			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description {
			get {
				return "Capturing grid from '" + _control + "'";
			}
		}

		public override object Clone() {
			CaptureGrid result = (CaptureGrid) base.Clone();

			result._resultTable = null;

			return result;
		}
	}
}
