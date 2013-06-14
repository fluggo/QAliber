using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QAliber.TestModel.Variables;
using System.Data;
using System.Text.RegularExpressions;

namespace QAliber.TestModel {
	public class TestStackFrame {
		BindingVariableList<ScenarioVariable<string>, string> _variables = new BindingVariableList<ScenarioVariable<string>,string>();
		BindingVariableList<ScenarioVariable<string[]>, string[]> _lists = new BindingVariableList<ScenarioVariable<string[]>,string[]>();
		BindingVariableList<ScenarioTable, DataTable> _tables = new BindingVariableList<ScenarioTable,DataTable>();

		public TestStackFrame( TestScenario scenario ) {
			foreach (ScenarioVariable<string> var in scenario.Variables)
			{
				_variables.Add(new ScenarioVariable<string>(
					var.Name, var.Value.ToString(), var.TestStep));
			}

			foreach (ScenarioVariable<string[]> var in scenario.Lists)
			{
				string[] copyList= new string[var.Value.Length];
				Array.Copy( var.Value, copyList, var.Value.Length );
				_lists.Add(new ScenarioVariable<string[]>(
					var.Name, copyList, var.TestStep));
			}

			foreach (ScenarioTable var in scenario.Tables)
			{
				DataTable table = var.Value as DataTable;
				DataTable copyTable = table.Copy();
				_tables.Add(new ScenarioTable(
					var.Name, copyTable, var.TestStep));
			}
		}

		public TestStackFrame( TestStackFrame parentFrame ) {
			foreach (ScenarioVariable<string> var in parentFrame.Variables)
			{
				_variables.Add(new ScenarioVariable<string>(
					var.Name, var.Value.ToString(), var.TestStep));
			}

			foreach (ScenarioVariable<string[]> var in parentFrame.Lists)
			{
				string[] copyList= new string[var.Value.Length];
				Array.Copy( var.Value, copyList, var.Value.Length );
				_lists.Add(new ScenarioVariable<string[]>(
					var.Name, copyList, var.TestStep));
			}

			foreach (ScenarioTable var in parentFrame.Tables)
			{
				DataTable table = var.Value as DataTable;
				DataTable copyTable = table.Copy();
				_tables.Add(new ScenarioTable(
					var.Name, copyTable, var.TestStep));
			}
		}

		public BindingVariableList<ScenarioVariable<string>, string> Variables {
			get { return _variables; }
		}

		public BindingVariableList<ScenarioVariable<string[]>, string[]> Lists {
			get { return _lists; }
		}

		public BindingVariableList<ScenarioTable, DataTable> Tables {
			get { return _tables; }
		}

		public string ReplaceAllVars(string input)
		{
			string res = Regex.Replace( input, @"\$([^\$\[\]]+)\$", new MatchEvaluator(ReplaceVarForMatch) );
			res = Regex.Replace( res, @"\$([^\$]+)\[([0-9]+)\]\$", new MatchEvaluator(ReplaceListForMatch) );
			res = Regex.Replace( res, @"\$([^\$]+)\[([0-9]+),([0-9]+)\]\$", new MatchEvaluator(ReplaceTableForMatch) );
			res = Regex.Replace( res, @"\$([^\$]+)\.([a-zA-Z]+)\$", new MatchEvaluator(ReplacePropertyForMatch));

			return res;
		}

		private string ReplaceVarForMatch(Match match)
		{
			string key = match.Value.Trim('$');
			var var = _variables[key];
			if (var != null)
				return var.Value.ToString();
			else
				return match.Value;
		}

		private string ReplaceListForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			var list = _lists[key];
			if (_lists != null)
			{
				string[] val = list.Value as string[];
				if (val != null)
				{
					int index = val.Length;
					if (int.TryParse(match.Groups[2].Value, out index))
					{
						if (index < val.Length)
							return val[index].ToString();
					}
				}
			}
			return match.Value;
		}

		private string ReplacePropertyForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			var list = _lists[key];
			if (_lists != null)
			{
				string[] val = list.Value as string[];
				if (val != null)
				{
					switch (match.Groups[2].Value.ToLower())
					{
						case "length":
							return val.Length.ToString();
						default:
							break;
					}
					
				}
			}
			var table = _tables[key];
			if (table != null)
			{
				DataTable val = table.Value as DataTable;
				if (val != null)
				{
					switch (match.Groups[2].Value.ToLower())
					{
						case "numberofrows":
							return val.Rows.Count.ToString();
						case "numberofcolumns":
							return val.Columns.Count.ToString();
						default:
							break;
					}

				}
			}
			return match.Value;
		}

		private string ReplaceTableForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			ScenarioVariable<DataTable> table = _tables[key];
			if (table != null)
			{
				DataTable dataTable = table.Value as DataTable;
				if (dataTable != null)
				{
					int rowIndex = dataTable.Rows.Count, colIndex = dataTable.Columns.Count;
					if (int.TryParse(match.Groups[2].Value, out rowIndex)
						&& int.TryParse(match.Groups[3].Value, out colIndex))
					{
						if (rowIndex < dataTable.Rows.Count && colIndex < dataTable.Columns.Count)
							return dataTable.Rows[rowIndex][colIndex].ToString();
					}
				}
			}
			return match.Value;
		}
	}

	/// <summary>
	/// Provides access to the current test run.
	/// </summary>
	public class TestRun {
		volatile bool _canceled;
		int _branchesToBreak;
		TestScenario _scenario;
		Stack<TestStackFrame> _stack = new Stack<TestStackFrame>();

		public TestRun( TestScenario scenario ) {
			_scenario = scenario;
			_stack.Push( new TestStackFrame( scenario ) );
		}

		public void Cancel() {
			_canceled = true;
		}

		class OpenStackFrame : IDisposable {
			TestRun _run;

			public OpenStackFrame( TestRun run ) {
				_run = run;
				_run._stack.Push( new TestStackFrame( _run._stack.Peek() ) );
			}

			public void Dispose() {
				_run._stack.Pop();
			}
		}

		public IDisposable NewStackFrame() {
			return new OpenStackFrame( this );
		}

		/// <summary>
		/// Gets a value that represents whether the run has been canceled.
		/// </summary>
		/// <value>True if the run should end at the nearest convenience, false otherwise.</value>
		public bool Canceled
			{ get { return _canceled; } }

		public int BranchesToBreak {
			get { return _branchesToBreak; }
			set { _branchesToBreak = value; }
		}

		public TestScenario Scenario
			{ get { return _scenario; } }

		public BindingVariableList<ScenarioVariable<string>, string> Variables {
			get { return _stack.Peek().Variables; }
		}

		public BindingVariableList<ScenarioVariable<string[]>, string[]> Lists {
			get { return _stack.Peek().Lists; }
		}

		public BindingVariableList<ScenarioTable, DataTable> Tables {
			get { return _stack.Peek().Tables; }
		}

		public string ReplaceAllVars( string input ) {
			return _stack.Peek().ReplaceAllVars( input );
		}
	}
}
