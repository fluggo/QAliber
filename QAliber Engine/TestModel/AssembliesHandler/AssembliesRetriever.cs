using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace QAliber.TestModel
{
	public class AssembliesRetriever
	{
		public AssembliesRetriever(bool silent)
		{
			this.silent = silent;
			InitAssemblies();
		}

		public void CopyRemoteToLocal()
		{
			foreach (AssemblyName remFile in remoteAssemblies)
			{
				bool found = false;
				try
				{
					foreach (AssemblyName locFile in localAssemblies)
					{
						if (string.Compare(remFile.Name, Path.GetFileName(locFile.Name), true) == 0)
						{
							found = true;
							if (GenerateBuildNumber(remFile) > GenerateBuildNumber(locFile))
							{
								File.Copy(new Uri(remFile.CodeBase).LocalPath, new Uri(locFile.CodeBase).LocalPath, true);
								
								break;
							}
						}
					}
					if (!found)
					{
						File.Copy(new Uri(remFile.CodeBase).LocalPath, Path.Combine(TestController.Default.LocalAssemblyPath, Path.GetFileName(new Uri(remFile.CodeBase).LocalPath)), true);
					}
				}
				catch (IOException)
				{
					if (!silent)
					{
						System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("It seems some updates requires the application to restart.\nDo you want to restart the application", "Restart Application ?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
						if (dialogResult == System.Windows.Forms.DialogResult.Yes)
						{
							System.Windows.Forms.Application.Exit();
							Process.Start(System.Windows.Forms.Application.ExecutablePath, "-update");

						}
						break;
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Copy Assemblies Error\n" + ex.Message);
				}
			}
			//Copy macro files and xml files
			ProcessStartInfo psi = new ProcessStartInfo("xcopy",
				string.Format("\"{0}\\*.macro\" \"{1}\\Macros\" /c /i /s /y", TestController.Default.RemoteAssemblyDirectory, TestController.Default.LocalAssemblyPath));
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			Process.Start(psi).WaitForExit(10000);
			psi.Arguments =  string.Format("\"{0}\\*.xml\" \"{1}\\Help\" /c /i /s /y", TestController.Default.RemoteAssemblyDirectory, System.Windows.Forms.Application.StartupPath);
			Process.Start(psi).WaitForExit(10000);
		}

		private void InitAssemblies()
		{
			localAssemblies = new List<AssemblyName>();
			remoteAssemblies = new List<AssemblyName>();
			
			foreach (string path in Directory.GetFiles(TestController.Default.LocalAssemblyPath, "*.dll"))
			{
				localAssemblies.Add(TryToGetAssemblyName(path));
			}
			foreach (string path in Directory.GetFiles(TestController.Default.LocalAssemblyPath, "*.exe"))
			{
				localAssemblies.Add(TryToGetAssemblyName(path));
			}

			foreach (string path in Directory.GetFiles(TestController.Default.RemoteAssemblyDirectory, "*.dll"))
			{
				remoteAssemblies.Add(TryToGetAssemblyName(path));

			}
			foreach (string path in Directory.GetFiles(TestController.Default.RemoteAssemblyDirectory, "*.exe"))
			{
				remoteAssemblies.Add(TryToGetAssemblyName(path));
			}
			
			
		}

		private double GenerateBuildNumber(AssemblyName info)
		{
			return info.Version.Revision +
				info.Version.Build * Math.Pow(10, 6) +
				info.Version.Minor * Math.Pow(10, 9) +
				info.Version.Major * Math.Pow(10, 12);
		}

		private AssemblyName TryToGetAssemblyName(string path)
		{
			AssemblyName res = null;
			try
			{
				res = AssemblyName.GetAssemblyName(path);
			}
			catch (FileNotFoundException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return res;
		}

		private bool silent;
		private List<AssemblyName> localAssemblies;
		private List<AssemblyName> remoteAssemblies;
	}
}
