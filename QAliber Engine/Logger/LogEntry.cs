using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Security;

namespace QAliber.Logger
{
	/// <summary>
	/// Represents a log entry in QAliber log
	/// </summary>
	[Serializable]
	public class LogEntry
	{
		internal LogEntry()
		{
			xmlSerializer = new XmlSerializer(GetType());
			link = "";
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
		internal BodyType Body
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

		private EntryStyle style;

		/// <summary>
		/// The font style and colors of the message
		/// </summary>
		public EntryStyle Style
		{
			get { return style; }
			set { style = value; }
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
	

		internal void ToXml(StreamWriter sw, int indents)
		{
			string tabs = "";
			for (int i = 0; i < indents + 1; i++)
			{
				tabs += "\t";
			}
			lock (this)
			{
				sw.WriteLine(tabs + "<LogEntry>");
				sw.WriteLine(tabs + "\t<Time>" + SecurityElement.Escape(time.ToString()) + "</Time>");
				sw.WriteLine(tabs + "\t<Message>" + SecurityElement.Escape(message) + "</Message>");
				sw.WriteLine(tabs + "\t<ExtendedMessage>" + SecurityElement.Escape(ext) + "</ExtendedMessage>");
				sw.WriteLine(tabs + "\t<Link>" + SecurityElement.Escape(link) + "</Link>");
				sw.WriteLine(tabs + "\t<Type>" + type + "</Type>");
				sw.WriteLine(tabs + "\t<Body>" + bodyType + "</Body>");
				sw.WriteLine(tabs + "\t<Verbosity>" + verbosity + "</Verbosity>");
				sw.WriteLine(tabs + "\t<Style>");
				sw.WriteLine(tabs + "\t\t<FontStyle>" + style.FontStyle + "</FontStyle>");
				sw.WriteLine(tabs + "\t\t<FGColorVal>" + style.FGColorVal + "</FGColorVal>");
				sw.WriteLine(tabs + "\t\t<BGColorVal>" + style.BGColorVal + "</BGColorVal>");
				sw.WriteLine(tabs + "\t</Style>");
				sw.WriteLine(tabs + "</LogEntry>");
			}

  
		}
	
		private XmlSerializer xmlSerializer;
	}
}
