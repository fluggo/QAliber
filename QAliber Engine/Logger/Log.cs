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
using System.Globalization;
using QAliber.RemotingModel;
using System.Xml;
using System.Net;

namespace QAliber.Logger
{
	/// <summary>
	/// Handles all logging activity
	/// </summary>
	public class Log : IDisposable
	{
		string _filename;
		XmlWriter _writer;
		int _imageCount = 0;

		public Log( string filename, string scenarioFile, bool partialRun ) {
			_filename = filename;

			XmlWriterSettings settings = new XmlWriterSettings() {
				Encoding = Encoding.UTF8,
				Indent = true,
				CloseOutput = true
			};

			_writer = XmlWriter.Create( filename, settings );
			_writer.WriteStartDocument();
			_writer.WriteStartElement( "Log" );

			if( scenarioFile != null )
				_writer.WriteAttributeString( "scenario", scenarioFile );

			_writer.WriteAttributeString( "partialRun", partialRun ? "true" : "false" );
			_writer.WriteAttributeString( "startTimeUtc", DateTime.UtcNow.ToString( "s", CultureInfo.InvariantCulture ) );
			_writer.WriteAttributeString( "machine", Dns.GetHostName() );
		}

		/// <summary>
		/// Writes a text entry to the log.
		/// </summary>
		/// <param name="type">The type of message.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public void WriteEntry( EntryType type, string message, string details, EntryVerbosity verbosity ) {
			LogEntry entry = new LogEntry();
			entry.Time = DateTime.UtcNow;
			entry.Message = message;
			entry.ExtendedMessage = details;
			entry.Body = BodyType.Text;
			entry.Type = type;
			entry.Verbosity = verbosity;

			EventHandler<LogEventArgs> handler = null;

			if( type == EntryType.Warning )
				handler = BeforeWarningIsPosted;
			else if( type == EntryType.Error )
				handler = BeforeErrorIsPosted;

			if( handler != null )
				handler( this, new LogEventArgs( entry ) );

			if( entry.Enabled ) {
				entry.ToXml(_writer);
				_writer.Flush();
			}
		}

		/// <summary>
		/// Writes an image entry to the log.
		/// </summary>
		/// <param name="image">The image to log.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public void WriteImageEntry( Image image, string message, string details, EntryVerbosity verbosity ) {
			LogEntry entry = new LogEntry();
			entry.Time = DateTime.UtcNow;
			entry.Message = message;
			entry.ExtendedMessage = details;
			entry.Body = BodyType.Picture;
			entry.Type = EntryType.Info;
			entry.Verbosity = verbosity;

			string imageFile = Path + @"\image" + _imageCount.ToString( CultureInfo.InvariantCulture ) + ".png";
			image.Save(imageFile, System.Drawing.Imaging.ImageFormat.Png);
			entry.Link = imageFile;
			_imageCount++;

			entry.ToXml(_writer);
			_writer.Flush();
		}

		/// <summary>
		/// Writes a link entry to the log.
		/// </summary>
		/// <param name="link">The link to log.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public void WriteLinkEntry( string link, string message, string details, EntryVerbosity verbosity ) {
			LogEntry entry = new LogEntry();
			entry.Time = DateTime.UtcNow;
			entry.Message = message;
			entry.ExtendedMessage = details;
			entry.Body = BodyType.Link;
			entry.Type = EntryType.Info;
			entry.Verbosity = verbosity;
			entry.Link = link;

			entry.ToXml(_writer);
			_writer.Flush();
		}

		/// <summary>
		/// Creates a new level in the test log.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		public void StartFolder( string message, string details ) {
			_writer.WriteStartElement( "Folder" );
			WriteEntry( EntryType.Info, message, details, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Ends a level in the test log.
		/// </summary>
		public void EndFolder() {
			_writer.WriteEndElement();
			_writer.Flush();
		}

		/// <summary>
		/// Starts a test step in the log. The step should be finished with <see cref="WriteResult"/> followed by <see cref="EndFolder"/>.
		/// </summary>
		///	<param name="namespace">The namespace of the test step.</param>
		/// <param name="type">The type of the test step.</param>
		/// <param name="name">The name of the test step.</param>
		/// <param name="description">A description of what the test step does.</param>
		public void StartTestStep( string @namespace, string type, string name, string description ) {
			_writer.WriteStartElement( "TestStep" );
			_writer.WriteAttributeString( "namespace", @namespace );
			_writer.WriteAttributeString( "type", type );
			_writer.WriteAttributeString( "name", name );

			if( !string.IsNullOrEmpty( description ) )
				_writer.WriteAttributeString( "description", description );

			_writer.WriteAttributeString( "startTimeUtc", DateTime.UtcNow.ToString( "s", CultureInfo.InvariantCulture ) );
		}

		/// <summary>
		/// Writes an info log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public static void Info( string message, string details, EntryVerbosity verbosity ) {
			Log log = _currentLog;

			if( log != null )
				log.WriteEntry( EntryType.Info, message, details, verbosity );
		}

		/// <summary>
		/// Writes an info log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		public static void Info( string message, string details ) {
			Info( message, details, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes an info log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		public static void Info( string message ) {
			Info( message, null, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes a warning log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public static void Warning( string message, string details, EntryVerbosity verbosity ) {
			Log log = _currentLog;

			if( log != null )
				log.WriteEntry( EntryType.Warning, message, details, verbosity );
		}

		/// <summary>
		/// Writes a warning log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		public static void Warning( string message, string details ) {
			Warning( message, details, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes a warning log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		public static void Warning( string message ) {
			Warning( message, null, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes an error log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public static void Error( string message, string details, EntryVerbosity verbosity ) {
			Log log = _currentLog;

			if( log != null )
				log.WriteEntry( EntryType.Error, message, details, verbosity );
		}

		/// <summary>
		/// Writes an error log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		public static void Error( string message, string details ) {
			Error( message, details, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes an error log entry to the current log, if any.
		/// </summary>
		/// <param name="message">The message for this entry.</param>
		public static void Error( string message ) {
			Error( message, null, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes an image entry to the current log, if any.
		/// </summary>
		/// <param name="image">The image to log.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public static void Image( Image image, string message, string details, EntryVerbosity verbosity ) {
			Log log = _currentLog;

			if( log != null )
				log.WriteImageEntry( image, message, details, verbosity );
		}

		/// <summary>
		/// Writes an image entry to the current log, if any.
		/// </summary>
		/// <param name="image">The image to log.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		public static void Image( Image image, string message, string details ) {
			Image( image, message, details, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes an image entry to the current log, if any.
		/// </summary>
		/// <param name="image">The image to log.</param>
		/// <param name="message">The message for this entry.</param>
		public static void Image( Image image, string message ) {
			Image( image, message, null, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes a link log entry to the current log, if any.
		/// </summary>
		/// <param name="link">The link to log.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		/// <param name="verbosity">The verbosity level of this entry.</param>
		public static void Link( string link, string message, string details, EntryVerbosity verbosity ) {
			Log log = _currentLog;

			if( log != null )
				log.WriteLinkEntry( link, message, details, verbosity );
		}

		/// <summary>
		/// Writes a link log entry to the current log, if any.
		/// </summary>
		/// <param name="link">The link to log.</param>
		/// <param name="message">The message for this entry.</param>
		/// <param name="details">Optional detailed information for this entry.</param>
		public static void Link( string link, string message, string details ) {
			Link( link, message, details, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Writes a link log entry to the current log, if any.
		/// </summary>
		/// <param name="link">The link to log.</param>
		/// <param name="message">The message for this entry.</param>
		public static void Link( string link, string message ) {
			Link( link, message, null, EntryVerbosity.Normal );
		}

		/// <summary>
		/// Logs the result of a test case.
		/// </summary>
		/// <param name="result">The result of the test case.</param>
		/// <remarks>TestCase uses this method to mark the final result of a test step. Please don't call it yourself.</remarks>
		public void WriteResult(TestCaseResult result)
		{
			_writer.WriteStartElement( "LogResult" );
			_writer.WriteString( result.ToString() );
			_writer.WriteEndElement();
			_writer.Flush();
		}

		/// <summary>
		/// Logs the result of a test case.
		/// </summary>
		/// <param name="result">The result of the test case.</param>
		/// <remarks>TestCase uses this method to mark the final result of a test step. Please don't call it yourself.</remarks>
		public static void Result( TestCaseResult result ) {
			if( _currentLog != null )
				_currentLog.WriteResult( result );
		}

		#region IDisposable Members

		/// <summary>
		/// Saves the log file, if not saved and cleans any resources associated with the log
		/// </summary>
		public void Dispose() {
			_writer.WriteEndDocument();
			_writer.Close();
			_writer = null;
		}

		#endregion

		private static Log _currentLog = null;

		/// <summary>
		/// The current log that all static methods will use.
		/// </summary>
		public static Log Current {
			get { return _currentLog; }
			set { _currentLog = value; }
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
			get { return _filename; }
		}

		/// <summary>
		/// The path of the log
		/// </summary>
		public string Path
		{
			get
			{
				return System.IO.Path.GetDirectoryName(_filename);
			}
		}

		/// <summary>
		/// This event is fired before an error is posted to the log
		/// </summary>
		public event EventHandler<LogEventArgs> BeforeErrorIsPosted;

		/// <summary>
		/// This event is fired before a warning is posted to the log
		/// </summary>
		public event EventHandler<LogEventArgs> BeforeWarningIsPosted; 
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
