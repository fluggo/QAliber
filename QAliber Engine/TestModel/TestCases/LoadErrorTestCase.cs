using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel.Attributes;
using System.Runtime.Serialization;

namespace QAliber.TestModel.TestCases
{
	[Serializable]
	[VisualPath(@"Hidden")]
	public class LoadErrorTestCase : TestCase, ISerializable
	{
		public LoadErrorTestCase()
		{
			name = "Error Loading Type";
			icon = Properties.Resources.LoadError;
			if (DeserializationBinder.errorIndex < DeserializationBinder.errorTypes.Count)
			{
				name += " " + DeserializationBinder.errorTypes[DeserializationBinder.errorIndex];
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
