using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using QAliber.Engine.Patterns;
using System.Xml;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.IO;

namespace QAliber.Engine.Controls.UIA {
	/// <summary>
	/// Compares two UIA control nodes for the correct sort order.
	/// </summary>
	/// <remarks>This works entirely on the basis of the control's index, as discovered
	///   and set when retrieving all children.</remarks>
	class XPathOrderComparer : IComparer<IXPathNode> {
		public int Compare( IXPathNode x, IXPathNode y ) {
			Stack<int> cx = ToIndexStack( x ), cy = ToIndexStack( y );

			while( cx.Count != 0 && cy.Count != 0 ) {
				int result = cx.Pop().CompareTo( cy.Pop() );

				if( result != 0 )
					return result;
			}

			if( cx.Count != 0 ) {
				// X is a descendant of Y, so X is later
				return 1;
			}

			if( cy.Count != 0 )
				return -1;

			return 0;
		}

		private Stack<int> ToIndexStack( IXPathNode node ) {
			XPathAdapter adapter = node as XPathAdapter;
			XPathAttributeAdapter attr = node as XPathAttributeAdapter;

			if( attr != null )
				adapter = attr.Owner;

			if( adapter != null ) {
				UIAControl control = adapter.Owner;
				Stack<int> result = new Stack<int>();

				do {
					result.Push( control.Index );
					control = (UIAControl) control.Parent;
				} while( control != null );

				return result;
			}

			throw new ArgumentException( "Trying to sort a node I don't know." );
		}
	}

	class UIAXPathEvaluator : XPathEvaluator {
		public UIAXPathEvaluator( TextWriter log ) : base( new XPathAdapter( Desktop.UIA, log ),
			new XmlNamespaceManager( new NameTable() ),
			new XPathOrderComparer() ) {
		}

		// TODO: Add variable support!

		public override object EvaluateFunction( XPathContext context, string name, object[] parameters ) {
			if( name == "match-regex" ) {
				if( parameters.Length != 2 )
					throw new ArgumentException( "Wrong number of parameters for the match-regex function." );

				string str = ToString( parameters[0] ), reg = ToString( parameters[1] );
				return Regex.IsMatch( str, reg, RegexOptions.CultureInvariant );
			}

			return base.EvaluateFunction( context, name, parameters );
		}
	}

	class XPathAdapter : IXPathNode {
		UIAControl _owner;
		TextWriter _log;

		public XPathAdapter( UIAControl owner, TextWriter log ) {
			_owner = owner;
			_log = log;
		}

		public HashSet<IXPathNode> FindNodesByAxis( string axis ) {
			if( axis == null )
				throw new ArgumentNullException( "axis" );

			if( axis == XPath.ChildAxis ) {
				// ElementNotAvailableExceptions are often thrown here; we're going to just
				// not return any elements in that case
				if( _log != null ) {
					_log.WriteLine( "Checking children of {0}:", _owner.CodePath );
				}

				try {
					UIControlBase[] children = _owner.GetChildren();

					if( _log != null ) {
						foreach( UIAControl child in children ) {
							_log.WriteLine( "  /{0}[@Name='{1}' and @ID='{2}' and @ClassName='{3}']", child.XPathElementName, child.Name, child.ID, child.ClassName );
						}
					}

					return new HashSet<IXPathNode>(
						children.Cast<UIAControl>().Select( c => new XPathAdapter( c, _log ) ) );
				}
				catch( ElementNotAvailableException ) {
					if( _log != null ) {
						_log.WriteLine( "  Failed with ElementNotAvailableException" );
					}

					return new HashSet<IXPathNode>();
				}
				catch( Exception ex ) {
					if( _log != null ) {
						_log.WriteLine( "  Failed with exception: " + ex.ToString() );
					}

					throw;
				}
			}
			else if( axis == XPath.AttributeAxis ) {
				// Find the appropriate attribute on this node
				PropertyDescriptor[] descs = TypeDescriptor.GetProperties( _owner ).Cast<PropertyDescriptor>().ToArray();

				if( _log != null ) {
					_log.WriteLine( "Fetching properties of {0}:", _owner.CodePath );

					foreach( PropertyDescriptor desc in descs ) {
						try {
							_log.WriteLine( "  @{0}: {1}", desc.Name, desc.GetValue( _owner ) );
						}
						catch( Exception ex ) {
							_log.WriteLine( "  @{0}: ({1})", desc.Name, ex.GetType().Name );
						}
					}
				}

				return new HashSet<IXPathNode>(
					TypeDescriptor.GetProperties( _owner ).Cast<PropertyDescriptor>()
						.Select( prop => new XPathAttributeAdapter( this, prop ) ) );
			}
			else if( axis == XPath.SelfAxis ) {
				return new HashSet<IXPathNode>( new IXPathNode[] { this } );
			}
			else {
				throw new NotSupportedException( "The \"" + axis + "\" axis is not supported at this time." );
			}
		}

		public string GetStringValue() {
			IText text = _owner.GetControlInterface<IText>();

			if( text != null )
				return text.Text;

			return string.Empty;
		}

		public string Namespace {
			get { return null; }
		}

		public string LocalName {
			get { return _owner.XPathElementName; }
		}

		public bool Equals( IXPathNode other ) {
			XPathAdapter adapter = other as XPathAdapter;

			if( object.ReferenceEquals( adapter, null ) )
				return false;

			if( object.ReferenceEquals( adapter, this ) )
				return true;

			return _owner.UIAutomationElement.Equals( adapter._owner.UIAutomationElement );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as IXPathNode );
		}

		public override int GetHashCode() {
			return _owner.UIAutomationElement.GetHashCode();
		}

		public UIAControl Owner
			{ get { return _owner; } }
	}

	class XPathAttributeAdapter : IXPathNode {
		XPathAdapter _owner;
		PropertyDescriptor _prop;

		public XPathAttributeAdapter( XPathAdapter owner, PropertyDescriptor prop ) {
			_owner = owner;
			_prop = prop;
		}

		public HashSet<IXPathNode> FindNodesByAxis( string axis ) {
			if( axis == XPath.ChildAxis || axis == XPath.AttributeAxis )
				return new HashSet<IXPathNode>();

			if( axis == XPath.SelfAxis || axis == XPath.DescendantOrSelfAxis )
				return new HashSet<IXPathNode>( new IXPathNode[] { this } );

			if( axis == XPath.ParentAxis )
				return new HashSet<IXPathNode>( new IXPathNode[] { _owner } );

			throw new NotImplementedException( "Support for axis " + axis + " not implemented." );
		}

		public string GetStringValue() {
			return Convert.ToString( _prop.GetValue( _owner.Owner ), CultureInfo.InvariantCulture );
		}

		public string Namespace {
			get { return null; }
		}

		public string LocalName {
			get { return _prop.Name; }
		}

		public bool Equals( IXPathNode other ) {
			XPathAttributeAdapter adapter = other as XPathAttributeAdapter;

			if( object.ReferenceEquals( adapter, null ) )
				return false;

			if( object.ReferenceEquals( adapter, this ) )
				return true;

			return _owner == adapter._owner && _prop == adapter._prop;
		}

		public override bool Equals( object obj ) {
			return Equals( obj as IXPathNode );
		}

		public override int GetHashCode() {
			return unchecked(_owner.GetHashCode() * _prop.GetHashCode());
		}

		public XPathAdapter Owner
			{ get { return _owner; } }
	}
}
