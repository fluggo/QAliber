using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QAliber.TestModel.Variables
{
	[Serializable]
	public class ScenarioList : ScenarioVariable
	{
		public ScenarioList()
		{

		}
		public ScenarioList(string name, ICollection initVal, TestCase definer)
			: base(name, definer)
		{
			this.initVal = initVal;
		}

		private ICollection initVal;

		public new object Value
		{
			get
			{
				List<string> listVals = new List<string>();
				ICollection col = initVal as ICollection;
				if (col != null)
				{
					foreach (object obj in initVal)
					{
						listVals.Add(obj.ToString());
					}
				}
				return listVals.ToArray();

			}
			set
			{
				ICollection col = value as ICollection;
				if (value != null)
					initVal = col;
				NotifyPropertyChanged("Value");
			}
		}
	}
}
