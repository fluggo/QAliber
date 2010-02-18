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



namespace QAliber.Repository.CommonTestCases.Processes
{
	/// <summary>
	/// Logs CPU, memory and virtual memory of a process to the test log
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Processes")]
	public class LogResources : global::QAliber.TestModel.TestCase
	{
		public LogResources()
		{
			name = "Log Process Resources";
			icon = Properties.Resources.StartProcess;
		}

		public override void Body()
		{
			Process[] processes = Process.GetProcessesByName(filename);
			if (processes.Length > 0)
			{
				Process process = processes[0];
				cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
				cpu = cpuCounter.NextValue();
				System.Threading.Thread.Sleep(100);
				cpu = cpuCounter.NextValue();
				System.Threading.Thread.Sleep(100);
				cpu = cpuCounter.NextValue();
				
				virtualMemory = process.PagedMemorySize64 / 1024f;
				memory = process.WorkingSet64 / 1024f;
				cpu = cpuCounter.NextValue();
				Log.Default.Info(string.Format("CPU = {0:0.00}%", cpu));
				Log.Default.Info(string.Format("Physical Memory = {0} KB", (int)memory));
				Log.Default.Info(string.Format("Virtual Memory = {0} KB", (int)virtualMemory));

			}
			else
				Log.Default.Warning("Process is not available");
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string filename = "";

		/// <summary>
		/// The process name to monitor
		/// </summary>
		[Category("Process")]
		[Description("The process to monitor")]
		[DisplayName("1) Process")]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		private double cpu;

		/// <summary>
		/// Read only - the CPU usage in percents
		/// </summary>
		[Category("Process")]
		[Description("The process's CPU usage in %")]
		[DisplayName("2) CPU")]
		public double CPU
		{
			get { return cpu; }
		}

		private double memory;

		/// <summary>
		/// Read only - the memory usage in KB
		/// </summary>
		[Category("Process")]
		[Description("The process's memory usage (in KB)")]
		[DisplayName("3) Memory")]
		public double Memory
		{
			get { return memory; }
		}

		/// <summary>
		/// Read only - the virtual memory usage in KB
		/// </summary>
		private double virtualMemory;

		[Category("Process")]
		[Description("The process's virtual memory usage (in KB)")]
		[DisplayName("4) Virtual Memory")]
		public double VirtualMemory
		{
			get { return virtualMemory; }
		}

		[NonSerialized]
		private PerformanceCounter cpuCounter;

		public override string Description
		{
			get
			{
				return "Logging usage for process '" + filename + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}
