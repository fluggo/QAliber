using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly:QAliber.TestModel.PreferredXmlPrefix("qc", QAliber.Repository.CommonTestCases.Util.XmlNamespace)]

namespace QAliber.Repository.CommonTestCases {
	class Util {
		public const string XmlNamespace = "urn:schemas-qaliber:test-steps:common";
	}
}
