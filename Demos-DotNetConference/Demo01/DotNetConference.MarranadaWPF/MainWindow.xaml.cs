using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using DotNetConference.MarranadaWPF.Resources;

namespace DotNetConference.MarranadaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StringWriter streamWriter;

        public MainWindow()
        {
            InitializeComponent();
            InputConsole.Text = ShitLiterals.Start;

        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            InputConsole.Text = ResultConsole.Text = string.Empty;
            streamWriter.Flush();
        }

        private void InputConsole_TextChanged_1(object sender, EventArgs e)
        {
            ResultConsole.Text = string.Empty;
            var memStream = new MemoryStream();
            streamWriter = new StringWriter();
            Console.SetOut(streamWriter);
            var memoryStream = new MemoryStream();
            var sb = new StringBuilder();

            SyntaxTree tree = CSharpSyntaxTree.ParseText(InputConsole.Text);
            var root = (CompilationUnitSyntax)tree.GetRoot();

            var compilation = CSharpCompilation.Create("AutoCompiled.test", new List<SyntaxTree>() { tree })
                .AddReferences(MetadataReference.CreateFromFile(typeof(Assembly).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IQueryable).Assembly.Location));

            var result = compilation.Emit(memoryStream);

            result.Diagnostics.ToList().ForEach(d => Console.WriteLine(sb.AppendLine(d.GetMessage()).ToString()));

            if (result.Success)
            {
                byte[] b = memoryStream.ToArray();
                var assembly = Assembly.Load(b ?? null);
                Console.WriteLine(string.Format("{0} loaded", assembly.FullName));
                if (assembly.EntryPoint != null)
                    try
                    {
                        Console.WriteLine(">>>>>>>>> YEAHHH Your code is Working! you're the fucking boss <<<<<<<<<<<< ");
                        Console.WriteLine(ShitLiterals.Ok);
                        Console.WriteLine();
                        Console.WriteLine("OUTPUT:");
                        assembly.EntryPoint.Invoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OOOOOOOOOUUUUCCHHH you're code has crashed!!!");
                        Console.WriteLine(ex.StackTrace);
                    }

            }
            else
                Console.WriteLine(string.Format(">>>>>> {0} ", ShitLiterals.GetOne));

            ResultConsole.Text += streamWriter.ToString();
        }



    }
}
