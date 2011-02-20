using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using Microsoft.Win32;

namespace QAliber.DAL
{
	public class Scenario
	{
		public int ID
		{
			get { return id; }
			internal set { id = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}


		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		public string Attachments
		{
			get { return attachments; }
			set { attachments = value; }
		}

		
	  
		private int id;
		private string name;
		private string filename;
		private string description;
		private string attachments;
	}
}
