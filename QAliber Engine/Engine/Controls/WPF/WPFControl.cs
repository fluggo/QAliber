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
using QAliber.Engine.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Documents;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Interop;

namespace QAliber.Engine.Controls.WPF
{
	[Serializable]
	public class WPFControl : UIControlBase
	{
		public WPFControl(FrameworkElement wpfElement, UpdateMethod updateMethod)
		{
			children = new List<UIControlBase>();
			BuildFromVisual(wpfElement, updateMethod);
		}

		//public override void SetFocus()
		//{
		//	  if (UIAutomationElement != null)
		//		  base.SetFocus();
		//}

		public override string CodePath
		{
			get
			{
				if (codePath == string.Empty)
				{
					string prefix = String.Empty;
					UIControlBase parent = Parent;
					if (parent == null || parent is WPFRoot)
						prefix = "Desktop.WPF";
					else
						prefix = parent.CodePath;
					codePath = prefix + "[@\"" + Name + "\", @\"" + ClassName + "\", @\"" + ID + "\"]";
				}
				return codePath;
			}
		}

		public override string ID
		{
			get
			{
				if (!string.IsNullOrEmpty(id))
					return id;
				return className;
			}
		}

		public override List<UIControlBase> Children
		{
			get
			{
				//if (children == null)
				//{
				//	  WPFControl control = (WPFControl)WPFAUTHelpers.TalkToAUT(GetWindowHandle(), "QueryWPFChildren", runtimeID.ToString());
				//	  if (control != null)
				//	  {
				//		  children = control.Children;
				//	  }
				//}
				return children;
			}
		}

		public override UIControlBase Parent
		{
			get
			{
				//if (parent == null)
				//	  parent = (WPFControl)WPFAUTHelpers.TalkToAUT(GetWindowHandle(), "QueryWPFParent", runtimeID.ToString());
				return parent;
			}
			set
			{
				parent = value;
			}
		}

		public override void Refresh()
		{
			codePath = string.Empty;
			id = string.Empty;
			children = null;
		}

		private int runtimeID;

		public int RuntimeID
		{
			get { return runtimeID; }
			set { runtimeID = value; }
		}
	
		private void BuildFromVisual(FrameworkElement wpfElement, UpdateMethod updateMethod)
		{
			name = wpfElement.Name;
			className = wpfElement.GetType().Name;
			id = wpfElement.Uid;
			visible = wpfElement.IsVisible;
			enabled = wpfElement.IsEnabled;
			runtimeID = wpfElement.GetHashCode();
			
			layout = new Rect(wpfElement.PointToScreen(new Point(0, 0)), wpfElement.RenderSize);

			switch (updateMethod)
			{
				case UpdateMethod.None:
					break;
				case UpdateMethod.Children:
					UpdateDescendants(wpfElement, UpdateMethod.None);
					break;
				case UpdateMethod.Descendants:
					UpdateDescendants(wpfElement, updateMethod);
					break;
				default:
					break;
			}
		   
		}

		private void UpdateDescendants(FrameworkElement wpfElement, UpdateMethod updateMethod)
		{
			StoreProperties(wpfElement);
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(wpfElement); i++)
			{
				
				FrameworkElement child = VisualTreeHelper.GetChild(wpfElement, i) as FrameworkElement;
				if (child != null)
				{
					WPFControl wpfChild = new WPFControl(child, updateMethod);
					children.Add(wpfChild);
					wpfChild.parent = this;
				}
			}
		}

		private void StoreProperties(FrameworkElement wpfElement)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(wpfElement);
			foreach (PropertyDescriptor prop in props)
			{
				if (prop.PropertyType.IsSerializable 
					&& prop.PropertyType.FullName.StartsWith("System")
					&& !prop.PropertyType.FullName.Contains("Uri"))
				{
					object obj = prop.GetValue(wpfElement);
					if (obj != null)
					{
						try
						{
							//Trying to serialize it to see it fits
							using (MemoryStream ms = new MemoryStream())
							{
								BinaryFormatter bf = new BinaryFormatter();
								bf.Serialize(ms, obj);
							}
							extendedProperties.Add(prop.Name, obj);
						}
						catch (System.Runtime.Serialization.SerializationException)
						{
						}
					}
				   
				}
			}
		}

		private IntPtr GetWindowHandle()
		{
			UIControlBase control = this;
			while (control != null)
			{
				if (control is WPFWindow)
					return new IntPtr(((WPFWindow)control).Handle);
				control = control.Parent;
			}
			return IntPtr.Zero;
		}

	}

	public enum UpdateMethod
	{
		None,
		Children,
		Descendants
	}
}
