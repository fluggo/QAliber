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
using QAliber.Recorder;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;
using QAliber.Engine;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace QAliber.VS2005.Plugin.Commands
{
	public abstract class Command
	{
		
		public event EventHandler Invoked;

		protected void OnInvoke()
		{
			if (Invoked != null)
				Invoked(this, EventArgs.Empty);
		}

		public abstract void Invoke();
		
	}

	public enum CommandType
	{
		Record = 0,
		StopRecord
	}


}
