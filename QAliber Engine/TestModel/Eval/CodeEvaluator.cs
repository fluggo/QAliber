using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace QAliber.TestModel.Eval
{
	public class CodeEvaluator
	{
		public static void Evaluate(string code, ReturnCodeType retType, out object retVal)
		{
			CSharpCodeProvider c = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();

			string strRetType = string.Empty;
			switch (retType)
			{
				case ReturnCodeType.Text:
					strRetType = "string";
					break;
				case ReturnCodeType.Number:
					strRetType = "double";
					break;
				case ReturnCodeType.Boolean:
					strRetType = "bool";
					break;
				default:
					strRetType = "string";
					break;
			}
			

			cp.ReferencedAssemblies.Add("system.dll");
			cp.ReferencedAssemblies.Add("system.xml.dll");
			cp.ReferencedAssemblies.Add("system.data.dll");

			cp.CompilerOptions = "/t:library";
			cp.GenerateInMemory = true;

			StringBuilder sb = new StringBuilder("");
			sb.Append("using System;\n");
			sb.Append("using System.IO;\n");
			sb.Append("using System.Xml;\n");
			sb.Append("using System.Data;\n");

			sb.Append("namespace CSCodeEvaler{ \n");
			sb.Append("public class CSCodeEvaler{ \n");
			sb.Append("public " + strRetType + " EvalCode(){\n");
			sb.Append("return " + code + "; \n");
			sb.Append("} \n");
			sb.Append("} \n");
			sb.Append("}\n");

			CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
			if (cr.Errors.Count > 0)
			{
				throw new ArgumentException("The expression '" + code + "' does not compile to C#", cr.Errors[0].ErrorText);
			}

			System.Reflection.Assembly a = cr.CompiledAssembly;
			object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

			Type t = o.GetType();
			MethodInfo mi = t.GetMethod("EvalCode");

			object s = mi.Invoke(o, null);
			retVal = s;
			return;
		}
	}

	public enum ReturnCodeType
	{
		Text,
		Number,
		Boolean
	}
}
