using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.TestModel.Variables
{
	[Serializable]
	public class ComboDropDownList
	{

		public string Selected
		{
			get { return selected; }
			set { selected = value; }
		}

		public string[] Items
		{
			get { return items; }
			set { items = value; }
		}

		public override string ToString()
		{
			return selected;
		}

		public virtual void Update(TestScenario scenario)
		{
		}

		private string selected = "";
		private string[] items = new string[] { };

	}

	[Serializable]
	public class ListVariableDropDownList : ComboDropDownList
	{
		public override void Update(TestScenario scenario)
		{
			List<string> tmpList = new List<string>();
			foreach (ScenarioList l in scenario.Lists)
			{
				tmpList.Add(l.Name);
			}
			Items = tmpList.ToArray();
		}

	}

	[Serializable]
	public class TableVariableDropDownList : ComboDropDownList
	{
		public override void Update(TestScenario scenario)
		{
			List<string> tmpList = new List<string>();
			foreach (ScenarioTable t in scenario.Tables)
			{
				tmpList.Add(t.Name);
			}
			Items = tmpList.ToArray();
		}

	}

	[Serializable]
	public class VariableDropDownList : ComboDropDownList
	{
		public override void Update(TestScenario scenario)
		{
			List<string> tmpList = new List<string>();
			foreach (ScenarioVariable v in scenario.Variables)
			{
				tmpList.Add(v.Name);
			}
			Items = tmpList.ToArray();
		}

	}

}
