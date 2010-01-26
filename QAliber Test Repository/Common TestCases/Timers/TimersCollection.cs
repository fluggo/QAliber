using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.Repository.CommonTestCases.Timers
{
	public class TimersCollection
	{

		public static void AddTimer(string key)
		{
			timers.Add(key, DateTime.Now);
		}

		public static double StopTimer(string key)
		{
			if (!timers.ContainsKey(key))
				throw new ArgumentException("Cannot find a timer with key " + key);
			TimeSpan span = DateTime.Now - timers[key];
			timers.Remove(key);
			return span.TotalMilliseconds;

		}

		private static Dictionary<string, DateTime> timers = new Dictionary<string,DateTime>();
	}
}
