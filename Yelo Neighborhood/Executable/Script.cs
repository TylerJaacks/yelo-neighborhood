using System;
using System.Collections.Generic;
using Yelo.Debug;
using System.Reflection.Emit;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using System.Reflection;
using Yelo.Shared;

namespace Yelo.Neighborhood
{
    public partial class Executable
    {
        public class Script
        {
            public string Name { get; set; }

            public string Code { get; set; }

            public string FileType { get; set; }

            public Script()
            { Code = ""; }

            public override string ToString()
            { return Name; }

            public void Run(string filename)
            {
                CSharpCodeProvider codeProvider = new CSharpCodeProvider();

                CompilerParameters compilerparams = new CompilerParameters();
                compilerparams.GenerateExecutable = false;
                compilerparams.GenerateInMemory = true;
                compilerparams.ReferencedAssemblies.Add("Yelo.Debug.dll");

                CompilerResults results = codeProvider.CompileAssemblyFromSource(compilerparams,
               @"using System;
                 using Yelo.Debug;

                 namespace Yelo.Neighborhood
                 {
                    public static class Scripting
                    {
                        public static void Script(Xbox XBox, string Filename)
                        {"
                            + Code +
                      @"}
                    }
                }"
                );

                if (results.Errors.HasErrors)
                {
                    var errors = new System.Text.StringBuilder();
                    foreach (CompilerError error in results.Errors)
                    {
                        errors.AppendFormat("Line {0},{1}\t: {2}\n",
                               error.Line, error.Column, error.ErrorText);
                    }
                    MessageBox.Show(errors.ToString(), "Compile Errors", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                Type scripting = results.CompiledAssembly.GetExportedTypes()[0];
                scripting.InvokeMember("Script", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { XBoxIO.XBox, filename });
            }
        }
    };
}