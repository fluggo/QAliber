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
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace QAliber.TestModel.Variables
{
	[Serializable]
	public class BindingVariableList<TVar, TValue> : BindingList<TVar> where TVar : ScenarioVariable<TValue>
	{
		public void AddOrReplace(TVar obj)
		{
			for (int i = 0; i < Count; i++)
			{
				TVar item = this[i];
				if (item.Name == obj.Name && item.TestStep == obj.TestStep)
				{
					this[i] = obj;
					return;
				}
			}
			Add(obj);
		}

		public void AddOrReplaceByName(TVar obj)
		{
			for (int i = 0; i < Count; i++)
			{
				TVar item = this[i];
				if (item.Name == obj.Name)
				{
					this[i] = obj;
					return;
				}
			}
			Add(obj);
		}

		public void RemoveIfFound(TVar obj)
		{
			for (int i = 0; i < Count; i++)
			{
				TVar item = this[i];
				if (item.Name == obj.Name && item.TestStep == obj.TestStep)
				{
					this.RemoveAt(i);
					return;
				}
			}
		}

		public void RemoveAllByTestCase(TestCase testcase)
		{
			for (int i = 0; i < Count; i++)
			{
				TVar item = this[i];
				if (item.TestStep != null && item.TestStep.Equals(testcase))
				{
					this.RemoveAt(i);
					return;
				}
			}
		}

		public TVar this[string name]
		{
			get
			{
				for (int i = 0; i < Count; i++)
				{
					TVar item = this[i];
					if (item.Name == name)
					{
						return item;
					}
				}
				return null;
			}
		}

		protected override void OnListChanged(ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
			{
				if ((e.ListChangedType == ListChangedType.ItemAdded && e.PropertyDescriptor == null) || (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Name"))
				{
					TVar obj = this[e.NewIndex];
					if (string.IsNullOrEmpty(obj.Name))
						obj.Name = "ChangeThisName";
					string res = ChangeDuplicateNames(obj.Name, e.NewIndex);
					if (res != obj.Name)
						obj.Name = res;
				}
			}

			base.OnListChanged(e);

		}
		private string ChangeDuplicateNames(string name, int indexToIgnore)
		{
			string res = name;
			for (int i = 0; i < this.Count; i++)
			{
				if (i != indexToIgnore)
				{
					TVar item = this[i];
					if (item.Name == name)
					{
						Regex regex = new Regex("_[0-9]+$");
						Match match = regex.Match(item.Name);
						if (match.Success)
						{
							string indexStr = match.Value.Substring(1);
							int index = 0;
							if (int.TryParse(indexStr, out index))
							{
								index++;
								res = regex.Replace(item.Name, "_" + index.ToString());
								res = ChangeDuplicateNames(res, indexToIgnore);
							}
						}
						else
						{
							res += "_1";
							res = ChangeDuplicateNames(res, indexToIgnore);
						}
						break;
					}
				}
			}
			return res;
		}


	}
}
