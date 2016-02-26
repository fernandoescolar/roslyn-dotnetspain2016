namespace DotNetConf2016.Console
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Linq;

    class Program
    {
        // args[0] = contains a valid VS Solution (.sln)
        static void Main(string[] args)
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            var soln = ws.OpenSolutionAsync(args[0]).Result;
            var proj = soln.Projects.Single();
            var compilation = proj.GetCompilationAsync().Result;

            foreach (var tree in compilation.SyntaxTrees)
            {
                var classes = tree.GetRoot().DescendantNodesAndSelf().Where(x => x.IsKind(SyntaxKind.ClassDeclaration));
                foreach (var c in classes)
                {
                    var classDeclaration = (ClassDeclarationSyntax)c;
                    var token = classDeclaration.Identifier;
                    if (token.Text.IsSweetie()) continue;

                    System.Console.WriteLine($"[Error] The name '{token.Text}' isn't so sweet");
                }
            }

            System.Console.ReadKey();
        }
    }
}
