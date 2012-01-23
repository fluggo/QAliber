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
using System.Xml.Serialization;

namespace QAliber.TestModel.Variables
{
	[Serializable]
	public sealed class ScenarioVariable<T> : INotifyPropertyChanged {
		private string _name;
		private T _value;
		private TestCase _testStep;

		public ScenarioVariable() {
		}

		public ScenarioVariable( string name, T value, TestCase testStep ) {
			_name = name;
			_value = value;
			_testStep = testStep;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name) {
			PropertyChangedEventHandler handler = PropertyChanged;

			if( handler != null )
				handler( this, new PropertyChangedEventArgs( name ) );
		}

		#endregion

		public string Name {
			get { return _name; }
			set {
				_name = value;
				NotifyPropertyChanged("Name");
			}
		}

		[DisplayName("Defined By")]
		[ReadOnly(true)]
		public TestCase TestStep {
			get { return _testStep; }
		}

		public T Value {
			get { return _value; }
			set {
				_value = value;
				NotifyPropertyChanged("Value");
			}
		}
	}
}
