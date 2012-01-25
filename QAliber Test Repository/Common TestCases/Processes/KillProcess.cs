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

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml.Serialization;



namespace QAliber.Repository.CommonTestCases.Processes
{
	/// <summary>
	/// Kills a process
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Processes")]
	[XmlType(TypeName="KillProcess", Namespace=Util.XmlNamespace)]
	public class KillProcess : global::QAliber.TestModel.TestCase
	{
		public KillProcess() : base( "Kill Process" )
		{
			icon = Properties.Resources.ProcessRemove;
		}

		public override void Body()
		{
			Process[] processes = Process.GetProcessesByName(filename);
			if (processes.Length > 0)
			{
				if (killAll)
				{
					foreach (Process p in processes)
					{
						p.Kill();
					}
				}
				else
					processes[0].Kill();
				
			}
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string filename = "";

		/// <summary>
		/// The name of the process to kill
		/// </summary>
		[Category("Process")]
		[Description("The process to kill")]
		[DisplayName("1) Process Name")]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		private bool killAll = true;

		/// <summary>
		/// If multiple processes exist with the same name, kill them all ?
		/// </summary>
		[Category("Process")]
		[DisplayName("2) Kill All Processes With Same Name ?")]
		public bool KillAll
		{
			get { return killAll; }
			set { killAll = value; }
		}
	

		
		public override string Description
		{
			get
			{
				return "Killing process '" + filename + "'";
			}
		}
	
	
	
	}
}
