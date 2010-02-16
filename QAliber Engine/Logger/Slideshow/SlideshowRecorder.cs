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
using System.Timers;
using System.IO;

namespace QAliber.Logger.Slideshow
{
	/// <summary>
	/// Provides a way to capture 'video' like consequent frames
	/// </summary>
	public class SlideshowRecorder
	{
		private SlideshowRecorder(double interval)
		{
			timer = new Timer(interval);
			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
		}

		
		/// <summary>
		/// The singleton access to the recorder
		/// </summary>
		public static SlideshowRecorder Default
		{
			get
			{
				if (recorder == null)
					recorder = new SlideshowRecorder(1000);
				return recorder;
			}
		}

		/// <summary>
		/// The time to wait, in miliseconds between consequent captures
		/// <remarks>A low interval (below 1000) could lead to performance and stability issues</remarks>
		/// </summary>
		public double Interval
		{
			get { return timer.Interval; }
			set { timer.Interval = value; }
		}

		/// <summary>
		/// Is the recorder currently recording
		/// </summary>
		public bool IsCapturing
		{
			get { return timer.Enabled; }
		}

		/// <summary>
		/// Starts taking pictures, by the specified interval
		/// <remarks>The pictures are saved in the log path under Video</remarks>
		/// </summary>
		/// <param name="name">The name of the video</param>
		public void Start(string name)
		{
			if (timer.Enabled)
				timer.Stop();
			path = Log.Default.Path + @"\Video";
			this.name = name;
			currentIndex = 0;
			Directory.CreateDirectory(path);
			timer.Start();
		}

		/// <summary>
		/// Stops takng pictures
		/// </summary>
		public void Stop()
		{
			if (timer.Enabled)
				timer.Stop();
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			string filename = path + @"\" + name + currentIndex + ".jpg";
			ScreenCapturer.Capture().Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
			currentIndex++;
		}

		private int currentIndex;
		private string path;
		private string name;
		private Timer timer;
		private static SlideshowRecorder recorder = null; 
	}
}
