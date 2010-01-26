using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;



namespace QAliber.Repository.CommonTestCases.DB
{
	/// <summary>
	/// Queries a database with a specific SQL query
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"Database")]
	public class QueryDatabase : global::QAliber.TestModel.TestCase
	{
		public QueryDatabase()
		{
			name = "Query Database";
			icon = Properties.Resources.DatabaseQuery;
		}

		public override void Body()
		{
			OleDbConnection conn = new OleDbConnection(connectionString);
			OleDbCommand cmd = new OleDbCommand(sqlQuery, conn);
			OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
			DataSet dataSet = new DataSet("Result");
			
			conn.Open();

			adapter.Fill(dataSet);
			dataSet.WriteXml(outFile, XmlWriteMode.IgnoreSchema);

			conn.Close();
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;


		}

		private string connectionString = "";

		/// <summary>
		/// The connection string of the database
		/// <remarks>You can find different connection strings to different databases in &lt;a href="http://www.connectionstrings.com/"&gt;here&lt;/a&gt;</remarks>
		/// </summary>
		[DisplayName("1) Connection String")]
		[Category("Database")]
		[Description("The database's connection string")]
		public string ConnectionString
		{
			get { return connectionString; }
			set { connectionString = value; }
		}

		private string sqlQuery = "";

		/// <summary>
		/// The SQL query to launch
		/// <remarks>If you're not familiar with SQL you can start &lt;a href="http://www.w3schools.com/SQl/default.asp"&gt;here&lt;/a&gt;</remarks>
		/// </summary>
		[DisplayName("2) SQL Query")]
		[Category("Database")]
		[Description("The query to execute on the database")]
		public string SQLQuery
		{
			get { return sqlQuery; }
			set { sqlQuery = value; }
		}

		private string outFile = "";

		/// <summary>
		/// The XML file to store the SQL's result into
		/// </summary>
		[DisplayName("3) XML Output File")]
		[Category("Database")]
		[Description("The xml file to store the sql query")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string OutFile
		{
			get { return outFile; }
			set { outFile = value; }
		}


		public override string Description
		{
			get
			{
				return "Executing sql query '" + sqlQuery + "'";
			}
			set
			{
				base.Description = value;
			}
		}



	}
}
