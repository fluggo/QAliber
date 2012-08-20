using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Globalization;

namespace QAliber.TestModel.TypeEditors {
	public class DisplayListConverter : TypeConverter {
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if( destinationType == typeof(string) ) {
				return string.Join( ", ", ((IEnumerable) value).Cast<object>().Select(
					obj => Convert.ToString( obj, culture ) ) );
			}

			return base.ConvertTo( context, culture, value, destinationType );
		}

		public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType ) {
			return false;
		}
	}
}
