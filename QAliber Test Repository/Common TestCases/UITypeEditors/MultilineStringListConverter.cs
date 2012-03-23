using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Globalization;

namespace QAliber.Repository.CommonTestCases.UITypeEditors {
	public class MultilineStringListConverter : TypeConverter {
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if( destinationType == typeof(string) ) {
				return string.Join( ", ", ((string) value).TrimEnd()
					.Split( '\n' ).Select( str => str.TrimEnd( '\r' ) ) );
			}

			return base.ConvertTo( context, culture, value, destinationType );
		}

		public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType ) {
			return false;
		}
	}
}
