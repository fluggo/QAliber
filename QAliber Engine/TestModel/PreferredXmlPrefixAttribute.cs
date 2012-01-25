using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QAliber.TestModel {
	/// <summary>
	/// Instructs QAliber to declare a prefix for the given namespace.
	/// </summary>
	/// <remarks>This is important to make the resulting XML friendlier to source control systems,
	///   otherwise the XML generator makes up sequentially-numbered prefixes.</remarks>
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class PreferredXmlPrefixAttribute : Attribute {
		readonly string _prefix, _namespace;

		/// <summary>
		/// Creates a new instance of the <see cref="PreferredXmlPrefixAttribute"/>.
		/// </summary>
		/// <param name="prefix">The preferred prefix for the namespace.</param>
		/// <param name="namespace">The URI of the XML namespace being declared.</param>
		public PreferredXmlPrefixAttribute( string prefix, string @namespace ) {
			_prefix = prefix;
			_namespace = @namespace;
		}

		/// <summary>
		/// Gets the preferred prefix for the namespace.
		/// </summary>
		/// <value>The preferred prefix for the namespace.</value>
		public string Prefix
			{ get { return _prefix; } }

		/// <summary>
		/// Gets the URI of the XML namespace.
		/// </summary>
		/// <value>The URI of the XML namespace being declared.</value>
		public string Namespace
			{ get { return _namespace; } }
	}
}
