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
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Represents a window of an application under windows.
	/// Almost any application above the Desktop will have UIAWindow control
	/// as the root for the application.
	/// </summary>
	public class UIAWindow : UIAControl
	{
		/// <summary>
		/// Ctor to initiate a UIAWindow wrapper to the UI automation Window control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation UIAWindow element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAWindow(AutomationElement element) : base(element)
		{

		}
		/// <summary>
		/// Retrieve the state of the window
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///  //  if (notepadWin.TopMost)
		/// //	 {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		/// //	 }
		/// </code>
		/// </example>
		/// <returns>
		/// WindowVisualState:
		/// Normal = 0, Specifies that the window is normal (restored).
		/// Maximized = 1, Specifies that the window is maximized.
		/// Minimized = 2, Specifies that the window is minimized.
		/// </returns>
		[Category("Window")]
		[DisplayName("Window Visual State")]
		public WindowVisualState WindowState
		{
			get
			{
				return (WindowVisualState)automationElement.GetCurrentPropertyValue(WindowPattern.WindowVisualStateProperty);
			}
		}
		/// <summary>
		/// Retrieve the window interaction state
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///		if (notepadWin.InteractionState == System.Windows.Automation.WindowInteractionState.ReadyForUserInteraction)
		///		{
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///		}
		/// </code>
		/// </example>
		/// <returns>
		/// WindowInteractionState:
		/// Running = 0,Indicates that the window is running. This does not guarantee that the window
		///			is responding or ready for user interaction.
		/// Closing = 1, Indicates that the window is closing.
		/// ReadyForUserInteraction = 2, Indicates that the window is ready for user interaction.
		/// BlockedByModalWindow = 3, Indicates that the window is blocked by a modal window.
		/// NotResponding = 4, Indicates that the window is not responding.
		/// </returns>
		[Category("Window")]
		[DisplayName("Window Ready State")]
		public WindowInteractionState InteractionState
		{
			get
			{
				return (WindowInteractionState)automationElement.GetCurrentPropertyValue(WindowPattern.WindowInteractionStateProperty);
			}
		}
		/// <summary>
		/// Verify this window is visible above all other windows on the Desktop
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///  //  if (notepadWin.TopMost)
		/// //	 {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		/// //	 }
		/// </code>
		/// </example>
		/// <returns>
		/// true if most top window, else return false
		/// </returns>
		[Category("Window")]
		[DisplayName("Is Topmost ?")]
		public bool TopMost
		{
			get
			{
				return (bool)automationElement.GetCurrentPropertyValue(WindowPattern.IsTopmostProperty);
			}
		}

		/// <summary>
		/// Verify if this window is Modal (always on top till user close it)
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		/// <returns>true is modal, else return false</returns>
		[Category("Window")]
		[DisplayName("Is Modal ?")]
		public bool Modal
		{
			get
			{

				return (bool)automationElement.GetCurrentPropertyValue(WindowPattern.IsModalProperty);
			}
		}
		/// <summary>
		/// Set the window hight and width
		/// </summary>
		/// <param name="width">requested new width in int</param>
		/// <param name="height">requested new hight in int</param>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		public void Resize(int width, int height)
		{
			TransformPattern trans = automationElement.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
			if (trans.Current.CanResize)
				trans.Resize(width, height);
			else
				QAliber.Logger.Log.Default.Error("Window can not be resized '" + Name + "'", codePath, QAliber.Logger.EntryVerbosity.Internal);
		}
		/// <summary>
		/// Move the window to new location by x,y pixles.
		/// Top left is 0,0
		/// </summary>
		/// <param name="x">horizental position by pixles</param>
		/// <param name="y">vertical position by pixles</param>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example> 
		public void Move(int x, int y)
		{
			TransformPattern trans = automationElement.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
			if (trans.Current.CanMove)
				trans.Move(x, y);
			else
				QAliber.Logger.Log.Default.Error("Window can not be moved '" + Name + "'", codePath, QAliber.Logger.EntryVerbosity.Internal);
		}
		/// <summary>
		/// Close the window
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		public void Close()
		{
			WindowPattern wind = automationElement.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
			wind.Close();
		}
		/// <summary>
		/// Minimize the window to tray
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		public void Minimize()
		{
			WindowPattern wind = automationElement.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
			wind.SetWindowVisualState(WindowVisualState.Minimized);
		}
		/// <summary>
		/// Maximize the window
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		public void Maximize()
		{
			WindowPattern wind = automationElement.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
			wind.SetWindowVisualState(WindowVisualState.Maximized);
		}
		/// <summary>
		/// Restore the window, to its size and location when its in normal state.
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.Restore();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		public void Restore()
		{
			WindowPattern wind = automationElement.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
			wind.SetWindowVisualState(WindowVisualState.Normal);
		}
		/// <summary>
		/// Set the focus to the current window, if the window is minimized restore it first
		/// </summary>
		/// <example>
		/// <code>
		///   System.Diagnostics.Process.Start("Notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///
		///    if (!notepadWin.Modal)
		///		   notepadWin.Minimize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (notepadWin.WindowState == System.Windows.Automation.WindowVisualState.Minimized)
		///		   notepadWin.Maximize();
		///
		///    System.Threading.Thread.Sleep(2000);
		///    if (!notepadWin.Modal)
		///    {
		///		   notepadWin.SetFocus();
		///		   notepadWin.Move(10, 10);
		///		   notepadWin.Resize(500, 200);
		///		   System.Threading.Thread.Sleep(2000);
		///		   notepadWin.Close();
		///    }
		/// </code>
		/// </example>
		public override void SetFocus()
		{
			if (WindowState == WindowVisualState.Minimized)
				Restore();
			base.SetFocus();
		}
	}
	
}
