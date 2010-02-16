/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
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
