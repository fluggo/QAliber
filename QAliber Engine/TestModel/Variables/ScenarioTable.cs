using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QAliber.TestModel.Variables
{
	[Serializable]
	public class ScenarioTable : ScenarioVariable
	{
		public ScenarioTable()
		{

		}

		public ScenarioTable(string name, DataTable initVal, TestCase definer)
			: base(name, definer)
		{
			this.initVal = initVal;
		}

		private DataTable initVal;

		public new object Value
		{
			get
			{
				if (initVal == null)
					initVal = new DataTable("Data Table");
				return initVal;

			}
			set
			{
				initVal = (DataTable)value;
				NotifyPropertyChanged("Value");
			}
		}
	}
}
