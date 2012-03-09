using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.ComponentModel;

namespace QAliber.TestModel {
	[Editor(typeof(TypeEditors.OutputPropertiesTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
	public class OutputPropertiesMap : Dictionary<string, string>, IXmlSerializable {
		public XmlSchema GetSchema() {
			return null;
		}

		public void ReadXml( XmlReader reader ) {
			if( !reader.IsEmptyElement ) {
				reader.Read();

				while( reader.NodeType != XmlNodeType.EndElement ) {
					if( reader.NodeType == XmlNodeType.Whitespace ) {
						reader.Read();
						continue;
					}

					if( reader.Name != "item" || reader.NamespaceURI != Util.XmlNamespace )
						throw new XmlException( "Found incorrect item in output property map." );

					string key = reader["key"];
					string value = reader["value"];

					Add( key, value );

					reader.Read();
				}
			}

			reader.Read();
		}

		public void WriteXml( XmlWriter writer ) {
			string prefix = writer.LookupPrefix( Util.XmlNamespace );

			foreach( var kv in this ) {
				if( prefix == null ) {
					writer.WriteStartElement( "item", Util.XmlNamespace );
					writer.WriteAttributeString( "key", Util.XmlNamespace, kv.Key );
					writer.WriteAttributeString( "value", Util.XmlNamespace, kv.Value );
				}
				else {
					writer.WriteStartElement( prefix, "item", null );
					writer.WriteAttributeString( prefix, "key", null, kv.Key );
					writer.WriteAttributeString( prefix, "value", null, kv.Value );
				}

				writer.WriteEndElement();
			}
		}
	}
}
