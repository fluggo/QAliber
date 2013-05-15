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
using System.ComponentModel;

namespace QAliber.TestModel
{
	/// <summary>
	/// Defines when to take a screenshot
	/// </summary>
	public enum TakeScreenshotOption
	{
		/// <summary>
		/// Do not take screenshots
		/// </summary>
		No,
		/// <summary>
		/// Take screenshot before the test case starts
		/// </summary>
		BeforeTestCase,
		/// <summary>
		/// Take screenshot after the test case ends
		/// </summary>
		AfterTestCase,
		/// <summary>
		/// Take screenshot before the test case starts, and after the test case ends
		/// </summary>
		Both,
		/// <summary>
		/// Take screenshot when the test case fails
		/// </summary>
		OnError
	}

	/// <summary>
	/// The execution state of a test scenario
	/// </summary>
	public enum ExecutionState
	{
		/// <summary>
		/// The test scenario was not executed (after load or new)
		/// </summary>
		NotExecuted,
		/// <summary>
		/// The test scenario is paused
		/// </summary>
		Paused,
		/// <summary>
		/// The test scenario is currently running
		/// </summary>
		InProgress,
		/// <summary>
		/// The test scenario was executed
		/// </summary>
		Executed
	}

	/// <summary>
	/// Defines the settings to record 'video' during an automated run
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class VideoOptions : ICloneable
	{
		private bool captureVideo;

		/// <summary>
		/// Gets or sets a value indicating whether we are capturing video
		/// </summary>
		[Category("Test Case Results")]
		[DisplayName("Record Video?")]
		[Description("Do you want to record the entire automation ?")]
		[DefaultValue(false)]
		public bool CaptureVideo
		{
			get { return captureVideo; }
			set { captureVideo = value; }
		}

		private int interval = 1000;

		/// <summary>
		/// Gets or set the interval (in miliseconds) between frames in the video (for performance reasons, it should be above 1000)
		/// </summary>
		[Category("Test Case Results")]
		[DisplayName("Video Capture Interval")]
		[Description("What is the interval (in milliseconds) you want to capture the frames")]
		[DefaultValue(1000)]
		public int Interval
		{
			get 
			{
				return interval;
			}
			set
			{
				if (value < 1000)
				{
					interval = 1000;
					throw new ArgumentException("Minimum interval is 1000");
				}
				interval = value;
			}
		}

		public override string ToString()
		{
			return string.Format("Enabled = {0} ; Interval = {1} ms", captureVideo, interval);
		}

		public override bool Equals( object obj ) {
			VideoOptions other = obj as VideoOptions;

			if( other == null )
				return false;

			return other.CaptureVideo == CaptureVideo && other.Interval == Interval;
		}

		public override int GetHashCode() {
			return unchecked(Interval.GetHashCode() * CaptureVideo.GetHashCode());
		}

		public object Clone() {
			return MemberwiseClone();
		}
	}
}
