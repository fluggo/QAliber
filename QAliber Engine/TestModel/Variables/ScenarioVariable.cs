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
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace QAliber.TestModel.Variables
{

	

	[Serializable]
	public class ScenarioVariable : IVariable, INotifyPropertyChanged
	{
		public ScenarioVariable()
		{

		}

		protected ScenarioVariable(string name, TestCase definer)
		{
			this.name = name;
			this.testCaseDefiner = definer;
		}

		public ScenarioVariable(string name, string initVal, TestCase definer)
			: this(name, definer)
		{
			this.initVal = initVal;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string name)
		{
			
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion

		protected string name;

		public string Name
		{
			get { return name; }
			set 
			{ 
				name = value;
				NotifyPropertyChanged("Name");
			}
		}


		public TestCase Definer
		{
			get { return testCaseDefiner; }
		}

	
		private string initVal;

		public object Value
		{
			get 
			{
				if (initVal == null)
					initVal = "";
				return initVal; 
			}
			set 
			{
				if (value == null)
					return;
				initVal = value.ToString();
				NotifyPropertyChanged("Value");
			}
		}

		[DisplayName("Defined By")]
		public virtual string DefinedBy
		{
			get
			{
				if (testCaseDefiner == null)
					return "User";
				else
					return testCaseDefiner.Name + "(" + testCaseDefiner.ID + ")";
			}
		}

		protected TestCase testCaseDefiner;


		
	}

	

	

	
}
