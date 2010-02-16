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
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.IO;
using System.Windows.Media;
using ManagedInjector;
using System.Threading;
using System.Reflection;

namespace QAliber.Engine.Controls.WPF
{
	class WPFAUTHelpers
	{
		//TODO : chnage synch and inter process method to MMF instead of file system
		public static object TalkToAUT(IntPtr handle, string method, string param)
		{
			File.Delete(UIControlBase.tmpFile);

			if (handle != IntPtr.Zero)
			{
				Injector.Launch(handle,
					typeof(WPFAUTHelpers).Assembly.Location,
					typeof(WPFAUTHelpers).FullName, method,
					param);
			}
			else
			{
				return null;
			}

			int i = 0;
			while (!File.Exists(UIControlBase.tmpFile))
			{
				i++;
				if (i > 30)
					return null;
				Thread.Sleep(100);
			}
			using (FileStream fs = File.Open(UIControlBase.tmpFile, FileMode.Open))
			{
				fs.Position = 0;
				BinaryFormatter bf = new BinaryFormatter();
				bf.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
				try
				{
					object res = bf.Deserialize(fs);
					return res;
				}
				catch
				{
					return null;
				}

			}
		}

		public static void QueryWPF(string title)
		{

			try
			{
				if (System.Windows.Application.Current != null && System.Windows.Application.Current.MainWindow != null)
				{
					FrameworkElement obj = System.Windows.Application.Current.MainWindow as FrameworkElement;
					if (obj != null)
					{
						WPFWindow wpfControl = new WPFWindow(obj, UpdateMethod.Descendants);
						
						using (FileStream fs = File.Open(UIControlBase.tmpFile, FileMode.Create))
						{
							BinaryFormatter bf = new BinaryFormatter();
							bf.Serialize(fs, wpfControl);
						}
					}
				}
			}
			catch
			{
			}

		}

		public static void QueryWPFChildren(string runtimeID)
		{
			try
			{
				if (System.Windows.Application.Current != null && System.Windows.Application.Current.MainWindow != null)
				{
					FrameworkElement obj = System.Windows.Application.Current.MainWindow as FrameworkElement;
					if (obj != null)
					{
						int rid = int.Parse(runtimeID);
						FrameworkElement control = null;
						if (obj.GetHashCode() == rid)
							control = obj;
						else
							control = FindElementByHashCode(obj, rid);
						if (control != null)
						{
							WPFControl wpfControl = new WPFControl(control, UpdateMethod.Children);
							using (FileStream fs = File.Open(UIControlBase.tmpFile, FileMode.Create))
							{
								BinaryFormatter bf = new BinaryFormatter();
								bf.Serialize(fs, wpfControl);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		public static void QueryWPFParent(string runtimeID)
		{
			try
			{
				if (System.Windows.Application.Current != null && System.Windows.Application.Current.MainWindow != null)
				{
					FrameworkElement obj = System.Windows.Application.Current.MainWindow as FrameworkElement;
					if (obj != null)
					{
						int rid = int.Parse(runtimeID);
						FrameworkElement control = null;
						if (obj.GetHashCode() == rid)
							control = obj;
						else
							control = FindElementByHashCode(obj, rid);
						if (control != null)
						{
							FrameworkElement parent = VisualTreeHelper.GetParent(control) as FrameworkElement;
							WPFControl wpfControl = new WPFControl(parent, UpdateMethod.None);
							using (FileStream fs = File.Open(UIControlBase.tmpFile, FileMode.Create))
							{
								BinaryFormatter bf = new BinaryFormatter();
								bf.Serialize(fs, wpfControl);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		private static FrameworkElement FindElementByHashCode(FrameworkElement root, int code)
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
			{

				FrameworkElement child = VisualTreeHelper.GetChild(root, i) as FrameworkElement;
				if (child != null)
				{
					if (child.GetHashCode() == code)
						return child;
					FrameworkElement res = FindElementByHashCode(child, code);
					if (res != null)
						return res;
				}
			}
			return null;
		}
	}

	sealed class AllowAllAssemblyVersionsDeserializationBinder : System.Runtime.Serialization.SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			Type typeToDeserialize = null;

			String currentAssembly = Assembly.GetExecutingAssembly().FullName;

			// Always use the currently assembly
			assemblyName = currentAssembly;

			// Get the type using the typeName and assemblyName
			typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
				typeName, assemblyName));

			return typeToDeserialize;
		}
	}

	
}
