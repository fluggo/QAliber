using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using QAliber.Engine;

[assembly:QAliber.TestModel.PreferredXmlPrefix("qc", QAliber.Repository.CommonTestCases.Util.XmlNamespace)]

namespace QAliber.Repository.CommonTestCases {
	class Util {
		public const string XmlNamespace = "urn:schemas-qaliber:test-steps:common";

		/// <summary>
		/// Tries to get the @Name condition from the given XPath expression.
		/// </summary>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static string GetControlNameFromXPath( string xpath ) {
			if( xpath.StartsWith( "uia:" ) )
				xpath = xpath.Substring( 4 );
			else
				return null;

			XPathExpression expr = XPath.Parse( xpath, true );
			XPathPredicate pred = expr as XPathPredicate;

			if( pred != null ) {
				// Try to identify when the XPath actually refers to a combo box
				XPathStep step = pred.Left as XPathStep;

				if( step != null && step.LocalPart == "edit" ) {
					XPathPredicate tempPred = step.Left as XPathPredicate;

					if( tempPred != null ) {
						XPathStep tempStep = tempPred.Left as XPathStep;

						if( tempStep != null && tempStep.LocalPart == "combobox" )
							pred = tempPred;
					}
				}

				XPathOperatorExpression op = pred.Filter as XPathOperatorExpression;

				if( op != null )
					return FindEqualsCondition( op, "Name" ) ?? FindEqualsCondition( op, "ID" );
			}

			return null;
		}

		public static string FindEqualsCondition( XPathOperatorExpression op, string attributeName ) {
			if( op.Operator == "=" ) {
				XPathStep step = (op.Left as XPathStep) ?? (op.Right as XPathStep);
				XPathLiteral lit = (op.Left as XPathLiteral) ?? (op.Right as XPathLiteral);

				if( step != null && step.Axis == "attribute" && step.Left == null && step.LocalPart == attributeName &&
					lit != null && lit.Value is string )
					return (string) lit.Value;
			}

			if( op.Operator == "and" ) {
				XPathOperatorExpression left = op.Left as XPathOperatorExpression;
				XPathOperatorExpression right = op.Right as XPathOperatorExpression;

				if( left != null ) {
					string result = FindEqualsCondition( left, attributeName );

					if( result != null )
						return result;
				}

				if( right != null )
					return FindEqualsCondition( right, attributeName );
			}

			return null;
		}
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
