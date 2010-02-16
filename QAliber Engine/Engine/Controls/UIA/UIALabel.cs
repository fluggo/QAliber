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
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIALabel class represents a Label in a user-interface under windows OS.
	/// </summary>
	public class UIALabel : UIAControl, IText
	{
		/// <summary>
		/// Ctor to initiate a UIALabel wrapper to the UI automation HeaderItem control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation HeaderItem element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIALabel(AutomationElement element)
			: base(element)
		{

		}

		#region IText Members
		/// <summary>
		/// Return string of the text on the label.
		/// </summary>
		/// <remarks>Note: most labels dont have Text, but the string can be taken from the Name property</remarks>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion

		
	}
}
