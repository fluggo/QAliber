using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace QAliber.Repository.CommonTestCases.Eval
{
	public class CodeEvaluator
	{
		public static object Evaluate(string code)
		{
			Assembly engineAssembly = Assembly.GetAssembly(typeof(QAliber.Engine.Controls.UIControlBase));

			CSharpCodeProvider c = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();

			cp.ReferencedAssemblies.Add("system.dll");
			cp.ReferencedAssemblies.Add("system.xml.dll");
			cp.ReferencedAssemblies.Add("system.data.dll");
			cp.ReferencedAssemblies.Add("system.drawing.dll");
			cp.ReferencedAssemblies.Add("system.windows.forms.dll");

			//TODO : test for x86 , x64
			cp.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.0\WindowsBase.dll");
			cp.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.0\UIAutomationTypes.dll");
			cp.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.0\UIAutomationClient.dll");
			cp.ReferencedAssemblies.Add(engineAssembly.Location);

			cp.CompilerOptions = "/t:library";
			cp.GenerateInMemory = true;

			StringBuilder sb = new StringBuilder("");
			sb.Append("using System;\n");
			sb.Append("using System.IO;\n");
			sb.Append("using System.Xml;\n");
			sb.Append("using System.Data;\n");
			sb.Append("using System.Windows.Forms;\n");
			sb.Append("using System.Windows;\n");
			sb.Append("using QAliber.Engine.Controls;\n"); 
			sb.Append("using QAliber.Engine.Controls.UIA;\n");

			sb.Append("namespace CSCodeEvaler{ \n");
			sb.Append("public class CSCodeEvaler{ \n");
			sb.Append("public object EvalCode(){\n");
			sb.Append(code);
			sb.Append("}\n");
			sb.Append("}\n");
			sb.Append("}\n");

			CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
			if (cr.Errors.Count > 0)
			{
				throw new ArgumentException("The expression '" + code + "' does not compile to C#, or does not return bool");
			}

			System.Reflection.Assembly a = cr.CompiledAssembly;
			object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

			Type t = o.GetType();
			MethodInfo mi = t.GetMethod("EvalCode");

			return mi.Invoke(o, null);
		   
		}
	}
}
