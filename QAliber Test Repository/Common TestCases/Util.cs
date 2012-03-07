using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

[assembly:QAliber.TestModel.PreferredXmlPrefix("qc", QAliber.Repository.CommonTestCases.Util.XmlNamespace)]

namespace QAliber.Repository.CommonTestCases {
	class Util {
		public const string XmlNamespace = "urn:schemas-qaliber:test-steps:common";
	}

	public class PercentageInt32Converter : TypeConverter {
		public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType ) {
			return sourceType == typeof(string) ||
				sourceType == typeof(int);
		}

		public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType ) {
			return destinationType == typeof(string) ||
				destinationType == typeof(int);
		}

		public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value ) {
			if( value is int )
				return value;

			string str = value as string;

			if( str == null )
				throw GetConvertFromException( value );

			str = str.Trim();

			if( str.EndsWith( culture.NumberFormat.PercentSymbol ) )
				str = str.Substring( 0, str.Length - culture.NumberFormat.PercentSymbol.Length );

			int result;

			if( !int.TryParse( str, NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands, culture, out result ) )
				throw GetConvertFromException( value );

			return result;
		}

		public override object ConvertTo( ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType ) {
			if( !(value is int) )
				throw GetConvertToException( value, destinationType );

			if( destinationType == typeof(int) )
				return (int) value;

			if( destinationType == typeof(string) )
				return ((int) value).ToString( culture ) + "%";

			throw GetConvertToException( value, destinationType );
		}
	}
}
