using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QAliber.VS2005.Plugin
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm form = new MainForm();

			Application.Run(form);



		}

	}  
}
