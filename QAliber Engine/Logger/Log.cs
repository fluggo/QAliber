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
using System.Drawing;

namespace QAliber.Logger
{
	/// <summary>
	/// Handles all logging activity
	/// </summary>
	public class Log : IDisposable
	{
		private Log()
		{
		}

		/// <summary>
		/// Logs an information entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		/// <param name="style">The style of the message</param>
		public void Info(string message, string extra, EntryVerbosity verbosity, EntryStyle style)
		{
			if (enabled)
			{
				LogEntry entry = new LogEntry();
				entry.Time = DateTime.Now;
				entry.Message = message;
				entry.ExtendedMessage = extra;
				entry.Body = BodyType.Text;
				entry.Type = EntryType.Info;
				entry.Verbosity = verbosity;
				entry.Style = style;

				entry.ToXml(writer, indents);
				writer.Flush();
			}

		}

		/// <summary>
		/// Logs an information entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		public void Info(string message, string extra, EntryVerbosity verbosity)
		{
			Info(message, extra, verbosity, new EntryStyle(verbosity));
		}

		/// <summary>
		/// Logs an information entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		public void Info(string message, string extra)
		{
			Info(message, extra, EntryVerbosity.Normal, new EntryStyle());
		}

		/// <summary>
		/// Logs an information entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Info(string message)
		{
			Info(message, "", EntryVerbosity.Normal, new EntryStyle());

		}

		/// <summary>
		/// Logs a warning entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		/// <param name="style">The style of the message</param>
		public void Warning(string message, string extra, EntryVerbosity verbosity, EntryStyle style)
		{
			if (enabled)
			{
				LogEntry entry = new LogEntry();
				entry.Time = DateTime.Now;
				entry.Message = message;
				entry.ExtendedMessage = extra;
				entry.Body = BodyType.Text;
				entry.Type = EntryType.Warning;
				entry.Verbosity = verbosity;
				entry.Style = style;

				if (BeforeWarningIsPosted != null)
					BeforeWarningIsPosted(this, new LogEventArgs(entry));
				if (entry.Enabled)
				{
					entry.ToXml(writer, indents);
					writer.Flush();
				}
			}

		}

		/// <summary>
		/// Logs a warning entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		public void Warning(string message, string extra, EntryVerbosity verbosity)
		{
			Warning(message, extra, verbosity, new EntryStyle(verbosity));
		}

		/// <summary>
		/// Logs a warning entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		public void Warning(string message, string extra)
		{
			Warning(message, extra, EntryVerbosity.Normal, new EntryStyle());
		}

		/// <summary>
		/// Logs a warning entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Warning(string message)
		{
			Warning(message, "", EntryVerbosity.Normal, new EntryStyle());

		}

		/// <summary>
		/// Logs an error entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		/// <param name="style">The style of the message</param>
		public void Error(string message, string extra, EntryVerbosity verbosity, EntryStyle style)
		{
			if (enabled)
			{
				LogEntry entry = new LogEntry();
				entry.Time = DateTime.Now;
				entry.Message = message;
				entry.ExtendedMessage = extra;
				entry.Body = BodyType.Text;
				entry.Type = EntryType.Error;
				entry.Verbosity = verbosity;
				entry.Style = style;

				if (BeforeErrorIsPosted != null)
					BeforeErrorIsPosted(this, new LogEventArgs(entry));

				if (entry.Enabled)
				{
					entry.ToXml(writer, indents);
					writer.Flush();
				}
			}

		}

		/// <summary>
		/// Logs an error entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		public void Error(string message, string extra, EntryVerbosity verbosity)
		{
			Error(message, extra, verbosity, new EntryStyle(verbosity));
		}

		/// <summary>
		/// Logs an error entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		public void Error(string message, string extra)
		{
			Error(message, extra, EntryVerbosity.Normal, new EntryStyle());
		}

		/// <summary>
		/// Logs an error entry to the log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Error(string message)
		{
			Error(message, "", EntryVerbosity.Normal, new EntryStyle());

		}

		/// <summary>
		/// Logs a picture entry to the log
		/// </summary>
		/// <param name="image">The image to log</param>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		/// <param name="style">The style of the message</param>
		public void Image(Image image, string message, string extra, EntryVerbosity verbosity, EntryStyle style)
		{
			if (enabled)
			{
				LogEntry entry = new LogEntry();
				entry.Time = DateTime.Now;
				entry.Message = message;
				entry.ExtendedMessage = extra;
				entry.Body = BodyType.Picture;
				entry.Type = EntryType.Info;
				entry.Verbosity = verbosity;
				entry.Style = style;

				string imageFile = System.IO.Path.GetDirectoryName(filename) + @"\image" + imageCounter + ".jpg";
				image.Save(imageFile, System.Drawing.Imaging.ImageFormat.Jpeg);
				entry.Link = imageFile;
				imageCounter++;

				entry.ToXml(writer, indents);
				writer.Flush();
			}

		}

		/// <summary>
		/// Logs a picture entry to the log
		/// </summary>
		/// <param name="image">The image to log</param>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		public void Image(Image image, string message, string extra, EntryVerbosity verbosity)
		{
			Image(image, message, extra, verbosity, new EntryStyle(verbosity));
		}

		/// <summary>
		/// Logs a picture entry to the log
		/// </summary>
		/// <param name="image">The image to log</param>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		public void Image(Image image, string message, string extra)
		{
			Image(image, message, extra, EntryVerbosity.Normal, new EntryStyle());
		}

		/// <summary>
		/// Logs a picture entry to the log
		/// </summary>
		/// <param name="image">The image to log</param>
		/// <param name="message">The message to log</param>
		public void Image(Image image, string message)
		{
			Image(image, message, "", EntryVerbosity.Normal, new EntryStyle());

		}

		/// <summary>
		/// Logs a link entry to the log
		/// </summary>
		/// <param name="link">The link to launch</param>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		/// <param name="style">The style of the message</param>
		public void Link(string link, string message, string extra, EntryVerbosity verbosity, EntryStyle style)
		{
			if (enabled)
			{
				LogEntry entry = new LogEntry();
				entry.Time = DateTime.Now;
				entry.Message = message;
				entry.ExtendedMessage = extra;
				entry.Body = BodyType.Link;
				entry.Type = EntryType.Info;
				entry.Verbosity = verbosity;
				entry.Style = style;
				entry.Link = link;

				entry.ToXml(writer, indents);
				writer.Flush();
			}

		}

		/// <summary>
		/// Logs a link entry to the log
		/// </summary>
		/// <param name="link">The link to launch</param>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="verbosity">The verbosity of the message</param>
		public void Link(string link, string message, string extra, EntryVerbosity verbosity)
		{
			Link(link, message, extra, verbosity, new EntryStyle(verbosity));
		}

		/// <summary>
		/// Logs a link entry to the log
		/// </summary>
		/// <param name="link">The link to launch</param>
		/// <param name="message">The message to log</param>
		/// <param name="extra">An additional info to log</param>
		public void Link(string link, string message, string extra)
		{
			Link(link, message, extra, EntryVerbosity.Normal, new EntryStyle());
		}

		/// <summary>
		/// Logs a link entry to the log
		/// </summary>
		/// <param name="link">The link to launch</param>
		/// <param name="message">The message to log</param>
		public void Link(string link, string message)
		{
			Link(link, message, "", EntryVerbosity.Normal, new EntryStyle());

		}

		/// <summary>
		/// Creates a folder in the log (for structured report)
		/// </summary>
		/// <param name="message">The message to show on the folder</param>
		public void IndentIn(string message)
		{
			IndentIn(message, "");
		}

		/// <summary>
		/// Creates a folder in the log (for structured report)
		/// </summary>
		/// <param name="message">The message to show on the folder</param>
		/// <param name="extra">An additional info to log</param>
		/// <param name="isTestCase">For internal use, this should always be set to false</param>
		public void IndentIn(string message, string extra, bool isTestCase)
		{
			if (enabled)
			{
				string tabs = "";
				for (int i = 0; i < indents + 1; i++)
				{
					tabs += "\t";
				}
				if (isTestCase)
					writer.WriteLine(tabs + "<TestCase>" + System.Security.SecurityElement.Escape(message) + "</TestCase>");
				writer.WriteLine(tabs + "<ChildEntries>");
				
				indents++;
				Info(message, extra);
			}
		}

		/// <summary>
		/// Creates a folder in the log (for structured report)
		/// </summary>
		/// <param name="message">The message to show on the folder</param>
		/// <param name="extra">An additional info to log</param>
		public void IndentIn(string message, string extra)
		{
			IndentIn(message, extra, false);
		}

		/// <summary>
		/// Close a floder
		/// </summary>
		public void IndentOut()
		{
			if (enabled && indents > 0)
			{
				string tabs = "";
				for (int i = 0; i < indents; i++)
				{
					tabs += "\t";
				}
				writer.WriteLine(tabs + "</ChildEntries>");
				indents--;
				writer.Flush();
			}
		}

		/// <summary>
		/// Logs the result of a testcase
		/// <remarks>This method should not be called outside of QAliber engine</remarks>
		/// </summary>
		/// <param name="result">The result of the test case</param>
		public void Result(QAliber.RemotingModel.TestCaseResult result)
		{
			string tabs = "";
			for (int i = 0; i < indents + 1; i++)
			{
				tabs += "\t";
			}
			lock (this)
			{
				writer.WriteLine(tabs + "<LogResult>" + result.ToString() + "</LogResult>");
				writer.Flush();
			   
			}
		}

		#region IDisposable Members

		/// <summary>
		/// Saves the log file, if not saved and cleans any resources associated with the log
		/// </summary>
		public void Dispose()
		{
			try
			{
				lock (this)
				{
					while (indents > 0)
						IndentOut();
					writer.WriteLine("</LogEntries>");
					writer.Close();
				}
			}
			catch { }
		}

		#endregion

		/// <summary>
		/// The singleton access to the log
		/// </summary>
		public static Log Default
		{
			get 
			{
				if (instance == null)
					instance = new Log();
				return instance; 
			}
			
		}

		/// <summary>
		/// The filename of the log
		/// <remarks>
		/// This property must be set prior to all logging
		/// Changing this property will cause all prior logging to be saved with the old filename, and all subsequent logging to be logged to the newly set filename
		/// </remarks>
		/// </summary>
		public string Filename
		{
			get { return filename; }
			set 
			{ 
				filename = value;
				if (writer != null)
					Dispose();
				writer = new StreamWriter(filename);
				writer.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
				writer.WriteLine("<LogEntries>");
			}
		}

		/// <summary>
		/// The path of the log
		/// </summary>
		public string Path
		{
			get
			{
				return System.IO.Path.GetDirectoryName(filename);
			}
		}

		/// <summary>
		/// Turns logging on and off 
		/// If Enabled = false, all logging methods will be ignored
		/// </summary>
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		/// <summary>
		/// This event is fired before an error is posted to the log
		/// </summary>
		public event EventHandler<LogEventArgs> BeforeErrorIsPosted;

		/// <summary>
		/// This event is fired before a warning is posted to the log
		/// </summary>
		public event EventHandler<LogEventArgs> BeforeWarningIsPosted; 

		private bool enabled = true;
		private int indents = 0;
		private int imageCounter = 0;
		private string filename = "";
		private StreamWriter writer = null;
		private static Log instance = null;
		
	}

	/// <summary>
	/// The arguments to pass to a logging event
	/// </summary>
	public class LogEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes new instance with a log entry to be passed
		/// </summary>
		/// <param name="entry">The log entry to pass to the callback</param>
		public LogEventArgs (LogEntry entry)
		{
				this.entry = entry;
		}

		private LogEntry entry;

		/// <summary>
		/// Gets the log entry the event fired with
		/// </summary>
		public LogEntry LogEntryProperties
		{
			get { return entry;}
		}
	
	}

	
}
