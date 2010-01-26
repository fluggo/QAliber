using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.TestModel.Variables
{
	public interface IVariable
	{
		string Name { get; set; }
		object Value { get; set; }
		TestCase Definer { get; }
		string DefinedBy { get; }
	}
}
