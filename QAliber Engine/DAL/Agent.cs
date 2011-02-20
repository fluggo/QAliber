using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using Microsoft.Win32;

namespace QAliber.DAL
{
	public class Agent
	{
		public Agent()
		{
			ReadMemoryAndCPU();
		}

		public int ID
		{
			get { return id; }
			internal set { id = value; }
		}

		public string Name
		{
			get { return Environment.MachineName; }
		}

		public string OS
		{
			get { return Environment.OSVersion.VersionString; }
		}

		public int Memory
		{
			get { return memoryInMB; }
		}

		public string CPU
		{
			get { return cpu; }
		}

		public string IP
		{
			get
			{
				string hostName = Dns.GetHostName();
				IPAddress[] addresses = Dns.GetHostAddresses(hostName);
				
				if (addresses.Length > 0)
					return addresses[0].ToString();
				else
					return "";
			}
		}

		public string Model
		{
			get { return "Unknown"; }
		}

		public AgentStatusType Status
		{
			get { return status; }
			set { status = value; }
		}

		private void ReadMemoryAndCPU()
		{
			try
			{
				double totalCapacity = 0;
				cpu = String.Empty;

				ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
				ManagementObjectSearcher searcher = new ManagementObjectSearcher(objectQuery);
				ManagementObjectCollection vals = searcher.Get();
				foreach (ManagementObject val in vals)
				{
					totalCapacity += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));
				}
				memoryInMB = (int)(totalCapacity / 1048576);

				RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\SYSTEM\CentralProcessor\0");
				cpu = key.GetValue("~Mhz").ToString() + " Mhz, " + Environment.ProcessorCount + " Cores";
				key.Close();
			}
			catch
			{
				memoryInMB = 0;
				cpu = "Unknown";
			}

		}



		private int id;
		private int memoryInMB;
		private string cpu;
		private AgentStatusType status;

	}

	public enum AgentStatusType
	{
		None,
		Idle,
		Up,
		Down,
		Running
	}
}
