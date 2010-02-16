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
