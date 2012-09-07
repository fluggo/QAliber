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
using System.Xml.Serialization;
using System.IO;
using System.Security;
using System.Xml;
using System.Globalization;

namespace QAliber.Logger
{
	/// <summary>
	/// Represents a log entry in QAliber log
	/// </summary>
	[Serializable]
	public class LogEntry
	{
		public LogEntry()
		{
			enabled = true;
			
		}
		private DateTime time;

		/// <summary>
		/// The time of the entry
		/// </summary>
		public DateTime Time
		{
			get { return time; }
			set { time = value; }
		}

		private string message;

		/// <summary>
		/// The message of the entry
		/// </summary>
		public string Message
		{
			get { return message; }
			set { message = value; }
		}

		private string ext;

		/// <summary>
		/// An additional text to add to the message
		/// </summary>
		public string ExtendedMessage
		{
			get { return ext; }
			set { ext = value; }
		}

		private string link;

		/// <summary>
		/// A link that can be opened from the log
		/// <remarks>The link will be opened via Process.Start(yourlink), so make sure that the client's computer has an association with it</remarks>
		/// </summary>
		public string Link
		{
			get { return link; }
			set { link = value; }
		}
	

		private EntryType type;

		/// <summary>
		/// The type of the entry
		/// </summary>
		public EntryType Type
		{
			get { return type; }
			set { type = value; }
		}

		private BodyType bodyType;

		/// <summary>
		/// How the message is treated (text, picture, link)
		/// </summary>
		public BodyType Body
		{
			get { return bodyType; }
			set { bodyType = value; }
		}

		private EntryVerbosity verbosity;

		/// <summary>
		/// The verbosity of the message
		/// </summary>
		public EntryVerbosity Verbosity
		{
			get { return verbosity; }
			set { verbosity = value; }
		}

		private bool enabled;

		/// <summary>
		/// Gets or sets if the LogEntry will be logged
		/// </summary>
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
	

		internal void ToXml( XmlWriter writer ) {
			writer.WriteStartElement( "LogEntry" );
			writer.WriteAttributeString( "timeUtc", time.ToString( "s", CultureInfo.InvariantCulture ) );
			writer.WriteAttributeString( "type", type.ToString() );
			writer.WriteAttributeString( "bodyType", bodyType.ToString() );

			if( verbosity != EntryVerbosity.Normal )
				writer.WriteAttributeString( "verbosity", verbosity.ToString() );

			if( link != null )
				writer.WriteAttributeString( "link", link );

			writer.WriteStartElement( "Message" );
			writer.WriteString( message );
			writer.WriteEndElement();

			if( !string.IsNullOrEmpty( ext ) ) {
				writer.WriteStartElement( "Details" );
				writer.WriteString( ext );
				writer.WriteEndElement();
			}

			writer.WriteEndElement();
		}
	}
}
