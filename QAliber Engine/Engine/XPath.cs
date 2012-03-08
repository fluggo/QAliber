/*	  
 * Copyright (C) 2012 Anonymous (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 *
 * XPath parser developed by Brian Crowell.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using System.Xml;
using System.Globalization;

namespace QAliber.Engine {
	/// <summary>
	/// Represents a node that can be navigated using XPath.
	/// </summary>
	public interface IXPathNode : IEquatable<IXPathNode> {
		/// <summary>
		/// Finds nodes related to this one on the given axis.
		/// </summary>
		/// <param name="axis">Axis to find nodes on.</param>
		/// <returns>A set of <see cref="IXPathNode"/> objects of all the nodes found matching the conditions.</returns>
		HashSet<IXPathNode> FindNodesByAxis( string axis );

		string GetStringValue();

		string Namespace { get; }
		string LocalName { get; }
	}

	/// <summary>
	/// Represents an XPath node that supports searching axes by name.
	/// </summary>
	public interface IXPathSearchableNode : IXPathNode {
		/// <summary>
		/// Finds nodes related to this one by name on the given axis.
		/// </summary>
		/// <param name="axis">Axis to find nodes on.</param>
		/// <param name="namespace">Namespace to search for, or null for no namespace.</param>
		/// <param name="localName">Local name to search for, or "*" for all names.</param>
		/// <returns>A set of <see cref="IXPathNode"/> objects of all the nodes found matching the conditions.</returns>
		HashSet<IXPathNode> FindNodesByName( string axis, string @namespace, string localName );
	}

	public class XPathContext {
		int _size, _position;
		IXPathNode _contextNode;

		public XPathContext( int size, int position, IXPathNode node ) {
			_size = size;
			_position = position;
			_contextNode = node;
		}

		public int Size
			{ get { return _size; } }
		public int Position
			{ get { return _position; } }
		public IXPathNode Node
			{ get { return _contextNode; } }
	}

	public class XPathEvaluator {
		IXPathNode _root;
		IXmlNamespaceResolver _nsResolver;
		IComparer<IXPathNode> _orderComparer;

		public IXPathNode Root
			{ get { return _root; } }

		public XPathEvaluator( IXPathNode root, IXmlNamespaceResolver resolver, IComparer<IXPathNode> orderComparer ) {
			if( root == null )
				throw new ArgumentNullException( "root" );

			if( resolver == null )
				throw new ArgumentNullException( "resolver" );

			if( orderComparer == null )
				throw new ArgumentNullException( "orderComparer" );

			_root = root;
			_nsResolver = resolver;
			_orderComparer = orderComparer;
		}

		public object Evaluate( XPathExpression expression ) {
			return Evaluate( new XPathContext( 1, 1, _root ), expression );
		}

		public object Evaluate( XPathContext context, XPathExpression expression ) {
			XPathFunctionCall func = expression as XPathFunctionCall;
			XPathLiteral lit = expression as XPathLiteral;
			XPathOperatorExpression op = expression as XPathOperatorExpression;
			XPathVariableReference var = expression as XPathVariableReference;
			XPathStep step = expression as XPathStep;
			XPathRootExpression rootExp = expression as XPathRootExpression;
			XPathPredicate pred = expression as XPathPredicate;

			if( rootExp != null )
				return new IXPathNode[] { _root };

			if( lit != null )
				return lit.Value;

			if( step != null )
				return EvaluateStep( step, context );

			if( op != null ) {
				return EvaluateOperator( op.Operator,
					Evaluate( context, op.Left ),
					Evaluate( context, op.Right ) );
			}

			if( func != null ) {
				object[] parameters = func.Parameters.Select( p => Evaluate( context, p ) ).ToArray();
				return EvaluateFunction( context, func.FunctionName, parameters );
			}

			if( pred != null )
				return EvaluatePredicate( context, pred );

			if( var != null )
				return EvaluateVariable( var.Name );

			throw new ArgumentException( "Unknown expression type." );
		}

		public virtual HashSet<IXPathNode> EvaluateStep( IXPathNode contextNode, string axis, string @namespace, string localName ) {
			IXPathSearchableNode searchable = contextNode as IXPathSearchableNode;

			if( searchable != null )
				return searchable.FindNodesByName( axis, @namespace, localName );

			HashSet<IXPathNode> startSet = contextNode.FindNodesByAxis( axis );

			return new HashSet<IXPathNode>( startSet.Where( node => {
				// I think this is what the node-test section (2.3) says:
				//	 No namespace + * == match all nodes
				//	 Namespace + * == match all nodes in that namespace
				//	 No namespace + localName == match nodes with no namespace and given localName
				//	 Namespace + localName == match nodes with given namespace and localName
				if( (@namespace != null || localName != "*") && @namespace != node.Namespace )
					return false;

				if( localName == "*" )
					return true;

				if( localName != node.LocalName )
					return false;

				return true;
			} ) );
		}

		/// <summary>
		/// Evaluates the given operator and operands.
		/// </summary>
		/// <param name="operator">The name of the operator.</param>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the operation.</returns>
		public virtual object EvaluateOperator( string @operator, object left, object right ) {
			if( @operator == "or" )
				return ToBoolean( left ) || ToBoolean( right );

			if( @operator == "and" )
				return ToBoolean( left ) && ToBoolean( right );

			if( @operator == "=" || @operator == "!=" || @operator == "<=" || @operator == "<" || @operator == ">=" || @operator == ">" ) {
				// Check for node-sets first
				IXPathNode[] leftNodes = left as IXPathNode[], rightNodes = right as IXPathNode[];

				if( leftNodes != null && rightNodes != null ) {
					// Compare node-sets; if the operator succeeds on any node pair, the test succeeds
					foreach( IXPathNode lnode in leftNodes ) {
						string lvalue = lnode.GetStringValue();

						foreach( IXPathNode rnode in rightNodes ) {
							if( (bool) EvaluateOperator( @operator, lvalue, rnode.GetStringValue() ) )
								return true;
						}
					}

					return false;
				}
				else if( leftNodes != null || rightNodes != null ) {
					if( rightNodes != null ) {
						// Swap them around so that we only have to write the test once
						leftNodes = rightNodes;
						right = left;

						if( @operator == "<=" )
							@operator = ">=";
						else if( @operator == "<" )
							@operator = ">";
						else if( @operator == ">" )
							@operator = "<";
						else if( @operator == ">=" )
							@operator = "<=";
					}

					if( right is double ) {
						return leftNodes.Any( lnode =>
							(bool) EvaluateOperator(
								@operator,
								ToNumber( lnode.GetStringValue() ),
								right )
							);
					}

					if( right is string ) {
						return leftNodes.Any( lnode =>
							(bool) EvaluateOperator(
								@operator,
								lnode.GetStringValue(),
								right )
							);
					}

					if( right is bool ) {
						return EvaluateOperator( @operator, ToBoolean( leftNodes ), right );
					}

					throw new ArgumentException( "Unexpected type " + right.GetType() + " encountered." );
				}

				// Equality operators next
				if( @operator == "=" || @operator == "!=" ) {
					if( left is bool || right is bool ) {
						if( @operator == "=" )
							return ToBoolean( left ) == ToBoolean( right );
						else
							return ToBoolean( left ) != ToBoolean( right );
					}

					if( left is double || right is double ) {
						if( @operator == "=" )
							return ToNumber( left ) == ToNumber( right );
						else
							return ToNumber( left ) != ToNumber( right );
					}

					if( @operator == "=" )
						return ToString( left ) == ToString( right );
					else
						return ToString( left ) != ToString( right );
				}

				// Now the only operators left are <, <=, >=, and >
				if( @operator == "<" )
					return ToNumber( left ) < ToNumber( right );
				else if( @operator == "<=" )
					return ToNumber( left ) <= ToNumber( right );
				else if( @operator == ">=" )
					return ToNumber( left ) >= ToNumber( right );
				else
					return ToNumber( left ) > ToNumber( right );
			}

			if( @operator == "|" ) {
				// Return the union, sorted in document order (the spec doesn't actually say)
				IXPathNode[] leftNodes = left as IXPathNode[], rightNodes = right as IXPathNode[];

				if( leftNodes == null || rightNodes == null )
					throw new ArgumentException( "Union operator used on something other than a node set." );

				return leftNodes.Union( rightNodes ).OrderBy( n =>n, _orderComparer ).ToArray();
			}

			if( @operator == "+" )
				return ToNumber( left ) + ToNumber( right );

			if( @operator == "-" )
				return ToNumber( left ) - ToNumber( right );

			if( @operator == "div" )
				return ToNumber( left ) / ToNumber( right );

			if( @operator == "mod" )
				return ToNumber( left ) % ToNumber( right );

			// Funny thing, I can't find in the spec where multiplication is defined
			if( @operator == "*" )
				return ToNumber( left ) * ToNumber( right );

			throw new ArgumentException( "Unknown operator \"" + @operator + "\"." );
		}

		public virtual object EvaluateFunction( XPathContext context, string name, object[] parameters ) {
			if( name == "boolean" ) {
				if( parameters.Length != 1 )
					throw new ArgumentException( "boolean() called with the wrong number of parameters." );

				return ToBoolean( parameters[0] );
			}

			if( name == "true" ) {
				if( parameters.Length != 0 )
					throw new ArgumentException( "true() called with the wrong number of parameters." );

				return true;
			}

			if( name == "false" ) {
				if( parameters.Length != 0 )
					throw new ArgumentException( "false() called with the wrong number of parameters." );

				return false;
			}

			if( name == "not" ) {
				if( parameters.Length != 1 )
					throw new ArgumentException( "not() called with the wrong number of parameters." );

				// Spec says I have to convert it to a boolean
				return !ToBoolean( parameters[0] );
			}

			if( name == "number" ) {
				if( parameters.Length == 1 )
					return ToNumber( parameters[0] );
				else if( parameters.Length == 0 )
					return ToNumber( new IXPathNode[] { context.Node } );
				else
					throw new ArgumentException( "number() called with the wrong number of parameters." );
			}

			if( name == "string" ) {
				if( parameters.Length == 1 )
					return ToString( parameters[0] );
				else if( parameters.Length == 0 )
					return ToString( new IXPathNode[] { context.Node } );
				else
					throw new ArgumentException( "string() called with the wrong number of parameters." );
			}

			if( name == "concat" ) {
				if( parameters.Length < 2 )
					throw new ArgumentException( "concat() called with the wrong number of parameters." );

				return string.Concat( parameters.Select( ToString ) );
			}

			if( name == "starts-with" ) {
				if( parameters.Length != 2 )
					throw new ArgumentException( "starts-with() called with the wrong number of parameters." );

				string arg1 = ToString( parameters[0] ), arg2 = ToString( parameters[1] );

				return arg1.StartsWith( arg2, StringComparison.Ordinal );
			}

			if( name == "contains" ) {
				if( parameters.Length != 2 )
					throw new ArgumentException( "contains() called with the wrong number of parameters." );

				string arg1 = ToString( parameters[0] ), arg2 = ToString( parameters[1] );

				return arg1.Contains( arg2 );
			}

			if( name == "last" ) {
				if( parameters.Length != 0 )
					throw new ArgumentException( "last() called with the wrong number of parameters." );

				return (double) context.Size;
			}

			if( name == "position" ) {
				if( parameters.Length != 0 )
					throw new ArgumentException( "position() called with the wrong number of parameters." );

				return (double) context.Position;
			}

			if( name == "count" ) {
				if( parameters.Length != 1 )
					throw new ArgumentException( "count() called with the wrong number of parameters." );

				if( !(parameters[0] is IXPathNode[]) )
					throw new ArgumentException( "count() called with something other than a node-set." );

				return (double) ((IXPathNode[]) parameters[0]).Length;
			}

			throw new NotSupportedException( "Unknown function \"" + name + "\"." );
		}

		private IXPathNode[] EvaluateStep( XPathStep step, XPathContext context ) {
			IXPathNode[] startNodes;

			if( step.Left != null ) {
				object leftResult = Evaluate( context, step.Left );
				startNodes = leftResult as IXPathNode[];

				if( startNodes == null )
					throw new ArgumentException( "The expression \"" + step.Left.ToString() + "\" did not evaluate to a node set." );
			}
			else {
				startNodes = new IXPathNode[] { context.Node };
			}

			string ns = null, localPart = "*";

			if( step.IsNodeType ) {
				if( step.LocalPart != "node" )
					throw new NotImplementedException( "Node type searches other than node() are not implemented." );
			}
			else {
				if( step.Prefix != null )
					ns = _nsResolver.LookupNamespace( step.Prefix );

				localPart = step.LocalPart;
			}

			HashSet<IXPathNode> result = new HashSet<IXPathNode>();

			foreach( IXPathNode node in startNodes ) {
				result.UnionWith( EvaluateStep( node, step.Axis, ns, localPart ) );
			}

			// Sort by document order based on axis
			if( step.Axis == "ancestor" || step.Axis == "ancestor-or-self" ||
					step.Axis == "preceding" || step.Axis == "preceding-sibling" ) {
				return result.OrderByDescending( n => n, _orderComparer ).ToArray();
			}
			else {
				return result.OrderBy( n => n, _orderComparer ).ToArray();
			}
		}

		public virtual object EvaluateVariable( string name ) {
			throw new ArgumentException( "Unknown variable $" + name + "." );
		}

		public IXPathNode[] EvaluatePredicate( XPathContext context, XPathPredicate predicate ) {
			if( predicate.Left == null )
				throw new ArgumentException( "A predicate appeared without a left-hand side. How did you do that, anyways?" );

			// Evaluate left side for a node set
			object leftResult = Evaluate( context, predicate.Left );
			IXPathNode[] contextNodes = leftResult as IXPathNode[];

			if( contextNodes == null )
				throw new ArgumentException( "The expression \"" + predicate.Left.ToString() + "\" did not evaluate to a node set." );

			// Process predicates
			return contextNodes.Where( (node, i) => {
				XPathContext newContext = new XPathContext( contextNodes.Length, i + 1, contextNodes[i] );
				object filterResult = Evaluate( newContext, predicate.Filter );

				if( filterResult is double ) {
					// Position test
					int targetPosition = (int) (((double) filterResult) + 0.5);
					return targetPosition == i + 1;
				}

				return ToBoolean( filterResult );
			} ).ToArray();
		}

		/// <summary>
		/// Converts the expression to boolean using XPath rules.
		/// </summary>
		/// <param name="expression">Expression to convert.</param>
		/// <returns>A boolean value.</returns>
		/// <remarks>This is the same as calling the XPath function boolean().</remarks>
		public static bool ToBoolean( object expression ) {
			IXPathNode[] nodes = expression as IXPathNode[];
			string str = expression as string;

			if( expression is bool )
				return (bool) expression;

			if( nodes != null )
				return nodes.Length != 0;

			if( str != null )
				return str.Length != 0;

			if( expression is double ) {
				double number = (double) expression;
				return !double.IsNaN( number ) && number != 0.0;
			}

			return Convert.ToBoolean( expression, CultureInfo.InvariantCulture );
		}

		const NumberStyles __numberFormat = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

		/// <summary>
		/// Converts the expression to number using XPath rules.
		/// </summary>
		/// <param name="expression">Expression to convert.</param>
		/// <returns>A number value.</returns>
		/// <remarks>This is the same as calling the XPath function number().</remarks>
		public static double ToNumber( object expression ) {
			IXPathNode[] nodes = expression as IXPathNode[];
			string str = expression as string;

			if( expression is double )
				return (double) expression;

			if( nodes != null )
				str = ToString( nodes );

			if( str != null ) {
				double result;

				if( double.TryParse( str, __numberFormat, CultureInfo.InvariantCulture, out result ) )
					return result;

				return double.NaN;
			}

			if( expression is bool )
				return ((bool) expression) ? 1.0 : 0.0;

			return Convert.ToDouble( expression, CultureInfo.InvariantCulture );
		}

		public static string ToString( object expression ) {
			IXPathNode[] nodes = expression as IXPathNode[];
			string str = expression as string;

			if( str != null )
				return str;

			if( nodes != null ) {
				if( nodes.Length == 0 )
					return string.Empty;

				return nodes[0].GetStringValue();
			}

			if( expression is double ) {
				double number = (double) expression;

				if( double.IsNaN( number ) )
					return "NaN";

				if( double.IsPositiveInfinity( number ) )
					return "Infinity";

				if( double.IsNegativeInfinity( number ) )
					return "-Infinity";

				return number.ToString( CultureInfo.InvariantCulture );
			}

			if( expression is bool )
				return ((bool) expression) ? "true" : "false";

			return Convert.ToString( expression, CultureInfo.InvariantCulture );
		}
	}

	/// <summary>
	/// Represents an expression that can appear in or as an XPath predicate.
	/// </summary>
	public abstract class XPathExpression {
	}

	/// <summary>
	/// Represents a literal appearing in an XPath predicate.
	/// </summary>
	public class XPathLiteral : XPathExpression {
		/// <summary>
		/// Gets or sets the value of the literal.
		/// </summary>
		/// <value>The value of the literal. This should be a <see cref="String"/> or a <see cref="Double"/>.</value>
		public object Value { get; set; }

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			if( Value is double )
				return ((double) Value).ToString( CultureInfo.InvariantCulture );
			else
				return "'" + Value.ToString() + "'";
		}
	}

	/// <summary>
	/// Represents a variable reference appearing in an XPath predicate.
	/// </summary>
	public class XPathVariableReference : XPathExpression {
		/// <summary>
		/// Gets or sets the name of the variable.
		/// </summary>
		/// <value>The name of the variable, without the "$" prefix.</value>
		public string Name { get; set; }

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			return "$" + Name;
		}
	}

	/// <summary>
	/// Represents a function call in an XPath predicate.
	/// </summary>
	public class XPathFunctionCall : XPathExpression {
		List<XPathExpression> _parameters = new List<XPathExpression>();

		/// <summary>
		/// Gets or sets the name of the function.
		/// </summary>
		/// <value>The name of the function.</value>
		public string FunctionName { get; set; }

		/// <summary>
		/// Gets the list of parameters to the function.
		/// </summary>
		/// <value>The list of parameters to the function.</value>
		public List<XPathExpression> Parameters
			{ get { return _parameters; } }

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			return FunctionName + "(" +
				string.Join( ", ", _parameters.Select( p => p.ToString() ) ) + ")";
		}
	}

	/// <summary>
	/// Represents a binary infix expression in an XPath predicate.
	/// </summary>
	/// <remarks>Parentheses are not represented in the expression tree,
	///   but the tree is organized in order of operations.</remarks>
	public class XPathOperatorExpression : XPathExpression {
		/// <summary>
		/// Gets or sets the left-hand expression.
		/// </summary>
		/// <value>The left-hand expression.</value>
		public XPathExpression Left { get; set; }

		/// <summary>
		/// Gets or sets the right-hand expression.
		/// </summary>
		/// <value>The right-hand expression.</value>
		public XPathExpression Right { get; set; }

		/// <summary>
		/// Gets or sets the name of the operator.
		/// </summary>
		/// <value>The name or symbol of the operator, such as "+" or "and".</value>
		public string Operator { get; set; }

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			// Since we don't know which operators take precedence over which others,
			// we have to include parentheses
			return "(" + Left.ToString() + ") " + Operator + " (" + Right.ToString() + ")";
		}
	}

	/// <summary>
	/// Represents a predicate filter.
	/// </summary>
	public class XPathPredicate : XPathExpression {
		/// <summary>
		/// Gets or sets the expression being filtered.
		/// </summary>
		///	<value>The expression being filtered.</value>
		public XPathExpression Left { get; set; }

		/// <summary>
		/// Gets or sets the filter expression.
		/// </summary>
		/// <value>The filter expression.</value>
		public XPathExpression Filter { get; set; }

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			if( Left is XPathStep || Left is XPathPredicate ) {
				// No parentheses necessary
				return Left.ToString() + "[" + Filter.ToString() + "]";
			}
			else {
				return "(" + Left.ToString() + ")[" + Filter.ToString() + "]";
			}
		}
	}

	/// <summary>
	/// Represents the root of the <see cref="XPath"/> tree.
	/// </summary>
	/// <remarks>This will appear as the left side of an <see cref="XPathStep"/> to represent an
	///   absolute path.</remarks>
	public class XPathRootExpression : XPathExpression {
		/// <summary>
		/// The single instance of the <see cref="XPathRootExpression"/> class.
		/// </summary>
		public static readonly XPathRootExpression Value = new XPathRootExpression();

		private XPathRootExpression() {
		}

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			return "/";
		}
	}

	/// <summary>
	/// Represents a step in an <see cref="XPath"/> expression.
	/// </summary>
	public class XPathStep : XPathExpression {
		/// <summary>
		/// Gets or sets the left-hand side of the expression.
		/// </summary>
		/// <value>The left-hand side of the expression, or null if there is none.</value>
		public XPathExpression Left { get; set; }

		/// <summary>
		/// Gets or sets the axis this step follows.
		/// </summary>
		/// <value>The name of the axis this step follows, such as "child" or "parent".</value>
		/// <remarks>All steps have to have an axis.</remarks>
		public string Axis { get; set; }

		/// <summary>
		/// Gets or sets an optional prefix on the elements to find.
		/// </summary>
		/// <value>The name of the prefix to find, or null. Only valid if <see cref="IsNodeType"/> is false.</value>
		public string Prefix { get; set; }

		/// <summary>
		/// Gets or sets the local part of the element name or the node type.
		/// </summary>
		/// <value>The local part of the element name to find or, if <see cref="IsNodeType"/> is true, the node type.
		///   "*" is a value that matches all element names.</value>
		public string LocalPart { get; set; }

		/// <summary>
		/// Gets or sets the optional literal parameter to the node type function.
		/// </summary>
		/// <value>The literal parameter to the node type function, which according to the XPath
		///   spec is only valid when <see cref="IsNodeType"/> is true and <see cref="LocalPart"/>
		///   is "processing-instruction".</value>
		public string Literal { get; set; }

		/// <summary>
		/// Gets or sets whether <see cref="LocalPart"/> refers to a node type.
		/// </summary>
		/// <value>True if <see cref="LocalPart"/> is a node type, or false if it's an element's local name.</value>
		public bool IsNodeType { get; set; }

		/// <summary>
		/// Overrides <see cref="Object.ToString"/>.
		/// </summary>
		/// <returns>A string approximating this expression's representation in XPath.</returns>
		public override string ToString() {
			string left;

			if( Left == null ) {
				left = string.Empty;
			}
			else if( Left == XPathRootExpression.Value ) {
				left = "/";
			}
			else {
				XPathStep leftStep = Left as XPathStep;

				if( leftStep != null && leftStep.IsNodeType && leftStep.LocalPart == "node" && leftStep.Axis == XPath.DescendantOrSelfAxis ) {
					if( leftStep.Left == null ) {
						left = ".//";
					}
					else if( leftStep.Left == XPathRootExpression.Value ) {
						left = "//";
					}
					else {
						left = leftStep.Left.ToString() + "//";
					}
				}
				else {
					left = Left.ToString() + "/";
				}
			}

			// Special-case shortcuts
			if( IsNodeType && LocalPart == "node" ) {
				if( Axis == XPath.SelfAxis )
					return left + ".";

				if( Axis == XPath.ParentAxis )
					return left + "..";
			}

			string axis;

			if( Axis == XPath.ChildAxis )
				axis = string.Empty;
			else if( Axis == XPath.AttributeAxis )
				axis = "@";
			else
				axis = Axis + "::";

			if( IsNodeType ) {
				if( Literal != null )
					return left + axis + LocalPart + "('" + Literal + "')";
				else
					return left + axis + LocalPart + "()";
			}

			if( Prefix != null )
				return left + axis + Prefix + ":" + LocalPart;

			return left + axis + LocalPart;
		}
	}

	/// <summary>
	/// Represents an XPath path.
	/// </summary>
	public static class XPath {
		enum XPathTokenType {
			OpenParenthesis,
			CloseParenthesis,
			OpenBracket,
			CloseBracket,
			Dot,
			DoubleDot,
			At,
			Comma,
			Colon,
			DoubleColon,
			Literal,
			Operator,
			NameTest,
			AxisName,
			FunctionName,
			NodeType,
			VariableReference,
			Number,
			AbsoluteSlash,
			AbsoluteDoubleSlash
		}

		class XPathToken {
			public XPathToken( XPathTokenType type, string input, int tokenStart, int tokenEnd ) {
				_type = type;
				_input = input;
				_tokenStart = tokenStart;
				_tokenEnd = tokenEnd;
			}

			private XPathToken( XPathTokenType type, string token ) {
				_type = type;
				_input = string.Intern( token );
				_tokenStart = 0;
				_tokenEnd = _input.Length - 1;
				_value = _input;
			}

			XPathTokenType _type;
			string _input, _value;
			int _tokenStart, _tokenEnd;

			public XPathTokenType Type
				{ get { return _type; } }
			public string Input
				{ get { return _input; } }
			public int TokenStart
				{ get { return _tokenStart; } }
			public int TokenEnd
				{ get { return _tokenEnd; } }

			public string Value {
				get {
					if( _value == null )
						_value = string.Intern( Input.Substring( TokenStart, Length ) );

					return _value;
				}
			}

			public int Length
				{ get { return TokenEnd - TokenStart + 1; } }

			public char this[int index] {
				get {
					if( index < 0 || index >= Length )
						throw new ArgumentOutOfRangeException( "index" );

					return Input[TokenStart + index];
				}
			}

			public override bool Equals( object obj ) {
				XPathToken other = obj as XPathToken;

				if( object.ReferenceEquals( other, null ) )
					return false;

				if( object.ReferenceEquals( other, this ) )
					return true;

				return Type == other.Type && Value == other.Value;
			}

			public override int GetHashCode() {
				return unchecked(Type.GetHashCode() * Value.GetHashCode());
			}

			public static bool operator == ( XPathToken left, XPathToken right ) {
				if( object.ReferenceEquals( left, right ) )
					return true;

				if( object.ReferenceEquals( left, null ) )
					return false;

				return left.Equals( right );
			}

			public static bool operator != ( XPathToken left, XPathToken right ) {
				return !(left == right);
			}

			public static readonly XPathToken EqualsOperator = new XPathToken( XPathTokenType.Operator, "=" );
			public static readonly XPathToken NotEqualsOperator = new XPathToken( XPathTokenType.Operator, "!=" );
			public static readonly XPathToken AndOperator = new XPathToken( XPathTokenType.Operator, "and" );
			public static readonly XPathToken OrOperator = new XPathToken( XPathTokenType.Operator, "or" );
			public static readonly XPathToken Dot = new XPathToken( XPathTokenType.Dot, "." );
			public static readonly XPathToken DoubleDot = new XPathToken( XPathTokenType.DoubleDot, ".." );
			public static readonly XPathToken OpenBracket = new XPathToken( XPathTokenType.OpenBracket, "[" );
			public static readonly XPathToken CloseBracket = new XPathToken( XPathTokenType.CloseBracket, "]" );
			public static readonly XPathToken OpenParenthesis = new XPathToken( XPathTokenType.OpenParenthesis, "(" );
			public static readonly XPathToken CloseParenthesis = new XPathToken( XPathTokenType.CloseParenthesis, ")" );
			public static readonly XPathToken Slash = new XPathToken( XPathTokenType.Operator, "/" );
			public static readonly XPathToken DoubleSlash = new XPathToken( XPathTokenType.Operator, "//" );
			public static readonly XPathToken DoubleColon = new XPathToken( XPathTokenType.DoubleColon, "::" );
			public static readonly XPathToken At = new XPathToken( XPathTokenType.At, "@" );
			public static readonly XPathToken Colon = new XPathToken( XPathTokenType.Colon, ":" );
		}

		// The tokens that cannot precede an operator, per section 3.7
		static readonly XPathTokenType[] __opPrecedeTokens = new XPathTokenType[] {
			XPathTokenType.At,
			XPathTokenType.DoubleColon,
			XPathTokenType.OpenParenthesis,
			XPathTokenType.OpenBracket,
			XPathTokenType.Operator,
			XPathTokenType.AbsoluteSlash,
			XPathTokenType.AbsoluteDoubleSlash
		};

		// Preceding tokens that signal that '/' and '//' are part of a relative path;
		// from the FilterExpr production, which must precede '/' and '//' according to PathExpr
		static readonly XPathTokenType[] __relativePrecedeTokens = new XPathTokenType[] {
			// FilterExpr: PrimaryExpr
			XPathTokenType.VariableReference,
			XPathTokenType.CloseParenthesis,
			XPathTokenType.Literal,
			XPathTokenType.Number,
			XPathTokenType.FunctionName,

			// FilterExpr/Step: Predicate
			XPathTokenType.CloseBracket,

			// Step: NodeTest: NameTest or ')'
			XPathTokenType.NameTest,

			// AbbreviatedStep
			XPathTokenType.Dot,
			XPathTokenType.DoubleDot,
		};

		static readonly string[] __nodeTypes = new string[] {
			"comment", "text", "processing-instruction", "node"
		};

		public const string ChildAxis = "child";
		public const string AttributeAxis = "attribute";
		public const string SelfAxis = "self";
		public const string DescendantOrSelfAxis = "descendant-or-self";
		public const string ParentAxis = "parent";

		const string __node = "node", __processingInstruction = "processing-instruction";

		/// <summary>
		/// Determines which of the given operators has higher precedence.
		/// </summary>
		/// <param name="op1">First operator.</param>
		/// <param name="op2">Second operator.</param>
		/// <returns>Greater than zero if <paramref name="op1"/> has higher precedence than <paramref name="op2"/>,
		///   less than zero for the opposite, and zero if they are of equal preference.</returns>
		public static int DefaultOperatorComparer( string op1, string op2 ) {
			if( op1 == op2 )
				return 0;

			return OpToPref( op1 ).CompareTo( OpToPref( op2 ) );
		}

		static int OpToPref( string op ) {
			switch( op ) {
				case "or":
					return 0;

				case "and":
					return 1;

				case "=":
				case "!=":
					return 2;

				case "<":
				case "<=":
				case ">":
				case ">=":
					return 3;

				case "+":
				case "-":
				default:
					return 4;

				case "*":
				case "div":
				case "mod":
					return 5;

				case "|":
					return 6;

				case "/":
				case "//":
					return 7;
			}
		}

		public static XPathExpression Parse( string input, bool allowStringEscapes ) {
			return Parse( input, allowStringEscapes, DefaultOperatorComparer );
		}

		public static XPathExpression Parse( string input, bool allowStringEscapes, Comparison<string> operatorPrecedenceComparison ) {
			XPathToken[] tokens = Tokenize( input, allowStringEscapes ).ToArray();

			return Parse( tokens, allowStringEscapes, operatorPrecedenceComparison );
		}

		static XPathExpression Parse( XPathToken[] tokens, bool allowStringEscapes, Comparison<string> operatorPrecedenceComparison ) {
			// This is Dijkstra's Shunting-Yard Algorithm for infix expressions
			// http://en.wikipedia.org/wiki/Shunting_yard_algorithm
			Stack<XPathExpression> expressions = new Stack<XPathExpression>();
			Stack<XPathToken> operatorStack = new Stack<XPathToken>();
			Stack<List<XPathExpression>> functionParamStack = new Stack<List<XPathExpression>>();

			for( int pos = 0; pos < tokens.Length; pos++ ) {
				if( tokens[pos].Type == XPathTokenType.Literal ) {
					string value = tokens[pos].Value.Substring( 1, tokens[pos].Value.Length - 2 );

					if( allowStringEscapes )
						value = UnescapeLiteral( value );

					expressions.Push( new XPathLiteral() {
						Value = value
					} );
				}
				else if( tokens[pos].Type == XPathTokenType.Number ) {
					expressions.Push( new XPathLiteral() {
						Value = double.Parse( tokens[pos].Value )
					} );
				}
				else if( tokens[pos].Type == XPathTokenType.VariableReference ) {
					expressions.Push( new XPathVariableReference() {
						Name = tokens[pos].Value.Substring( 1 )
					} );
				}
				else if( tokens[pos].Type == XPathTokenType.AbsoluteSlash ) {
					// In theory, an absolute path slash should be followed by
					// a NameTest; we don't confirm that here
					expressions.Push( XPathRootExpression.Value );
				}
				else if( tokens[pos].Type == XPathTokenType.AbsoluteDoubleSlash ) {
					// Also should be followed, again we don't confirm
					expressions.Push( new XPathStep() {
						Axis = DescendantOrSelfAxis,
						LocalPart = __node,
						IsNodeType = true,
						Left = XPathRootExpression.Value
					} );
				}
				else if( tokens[pos].Type == XPathTokenType.Dot ) {
					// AbbreviatedStep; short for self::node()
					expressions.Push( new XPathStep() {
						Axis = SelfAxis,
						LocalPart = __node,
						IsNodeType = true
					} );
				}
				else if( tokens[pos].Type == XPathTokenType.DoubleDot ) {
					// AbbreviatedStep; short for parent::node()
					expressions.Push( new XPathStep() {
						Axis = ParentAxis,
						LocalPart = __node,
						IsNodeType = true
					} );
				}
				else if( tokens[pos].Type == XPathTokenType.FunctionName ) {
					operatorStack.Push( tokens[pos] );
					functionParamStack.Push( new List<XPathExpression>() );

					// Verify next token is '('
					pos++;

					if( pos == tokens.Length || tokens[pos].Type != XPathTokenType.OpenParenthesis )
						throw new ArgumentException( "Expected function parameters at " + (tokens[pos - 1].TokenEnd + 1).ToString() );
				}
				else if( tokens[pos].Type == XPathTokenType.Comma ) {
					// Resolve everything on the stack until we get to a function name
					if( functionParamStack.Count == 0 )
						throw new ArgumentException( "Comma was found outside a function call at " + tokens[pos].TokenStart.ToString() );

					List<XPathExpression> functionParams = functionParamStack.Peek();

					for( ;; ) {
						if( operatorStack.Count == 0 )
							throw new ArgumentException( "Comma was found outside a function call at " + tokens[pos].TokenStart.ToString() );

						if( operatorStack.Peek().Type == XPathTokenType.FunctionName )
							break;

						XPathToken op = operatorStack.Pop();

						if( op.Type == XPathTokenType.Operator )
							ResolveOperator( expressions, op );
						else
							throw new ArgumentException( "Unexpected token '" + op.Value + "' on operator stack at " + op.TokenStart );
					}

					functionParams.Add( expressions.Pop() );
				}
				else if( tokens[pos].Type == XPathTokenType.Operator ) {
					// Pop off operators that have higher precedence than we do
					while( operatorStack.Count != 0 ) {
						XPathToken op = operatorStack.Peek();

						if( op.Type != XPathTokenType.Operator )
							break;

						int comp = operatorPrecedenceComparison( tokens[pos].Value, op.Value );

						// Left-associative only; if o1 was right-assoc, this would be comp < 0
						if( comp <= 0 ) {
							ResolveOperator( expressions, op );
							operatorStack.Pop();
						}
						else {
							break;
						}
					}

					operatorStack.Push( tokens[pos] );
				}
				else if( tokens[pos].Type == XPathTokenType.OpenParenthesis ) {
					operatorStack.Push( tokens[pos] );
				}
				else if( tokens[pos].Type == XPathTokenType.OpenBracket ) {
					// Bracket is lower precedence than the composition operators; resolve them first
					while( operatorStack.Count != 0 ) {
						XPathToken token = operatorStack.Peek();

						if( token != XPathToken.Slash && token != XPathToken.DoubleSlash )
							break;

						ResolveOperator( expressions, operatorStack.Pop() );
					}

					operatorStack.Push( tokens[pos] );
				}
				else if( tokens[pos].Type == XPathTokenType.CloseParenthesis ) {
					// Resolve everything on the stack until we get to open parenthesis or function call
					for( ;; ) {
						if( operatorStack.Count == 0 )
							throw new ArgumentException( "Unmatched close parenthesis at " + tokens[pos].TokenStart.ToString() );

						XPathToken op = operatorStack.Pop();

						if( op.Type == XPathTokenType.OpenParenthesis )
							break;

						if( op.Type == XPathTokenType.FunctionName ) {
							// If the previous token wasn't '(', we've got a parameter waiting for us
							if( tokens[pos - 1].Type != XPathTokenType.OpenParenthesis )
								functionParamStack.Peek().Add( expressions.Pop() );

							XPathFunctionCall call = new XPathFunctionCall() { FunctionName = op.Value };
							call.Parameters.AddRange( functionParamStack.Pop() );

							expressions.Push( call );

							break;
						}

						if( op.Type == XPathTokenType.Operator )
							ResolveOperator( expressions, op );
						else
							throw new ArgumentException( "Unexpected token '" + op.Value + "' on operator stack at " + op.TokenStart );
					}
				}
				else if( tokens[pos].Type == XPathTokenType.CloseBracket ) {
					// Resolve everything on the stack until we get to open bracket
					for( ;; ) {
						if( operatorStack.Count == 0 )
							throw new ArgumentException( "Unmatched close bracket at " + tokens[pos].TokenStart.ToString() );

						XPathToken op = operatorStack.Pop();

						if( op.Type == XPathTokenType.OpenParenthesis || op.Type == XPathTokenType.FunctionName )
							throw new ArgumentException( "Unmatched open parenthesis at " + op.TokenStart.ToString() );

						if( op.Type == XPathTokenType.OpenBracket ) {
							// Now combine this expression with previous
							if( expressions.Count < 2 )
								throw new ArgumentException( "Predicate without preceding primary" );

							XPathExpression right = expressions.Pop();
							XPathExpression left = expressions.Pop();

							expressions.Push( new XPathPredicate() {
								Left = left,
								Filter = right
							} );

							break;
						}

						if( op.Type == XPathTokenType.Operator )
							ResolveOperator( expressions, op );
						else
							throw new ArgumentException( "Unexpected token '" + op.Value + "' on operator stack at " + op.TokenStart );
					}
				}
				else {
					// Last thing we're looking for: a path step in pieces
					XPathStep step = new XPathStep() { Axis = ChildAxis };

					// If the previous token specifies an absolute path, add a composition slash to the op stack
					if( pos > 0 && (tokens[pos - 1].Type == XPathTokenType.AbsoluteDoubleSlash || tokens[pos - 1].Type == XPathTokenType.AbsoluteSlash) ) {
						operatorStack.Push( XPathToken.Slash );
					}

					// Production: AxisSpecifier
					if( tokens[pos] == XPathToken.At ) {
						// AbbreviatedAxisSpecifier
						step.Axis = AttributeAxis;
						pos++;
					}
					else if( tokens[pos].Type == XPathTokenType.AxisName ) {
						// AxisName
						step.Axis = tokens[pos].Value;
						pos++;

						if( pos == tokens.Length || tokens[pos] != XPathToken.DoubleColon )
							throw new ArgumentException( "Expected '::' after axis name at " + (tokens[pos - 1].TokenEnd + 1).ToString() );

						pos++;
					}

					if( pos == tokens.Length )
						throw new ArgumentException( "Expected node test at " + (tokens[pos - 1].TokenEnd + 1).ToString() );

					// Production: NodeTest
					if( tokens[pos].Type == XPathTokenType.NodeType ) {
						// NodeType '(' ')' or 'processing-instruction' '(' Literal ')'
						step.IsNodeType = true;
						step.LocalPart = tokens[pos].Value;
						pos++;

						if( pos == tokens.Length || tokens[pos] != XPathToken.OpenParenthesis )
							throw new ArgumentException( "Expected '(' at " + (tokens[pos - 1].TokenEnd + 1).ToString() );

						pos++;

						if( step.LocalPart == __processingInstruction ) {
							// Expect literal
							if( pos == tokens.Length || tokens[pos].Type != XPathTokenType.Literal )
								throw new ArgumentException( "Expected literal at " + (tokens[pos - 1].TokenEnd + 1).ToString() );

							step.Literal = tokens[pos].Value.Substring( 1, tokens[pos].Value.Length - 2 );

							if( allowStringEscapes )
								step.Literal = UnescapeLiteral( step.Literal );

							pos++;
						}

						// Expect ')'
						if( pos == tokens.Length || tokens[pos] != XPathToken.CloseParenthesis )
							throw new ArgumentException( "Expected ')' at " + (tokens[pos - 1].TokenEnd + 1).ToString() );

						pos++;
					}
					else if( tokens[pos].Type == XPathTokenType.NameTest ) {
						// NameTest
						string[] parts = tokens[pos].Value.Split( ':' );

						if( parts.Length == 1 ) {
							step.LocalPart = parts[0];
						}
						else {
							step.Prefix = parts[0];
							step.LocalPart = parts[1];
						}

						pos++;
					}
					else {
						throw new ArgumentException( "Unexpected token " + tokens[pos].Type.ToString() );
					}

					expressions.Push( step );
					pos--;
				}
			}

			// Finally: resolve *all* expressions
			while( operatorStack.Count != 0 ) {
				XPathToken op = operatorStack.Pop();

				if( op.Type != XPathTokenType.Operator )
					throw new ArgumentException( "Unresolved " + op.Type.ToString() + " on operator stack at end of expression" );

				ResolveOperator( expressions, op );
			}

			if( expressions.Count == 0 )
				throw new ArgumentException( "No expressions found on expression stack" );

			if( expressions.Count > 1 )
				throw new ArgumentException( "Too many expressions found on expression stack" );

			return expressions.Pop();
		}

		const string __hexChars = "0123456789ABCDEF";

		/// <summary>
		/// Unescapes a string literal.
		/// </summary>
		/// <param name="input">A string literal (not including quotes) that might contain escape characters.</param>
		/// <returns>A string with the escapes interpreted as characters.</returns>
		public static string UnescapeLiteral( string input ) {
			// Handle the usual C# escapes
			char[] result = new char[input.Length];
			int outPos = 0, inPos = 0;

			for( inPos = 0; inPos < input.Length; inPos++ ) {
				if( input[inPos] == '\\' ) {
					inPos++;

					if( input[inPos] == '\\' )
						result[outPos++] = '\\';
					else if( input[inPos] == '\'' )
						result[outPos++] = '\'';
					else if( input[inPos] == '\"' )
						result[outPos++] = '\"';
					else if( input[inPos] == '0' )
						result[outPos++] = '\0';
					else if( input[inPos] == 'a' )
						result[outPos++] = '\a';
					else if( input[inPos] == 'b' )
						result[outPos++] = '\b';
					else if( input[inPos] == 'f' )
						result[outPos++] = '\f';
					else if( input[inPos] == 'n' )
						result[outPos++] = '\n';
					else if( input[inPos] == 'r' )
						result[outPos++] = '\r';
					else if( input[inPos] == 't' )
						result[outPos++] = '\t';
					else if( input[inPos] == 'v' )
						result[outPos++] = '\v';
					else if( input[inPos] == 'x' ) {
						// Up to four characters
						int charValue = 0;
						inPos++;

						for( int i = 0; i < 4 && inPos < input.Length; i++ ) {
							int hexValue = __hexChars.IndexOf( char.ToUpperInvariant( input[inPos] ) );

							if( hexValue == -1 )
								break;

							charValue = (charValue << 4) | hexValue;
							inPos++;
						}

						result[outPos++] = (char) charValue;
						inPos--;
					}
					else if( input[inPos] == 'u' ) {
						// Exactly four characters
						int charValue = 0;
						inPos++;

						for( int i = 0; i < 4 && inPos < input.Length; i++ ) {
							int hexValue = __hexChars.IndexOf( char.ToUpperInvariant( input[inPos] ) );

							if( hexValue == -1 )
								throw new ArgumentException( "Bad hex digit in the middle of Unicode escape sequence.", "input" );

							charValue = (charValue << 4) | hexValue;
							inPos++;
						}

						result[outPos++] = (char) charValue;
						inPos--;
					}
				}
				else {
					result[outPos++] = input[inPos];
				}
			}

			return new string( result, 0, outPos );
		}

		/// <summary>
		/// Escapes the given string.
		/// </summary>
		/// <param name="input">String to escape.</param>
		/// <returns>Escaped string.</returns>
		public static string EscapeLiteral( string input ) {
			StringBuilder output = new StringBuilder( input.Length * 2 );

			for( int i = 0; i < input.Length; i++ ) {
				char value = input[i];

				if( value == '\n' )
					output.Append( "\\n" );
				else if( value == '\r' )
					output.Append( "\\r" );
				else if( value == '\\' )
					output.Append( "\\\\" );
				else if( value == '\'' )
					output.Append( "\\\'" );
				else if( value == '\"' )
					output.Append( "\\\"" );
				else if( value == '\0' )
					output.Append( "\\0" );
				else if( value == '\a' )
					output.Append( "\\a" );
				else if( value == '\b' )
					output.Append( "\\b" );
				else if( value == '\f' )
					output.Append( "\\f" );
				else if( value == '\t' )
					output.Append( "\\t" );
				else if( value == '\v' )
					output.Append( "\\v" );
				else if( char.IsControl( value ) ) {
					output.Append( "\\u" );
					output.Append( __hexChars[(((int) value) >> 12) & 0xF] );
					output.Append( __hexChars[(((int) value) >> 8) & 0xF] );
					output.Append( __hexChars[(((int) value) >> 4) & 0xF] );
					output.Append( __hexChars[(((int) value) >> 0) & 0xF] );
				}
				else {
					output.Append( value );
				}
			}

			return output.ToString();
		}

		static void ResolveOperator( Stack<XPathExpression> expressions, XPathToken op ) {
			if( expressions.Count < 2 )
				throw new ArgumentException( "Too many operators in expression" );

			XPathExpression right = expressions.Pop();
			XPathExpression left = expressions.Pop();

			if( op == XPathToken.Slash ) {
				// Right hand should be a path
				XPathStep rightStep = right as XPathStep;

				if( rightStep == null )
					throw new ArgumentException( "Slash not followed by path step." );

				rightStep.Left = left;
				expressions.Push( rightStep );
				return;
			}

			if( op == XPathToken.DoubleSlash ) {
				// Right hand should be a path
				XPathStep rightStep = right as XPathStep;

				if( rightStep == null )
					throw new ArgumentException( "Double slash not followed by path step." );

				rightStep.Left = new XPathStep() {
					Axis = DescendantOrSelfAxis,
					LocalPart = __node,
					IsNodeType = true,
					Left = left
				};

				expressions.Push( rightStep );
				return;
			}

			expressions.Push( new XPathOperatorExpression() {
				Left = left,
				Operator = op.Value,
				Right = right
			} );
		}

		static IEnumerable<XPathToken> Tokenize( string input, bool allowStringEscapes ) {
			int pos = 0;
			XPathToken lastToken = null;

			while( pos < input.Length ) {
				int start = pos;

				switch( input[pos] ) {
					case '(':
						lastToken = new XPathToken( XPathTokenType.OpenParenthesis, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case ')':
						lastToken = new XPathToken( XPathTokenType.CloseParenthesis, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case '[':
						lastToken = new XPathToken( XPathTokenType.OpenBracket, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case ']':
						lastToken = new XPathToken( XPathTokenType.CloseBracket, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case '@':
						lastToken = new XPathToken( XPathTokenType.At, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case '.':
						if( pos < input.Length - 1 && input[pos + 1] == '.' ) {
							lastToken = new XPathToken( XPathTokenType.DoubleDot, input, pos, pos + 1 );
							yield return lastToken;
							pos += 2;
						}
						else {
							lastToken = new XPathToken( XPathTokenType.Dot, input, pos, pos );
							yield return lastToken;
							pos++;
						}

						break;

					case ':':
						if( pos < input.Length - 1 && input[pos + 1] == ':' ) {
							lastToken = new XPathToken( XPathTokenType.DoubleColon, input, pos, pos + 1 );
							yield return lastToken;
							pos += 2;
							break;
						}

						throw new ArgumentException( "Unexpected single colon at " + pos.ToString() );

					case '\'':
					case '\"': {
						// Literal
						char startChar = input[pos];

						do {
							if( allowStringEscapes && pos < input.Length && input[pos] == '\\' )
								pos++;

							pos++;
						} while( pos < input.Length && input[pos] != startChar );

						if( pos == input.Length )
							throw new ArgumentException( "Unterminated literal starting at " + start.ToString() );

						lastToken = new XPathToken( XPathTokenType.Literal, input, start, pos );
						yield return lastToken;
						pos++;
						break;
					}

					case '*':
						// Could be a MultiplyOperator or a NameTest
						// "If there is a preceding token and the preceding token is not one of
						// @, ::, (, [, , or an Operator, then a * must be recognized as a
						// MultiplyOperator"
						if( lastToken != null && !__opPrecedeTokens.Contains( lastToken.Type ) ) {
							lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos );
							yield return lastToken;
							pos++;
							break;
						}
						else {
							lastToken = new XPathToken( XPathTokenType.NameTest, input, pos, pos );
							yield return lastToken;
							pos++;
							break;
						}

					case '\x20':
					case '\x09':
					case '\x0D':
					case '\x0A':
						// Whitespace, skip
						pos++;
						break;

					// Operators
					case '/':
						// "/" and "//" are treated as operators *only* if they appear
						// after a PrimaryExpr (VariableReference, CloseParenthesis, Literal, Number, FunctionCall),
						// or Predicate (CloseBracket); otherwise they signal the start of an absolute path

						if( pos < input.Length - 1 && input[pos + 1] == '/' ) {
							if( lastToken != null && __relativePrecedeTokens.Contains( lastToken.Type ) )
								lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos + 1 );
							else
								lastToken = new XPathToken( XPathTokenType.AbsoluteDoubleSlash, input, pos, pos + 1 );

							yield return lastToken;
							pos += 2;
						}
						else {
							if( lastToken != null && __relativePrecedeTokens.Contains( lastToken.Type ) )
								lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos );
							else
								lastToken = new XPathToken( XPathTokenType.AbsoluteSlash, input, pos, pos );

							yield return lastToken;
							pos++;
						}

						break;

					case '|':
					case '+':
					case '=':
						lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case ',':
						lastToken = new XPathToken( XPathTokenType.Comma, input, pos, pos );
						yield return lastToken;
						pos++;
						break;

					case '!':
						if( pos == input.Length - 1 )
							throw new ArgumentException( "Path ends with !" );

						if( input[pos + 1] == '=' ) {
							lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos + 1 );
							yield return lastToken;
							pos += 2;
							break;
						}

						throw new ArgumentException( "Unexpected character '" + input[pos + 1] + "' after ! at " + pos.ToString() );

					case '<':
					case '>':
						if( pos < input.Length - 1 && input[pos + 1] == '=' ) {
							lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos + 1 );
							yield return lastToken;
							pos += 2;
						}
						else {
							lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos );
							yield return lastToken;
							pos++;
						}

						break;

					default: {
						// Try to identify a number first
						bool minus = false;

						if( input[pos] == '-' ) {
							if( lastToken.Type == XPathTokenType.Operator ||
									lastToken.Type == XPathTokenType.OpenParenthesis ||
									lastToken.Type == XPathTokenType.OpenBracket ) {
								// Unary minus
								minus = true;
								pos++;
							}
							else {
								// Binary minus
								lastToken = new XPathToken( XPathTokenType.Operator, input, pos, pos );
								yield return lastToken;
								pos++;
								break;
							}
						}

						if( char.IsDigit( input[pos] ) || input[pos] == '.' ) {
							do pos++; while( pos < input.Length && (char.IsDigit( input[pos] ) || input[pos] == '.') );
							lastToken = new XPathToken( XPathTokenType.Number, input, start, pos - 1 );
							yield return lastToken;
							break;
						}

						if( minus )
							throw new ArgumentException( "Unexpected character '-' at " + pos.ToString() );

						bool varRef = false;

						if( input[pos] == '$' ) {
							varRef = true;
							pos++;
						}

						// Pick up NCNames
						if( XmlConvert.IsStartNCNameChar( input[pos] ) ) {
							do pos++; while( pos < input.Length && XmlConvert.IsNCNameChar( input[pos] ) );

							// "If there is a preceding token and the preceding token is not one of
							// @, ::, (, [, , or an Operator, then an NCName must be recognized as
							// an OperatorName"
							if( !varRef && lastToken != null && !__opPrecedeTokens.Contains( lastToken.Type ) ) {
								lastToken = new XPathToken( XPathTokenType.Operator, input, start, pos - 1 );
								yield return lastToken;
							}
							else {
								// Lookahead to next token
								int nextPos = pos;

								while( nextPos < input.Length && XmlConvert.IsWhitespaceChar( input[nextPos] ) )
									nextPos++;

								if( !varRef && nextPos < input.Length && input[nextPos] == '(' ) {
									// NodeType or FunctionName
									if( __nodeTypes.Contains( input.Substring( start, pos - start ) ) )
										lastToken = new XPathToken( XPathTokenType.NodeType, input, start, pos - 1 );
									else
										lastToken = new XPathToken( XPathTokenType.FunctionName, input, start, pos - 1 );

									yield return lastToken;
									break;
								}
								else if( !varRef && nextPos < input.Length - 1 && input[nextPos] == ':' && input[nextPos + 1] == ':' ) {
									// AxisName
									lastToken = new XPathToken( XPathTokenType.AxisName, input, start, pos - 1 );
									yield return lastToken;
									break;
								}
								else {
									// NameTest
									// XPath spec does not specify allowing whitespace inside a nametest,
									// so we don't expect any

									// Two places we could be: at the beginning of NCName ':' '*' or QName
									if( pos < input.Length - 1 && input[pos] == ':' ) {
										if( !varRef && input[pos + 1] == '*' ) {
											lastToken = new XPathToken( XPathTokenType.NameTest, input, start, pos + 1 );
											yield return lastToken;
											pos += 2;
											break;
										}

										// Expect NCName after colon
										if( !XmlConvert.IsStartNCNameChar( input[pos + 1] ) )
											throw new ArgumentException( "Unexpected character '" + input[pos + 1].ToString() + "' in middle of QName at " + (pos + 1).ToString() );

										// Consume NCName
										pos++;
										do pos++; while( pos < input.Length && XmlConvert.IsNCNameChar( input[pos] ) );

										lastToken = new XPathToken( varRef ?
											XPathTokenType.VariableReference : XPathTokenType.NameTest,
											input, start, pos - 1 );
										yield return lastToken;
										break;
									}
									else {
										// Standalone NCName is QName
										lastToken = new XPathToken( varRef ?
											XPathTokenType.VariableReference : XPathTokenType.NameTest,
											input, start, pos - 1 );
										yield return lastToken;
										break;
									}

								}
							}

							break;
						}

						throw new ArgumentException( "Unrecognized token '" + input[start] + "' at " + start.ToString() );
					}
				}
			}
		}
	}
}
