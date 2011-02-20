using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using Microsoft.Win32;

namespace QAliber.DAL
{
	public class TestCaseType
	{

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string AssembnlyName
		{
			get { return assemblyName; }
			set { assemblyName = value; }
		}

		
	
		private string name;
		private string assemblyName;
		

	}

	
}
