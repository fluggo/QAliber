using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.TestModel.Attributes
{
	public class VisualPathAttribute : Attribute
	{
		public VisualPathAttribute(string path)
		{
			this.path = path;
		}

		private string path;

		public string Path
		{
			get { return path; }
		}
	
	}
}
