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
using ManagedInjector;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace QAliber.Engine.Controls.WPF
{
	[Serializable]
	public class WPFRoot : UIControlBase, IControlLocator
	{
		public WPFRoot()
		{
			
		}

		public override string CodePath
		{
			get
			{
				return "Desktop.WPF";
			}
		}

		public override List<UIControlBase> Children
		{
			get
			{

				if (children == null)
				{
					children = new List<UIControlBase>();
					foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
					{
						if (p.ProcessName != "System" && !p.ProcessName.Contains("devenv") && !p.ProcessName.Contains("vshost"))
						{
							try
							{
								foreach (System.Diagnostics.ProcessModule m in p.Modules)
								{
									if (m.ModuleName.Contains("PresentationFramework.dll") ||
										m.ModuleName.Contains("PresentationFramework.ni.dll"))
									{
										WPFWindow c = (WPFWindow)WPFAUTHelpers.TalkToAUT(p.MainWindowHandle, "QueryWPF", p.MainWindowTitle);
										c.Handle = p.MainWindowHandle.ToInt32();
										if (c != null)
										{
											children.Add(c);
											c.Parent = this;
										}
									}
								}
							}
							catch (Win32Exception)
							{
							}
						}
					}
					GetLayouts();
					
				}
				return children;

			}
		}

		#region IControlLocator Members

		public UIControlBase GetControlFromCursor()
		{
			System.Windows.Point pt = new System.Windows.Point(System.Windows.Forms.Cursor.Position.X,
							  System.Windows.Forms.Cursor.Position.Y);
			return GetControlFromPoint(pt);
		}

		public UIControlBase GetControlFromPoint(Point pt)
		{
			for (int i = 0; i < layouts.Count; i++)
			{
				if (layouts[i].Layout.Contains(pt))
					return layouts[i];
			}
			return null;
		}

		public UIControlBase GetFocusedElement()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		private void GetLayouts()
		{
			layouts = new List<UIControlBase>();
			GetLayoutsRec(this);
			layouts.Sort(new RectAreaSorter());

		}

		private void GetLayoutsRec(UIControlBase parent)
		{
			foreach (UIControlBase c in parent.Children)
			{
				layouts.Add(c);
				GetLayoutsRec(c);
			}
		}

		private List<UIControlBase> layouts;
		
	}

	class RectAreaSorter : IComparer<UIControlBase>
	{
		#region IComparer<Rect> Members

		public int Compare(UIControlBase x, UIControlBase y)
		{
			return (int)(x.Layout.Width * x.Layout.Width + x.Layout.Height * x.Layout.Height
				- (y.Layout.Width * y.Layout.Width + y.Layout.Height * y.Layout.Height));
		}

		#endregion
	}
}
