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
