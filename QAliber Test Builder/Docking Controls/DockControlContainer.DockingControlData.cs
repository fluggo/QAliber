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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Darwen.Windows.Forms.General;

namespace Darwen.Windows.Forms.Controls.Docking
{
	partial class DockControlContainer
	{
		private class DockingControlData
		{
			private ToolStripButton _toolStripButton;
			private AutoHideChangedHandler _autoHideHandler;
			private CancelledChangedHandler _cancelledChangedHandler;

			public DockingControlData(ToolStripButton button, AutoHideChangedHandler autoHideHandler, CancelledChangedHandler cancelledChangedHandler)
			{
				_toolStripButton = button;
				_autoHideHandler = autoHideHandler;
				_cancelledChangedHandler = cancelledChangedHandler;
			}

			public ToolStripButton Button
			{
				get
				{
					return _toolStripButton;
				}
			}

			public AutoHideChangedHandler AutoHideChangedHandler
			{
				get
				{
					return _autoHideHandler;
				}
			}

			public CancelledChangedHandler CancelledChangedHandler
			{
				get
				{
					return _cancelledChangedHandler;
				}
			}
		}
	}
}
