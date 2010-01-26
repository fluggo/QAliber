using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace QAliber.TestModel.Variables
{
	[Serializable]
	public class BindingVariableList<T> : BindingList<T> where T : IVariable
	{
		public void AddOrReplace(T obj)
		{
			for (int i = 0; i < Count; i++)
			{
				T item = this[i];
				if (item.Name == obj.Name && item.DefinedBy == obj.DefinedBy)
				{
					this[i] = obj;
					return;
				}
			}
			Add(obj);
		}

		public void AddOrReplaceByName(T obj)
		{
			for (int i = 0; i < Count; i++)
			{
				T item = this[i];
				if (item.Name == obj.Name)
				{
					this[i] = obj;
					return;
				}
			}
			Add(obj);
		}

		public void RemoveIfFound(T obj)
		{
			for (int i = 0; i < Count; i++)
			{
				T item = this[i];
				if (item.Name == obj.Name && item.DefinedBy == obj.DefinedBy)
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
				T item = this[i];
				if (item.Definer.Equals(testcase))
				{
					this.RemoveAt(i);
					return;
				}
			}
		}

		public T this[string name]
		{
			get
			{
				for (int i = 0; i < Count; i++)
				{
					T item = this[i];
					if (item.Name == name)
					{
						return item;
					}
				}
				return default(T);
			}
		}

		protected override void OnListChanged(ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
			{
				if ((e.ListChangedType == ListChangedType.ItemAdded && e.PropertyDescriptor == null) || (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Name"))
				{
					T obj = this[e.NewIndex];
					if (string.IsNullOrEmpty(obj.Name))
						obj.Name = "ChangeThisName";
					string res = ChangeDuplicateNames(obj.Name, e.NewIndex);
					if (res != obj.Name)
						obj.Name = res;
				}
			}

			base.OnListChanged(e);

		}
		protected override void RemoveItem(int index)
		{
			T obj = this[index];
			if (obj.Definer != null && obj.Definer.OutputProperties != null)
			{
				if (obj.Definer.OutputProperties.ContainsKey(obj.Name))
				{
					obj.Definer.OutputProperties.Remove(obj.Name);
				}
			}
			base.RemoveItem(index);
		}
		private string ChangeDuplicateNames(string name, int indexToIgnore)
		{
			string res = name;
			for (int i = 0; i < this.Count; i++)
			{
				if (i != indexToIgnore)
				{
					T item = this[i];
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
