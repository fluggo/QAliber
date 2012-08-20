using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace QAliber.TestModel.TypeEditors {
	public class EnumDescriptionConverter : EnumConverter {
		public EnumDescriptionConverter( Type enumType ) : base( enumType ) {}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if( destinationType == typeof(string) ) {
				if( value is string )
					return (string) value;

				Type type;

				if( context != null )
					type = context.PropertyDescriptor.PropertyType;
				else
					type = value.GetType();

				string name = Enum.GetName( type, value );

				FieldInfo field = type.GetField( name );
				DescriptionAttribute descrAttr = (DescriptionAttribute)
					Attribute.GetCustomAttribute( field, typeof(DescriptionAttribute) );

				if( descrAttr != null )
					return descrAttr.Description;

				return name;
			}

			return base.ConvertTo( context, culture, value, destinationType );
		}

		public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType ) {
			return sourceType == typeof(string) || sourceType == typeof(Enum);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			if( destinationType == typeof(string) || destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor) ) {
				return true;
			}

			return false;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			Type type = context.PropertyDescriptor.PropertyType;

			if( value.GetType() == type )
				return value;

			string str = value as string;

			if( str != null ) {
				FieldInfo[] fields = type.GetFields();

				foreach( FieldInfo field in fields ) {
					DescriptionAttribute descrAttr = (DescriptionAttribute)
						Attribute.GetCustomAttribute( field, typeof(DescriptionAttribute) );

					if( descrAttr != null && str == descrAttr.Description )
						return Enum.Parse( type, field.Name );

					if( str == field.Name )
						return Enum.Parse( type, str );
				}

				throw new Exception( "Couldn't parse the value." );
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
			Type type = context.PropertyDescriptor.PropertyType;
			return new StandardValuesCollection( Enum.GetValues( type ) );
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
			return true;
		}
	}
}
