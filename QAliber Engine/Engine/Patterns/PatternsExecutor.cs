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
using System.Windows.Automation;
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;

namespace QAliber.Engine.Patterns
{
	internal class PatternsExecutor
	{
		private PatternsExecutor()
		{
			
		}

		#region Grid Pattern
		public static AutomationElement GridGetCell(AutomationElement element, int row, int column)
		{
			try
			{
				GridPattern pattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
				if (pattern != null)
				{
					return pattern.GetItem(row, column);
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support grid", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return null;
		}

		

		public static int GridGetRowCount(AutomationElement element)
		{
			try
			{
				GridPattern pattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
				if (pattern != null)
				{
					return pattern.Current.RowCount;
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support grid", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}

		public static int GridGetColumnCount(AutomationElement element)
		{
			try
			{
				GridPattern pattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
				if (pattern != null)
				{
					return pattern.Current.ColumnCount;
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support grid", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}
		#endregion

		#region Table Pattern
		public static UIAControl TableGetCell(AutomationElement element, int row, int column)
		{
			try
			{
				TablePattern pattern = element.GetCurrentPattern(TablePattern.Pattern) as TablePattern;
				if (pattern != null)
				{
					return UIAControl.GetControlByType(pattern.GetItem(row, column));
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support table", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return null;
		}

		public static int TableGetRowCount(AutomationElement element)
		{
			try
			{
				TablePattern pattern = element.GetCurrentPattern(TablePattern.Pattern) as TablePattern;
				if (pattern != null)
				{
					return pattern.Current.RowCount;
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support table", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}

		public static int TableGetColumnCount(AutomationElement element)
		{
			try
			{
				TablePattern pattern = element.GetCurrentPattern(TablePattern.Pattern) as TablePattern;
				if (pattern != null)
				{
					return pattern.Current.ColumnCount;
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support table", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}
		#endregion

		#region Invoke Pattern
		public static void Invoke(UIAControl control)
		{
			try
			{
				InvokePattern pattern = control.UIAutomationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
				if (pattern != null)
				{
					pattern.Invoke();
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not invoke " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
				control.Click();
			}

		}
		#endregion

		#region Toggle Pattern
		public static ToggleState GetToggleState(AutomationElement element)
		{
			try
			{
				TogglePattern pattern = element.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
				if (pattern != null)
				{
					return pattern.Current.ToggleState;
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not retrieve check/uncheck state of '" + element.Current.Name + "'", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return ToggleState.Off;
		}

		public static void Toggle(AutomationElement element)
		{
			try
			{
				TogglePattern pattern = element.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
				if (pattern != null)
				{
					pattern.Toggle();
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not check/uncheck " + element.Current.LocalizedControlType + " '" + element.Current.Name + "'", "", QAliber.Logger.EntryVerbosity.Internal);
			}

		}
		#endregion

		#region Select Pattern

		public static void AddToSelection(UIAControl control)
		{
			try
			{
				SelectionItemPattern pattern = control.UIAutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
				pattern.AddToSelection();
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not modify selection for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
		}

		public static void RemoveFromSelection(UIAControl control)
		{
			try
			{
				SelectionItemPattern pattern = control.UIAutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
				pattern.RemoveFromSelection();
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not modify selection for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
		}

		public static void Select(UIAControl control)
		{
			try
			{
				SelectionItemPattern pattern = control.UIAutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
				pattern.Select();
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not select " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
				control.Click();
			}


		}

		public static void Select(UIAControl control, int index)
		{
			try
			{
				AutomationElementCollection childs = control.UIAutomationElement.FindAll(TreeScope.Descendants, BuildItemCondition(control.UIType));
				if (childs.Count > index)
				{
					SelectionItemPattern pattern = childs[index].GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
					pattern.Select();
				}
				else
					QAliber.Logger.Log.Default.Error("Could not find index " + index + " in '" + control.Name + "'", "", QAliber.Logger.EntryVerbosity.Internal);

			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not select " + control.UIType + " '" + control.Name + "'", "", QAliber.Logger.EntryVerbosity.Internal);
			}
		}

		public static void Select(UIAControl control, string name)
		{
			try
			{
				AutomationElementCollection childs = control.UIAutomationElement.FindAll(TreeScope.Descendants, itemPropertyCondition);
				foreach (AutomationElement child in childs)
				{
					if (string.Compare(child.Current.Name, name, true) == 0)
					{
						try
						{
							SelectionItemPattern pattern = child.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
							pattern.Select();
							return;
						}
						catch (InvalidOperationException)
						{
							//Could not select through the selection pattern, trying to click it
							QAliber.Logger.Log.Default.Warning("Could not find the item selectable, trying to click it", "", QAliber.Logger.EntryVerbosity.Internal);
							new UIAControl(child).Click();
							return;
						}
					}
				} 
				QAliber.Logger.Log.Default.Error("Could not find name " + name + " in '" + control.Name + "'", "", QAliber.Logger.EntryVerbosity.Internal);

			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not select " + control.UIType + " '" + control.Name + "'", "", QAliber.Logger.EntryVerbosity.Internal);
			}
		}

		public static UIAControl[] GetSelectedItems(UIAControl control)
		{
			List<UIAControl> res = new List<UIAControl>();
			try
			{
				SelectionPattern pattern = control.UIAutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
				if (pattern != null)
				{
					AutomationElement[] items = pattern.Current.GetSelection();
					foreach (AutomationElement item in items)
					{
						res.Add(UIAControl.GetControlByType(item));
					}
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not get selection for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
			return res.ToArray();


		}

		public static string[] GetItems(UIAControl control)
		{
			List<string> res = new List<string>();
			try
			{
				AutomationElementCollection items = control.UIAutomationElement.FindAll(TreeScope.Descendants, itemPropertyCondition);
				foreach (AutomationElement item in items)
				{
					res.Add(item.Current.Name);
				}
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not get selection for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
			return res.ToArray();
		}

		public static bool CanSelectMultiple(UIAControl control)
		{
			try
			{
				SelectionPattern pattern = control.UIAutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
				if (pattern != null)
					return pattern.Current.CanSelectMultiple;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not get selection data for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;
		}

		public static bool IsSeleced(UIAControl control)
		{
			try
			{
				SelectionItemPattern pattern = control.UIAutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
				if (pattern != null)
					return pattern.Current.IsSelected;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not select " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;


		}
		#endregion

		#region Scroll Pattern
		public static void Scroll(UIAControl control, double horPercent, double verPercent)
		{
			try
			{
				ScrollPattern pattern = control.UIAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
				pattern.SetScrollPercent(horPercent, verPercent);
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not scroll for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
				
			}
		}

		public static double GetHorScrollPercents(UIAControl control)
		{
			try
			{
				ScrollPattern pattern = control.UIAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
				if (pattern != null)
					return pattern.Current.HorizontalScrollPercent;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not get scroll for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
				
			}
			return 0;
		}

		public static bool CanHorScroll(UIAControl control)
		{
			try
			{
				ScrollPattern pattern = control.UIAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
				if (pattern != null)
					return pattern.Current.HorizontallyScrollable;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not get scroll for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;
		}

		public static double GetVerScrollPercents(UIAControl control)
		{
			try
			{
				ScrollPattern pattern = control.UIAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
				if (pattern != null)
					return pattern.Current.VerticalScrollPercent;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not get scroll for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
				
			}
			return 0;
		}

		public static bool CanVerScroll(UIAControl control)
		{
			try
			{
				ScrollPattern pattern = control.UIAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
				if (pattern != null)
					return pattern.Current.VerticallyScrollable;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Warning("Could not get scroll for " + control.UIType + " '" + control.Name + "'", "Trying to click it instead", QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;
		}
		#endregion

		#region Range Pattern
		public static double GetRange(AutomationElement element)
		{
			try
			{
				RangeValuePattern rngPattern = element.GetCurrentPattern(RangeValuePattern.Pattern) as RangeValuePattern;
				if (rngPattern != null)
					return rngPattern.Current.Value;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not retrieve range value for ", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}

		public static double GetRangeMax(AutomationElement element)
		{
			try
			{
				RangeValuePattern rngPattern = element.GetCurrentPattern(RangeValuePattern.Pattern) as RangeValuePattern;
				if (rngPattern != null)
					return rngPattern.Current.Maximum;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not retrieve range value for ", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}

		public static double GetRangeMin(AutomationElement element)
		{
			try
			{
				RangeValuePattern rngPattern = element.GetCurrentPattern(RangeValuePattern.Pattern) as RangeValuePattern;
				if (rngPattern != null)
					return rngPattern.Current.Minimum;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not retrieve range value for ", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			return 0;
		}
		#endregion

		#region Expand / Collapse Pattern

		public static ExpandCollapseState GetExpandCollapseState(AutomationElement element)
		{
			
			try
			{
				ExpandCollapsePattern pattern = element.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
				if (pattern != null)
					return pattern.Current.ExpandCollapseState;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support expand / collapse", "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return ExpandCollapseState.Collapsed;
			
		}

		public static void Expand(AutomationElement element)
		{
			try
			{
				ExpandCollapsePattern pattern = element.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
				if (pattern != null)
					pattern.Expand();
				else
					throw new InvalidCastException();
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "' does not seem to support expand / collapse", "", QAliber.Logger.EntryVerbosity.Internal);
			}
		}

		public static void Collapse(AutomationElement element)
		{
			try
			{
				ExpandCollapsePattern pattern = element.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
				if (pattern != null)
					pattern.Collapse();
				else
					throw new InvalidCastException();
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("'" + element.Current.Name + "'does not seem to support expand / collapse", "", QAliber.Logger.EntryVerbosity.Internal);
			}
		}

		#endregion

		#region Value / Text Pattern
		public static string GetText(AutomationElement element)
		{
			try
			{
				TextPattern txtPattern = element.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
				if (txtPattern != null)
					return txtPattern.DocumentRange.GetText(-1);
			}
			catch (InvalidOperationException)
			{
				try
				{
					ValuePattern valPattern = element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
					if (valPattern != null)
						return valPattern.Current.Value;
				}
				catch (InvalidOperationException)
				{
					QAliber.Logger.Log.Default.Error("Could not retrieve text", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
					
				}
			}
			return "";
		}

		#endregion

		#region Transform Pattern
		public static void Move(AutomationElement element, double x, double y)
		{
			try
			{
				TransformPattern pattern = element.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
				if (pattern.Current.CanMove)
					pattern.Move(x, y);
				else
					QAliber.Logger.Log.Default.Error("Control is not movable", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			catch (InvalidOperationException)
			{
				
				QAliber.Logger.Log.Default.Error("Could not move control", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
				
			}
		}

		public static void Resize(AutomationElement element, double width, double height)
		{
			try
			{
				TransformPattern pattern = element.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
				if (pattern.Current.CanResize)
					pattern.Resize(width, height);
				else
					QAliber.Logger.Log.Default.Error("Control is not resizable", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			catch (InvalidOperationException)
			{

				QAliber.Logger.Log.Default.Error("Could not resize control", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);

			}
		}

		public static void Rotate(AutomationElement element, double degrees)
		{
			try
			{
				TransformPattern pattern = element.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
				if (pattern.Current.CanRotate)
					pattern.Rotate(degrees);
				else
					QAliber.Logger.Log.Default.Error("Control is not rotatable", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			catch (InvalidOperationException)
			{

				QAliber.Logger.Log.Default.Error("Could not rotate control", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);

			}
		}

		public static bool CanMove(AutomationElement element)
		{
			try
			{
				TransformPattern pattern = element.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
				if (pattern != null)
					return pattern.Current.CanMove;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not get property", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;
		}

		public static bool CanResize(AutomationElement element)
		{
			try
			{
				TransformPattern pattern = element.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
				if (pattern != null)
					return pattern.Current.CanResize;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not get property", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;

		}

		public static bool CanRotate(AutomationElement element)
		{
			try
			{
				TransformPattern pattern = element.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
				if (pattern != null)
					return pattern.Current.CanRotate;
			}
			catch (InvalidOperationException)
			{
				QAliber.Logger.Log.Default.Error("Could not get property", element.Current.Name, QAliber.Logger.EntryVerbosity.Internal);
			}
			return false;

		}
		#endregion

		private static Condition BuildItemCondition(string type)
		{
			switch (type)
			{
				case "UIATab":
					return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
				case "UIList":
					return new OrCondition(
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem),
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
				case "UIAMenu":
					return new OrCondition(
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem),
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem));
				case "UIATree":
					return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem);
				default:
					return itemPropertyCondition;
			}
		}

		private static OrCondition itemPropertyCondition = new OrCondition(
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem),
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem),
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem),
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem),
				new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem));
	}
}
