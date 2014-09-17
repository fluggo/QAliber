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
						if(locFile == null)
							continue;

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
