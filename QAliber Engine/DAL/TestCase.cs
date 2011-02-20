using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using Microsoft.Win32;

namespace QAliber.DAL
{
	public class TestCase
	{

		public DateTime StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string AssemblyName
		{
			get { return assemblyName; }
			set { assemblyName = value; }
		}

		public string Warnings
		{
			get { return warnings; }
			set { warnings = value; }
		}

		public string Errors
		{
			get { return errors; }
			set { errors = value; }
		}

		private string warnings;
		private string errors;
		private string assemblyName;
		private string name;
		private DateTime startTime;
		

	}

	
}
