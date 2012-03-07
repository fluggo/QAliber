using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel.Attributes;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QAliber.TestModel.TestCases
{
	[Serializable]
	[VisualPath(@"Hidden")]
	[XmlType("LoadError", Namespace=Util.XmlNamespace)]
	public class LoadErrorTestCase : TestCase, ISerializable
	{
		public LoadErrorTestCase() : base( "Error Loading Type" )
		{
			Icon = Properties.Resources.LoadError;
			if (DeserializationBinder.errorIndex < DeserializationBinder.errorTypes.Count)
			{
				Name += " " + DeserializationBinder.errorTypes[DeserializationBinder.errorIndex];
				DeserializationBinder.errorIndex++;
			}

		}

		protected LoadErrorTestCase(SerializationInfo info, StreamingContext context) : this()
		{
			
		}

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			
		}

		#endregion
	}
}
