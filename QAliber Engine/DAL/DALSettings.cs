using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.DAL
{
	
	internal sealed partial class Settings {

		private static Settings defaultInstance = new Settings();

		public Settings()
		{
			if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey) == null)
				subKey = @"Software\Wow6432Node\QAlibers";
		}
		
		public static Settings Default {
			get {
				return defaultInstance;
			}
		}
		
		public string AutomationConnectionString {
			get {
				try
				{
					string dataSource = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey).GetValue("DataSource").ToString();

					string userID = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey).GetValue("User ID").ToString();

					string pwd = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey).GetValue("Password").ToString();
				   
					return string.Format("Data Source={0};Initial Catalog=Automation;Integrated Security=False;User ID={1};Password={2}",
						dataSource, userID, pwd);
				}
				catch
				{
					return string.Empty;
				}
			}
		}

		private string subKey = @"SOFTWARE\QAlibers";

	}
}


