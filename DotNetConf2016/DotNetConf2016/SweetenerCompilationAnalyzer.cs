namespace DotNetConf2016
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;
    using System.Linq;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SweetenerCompilationAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SweetyRuleBuild";
        private const string Title = "It's no so sweetie";
        private const string MessageFormat = "The name: '{0}' isn't so sweetie";
        private const string Description = "This name isn't so sweetie";
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(AnalyzeSweetNames);
        }

        private void AnalyzeSweetNames(CompilationAnalysisContext context)
        {
            foreach (var tree in context.Compilation.SyntaxTrees)
            {
                var classes = tree.GetRoot().DescendantNodesAndSelf().Where(x => x.IsKind(SyntaxKind.ClassDeclaration));
                foreach (var c in classes)
                {
                    var classDeclaration = (ClassDeclarationSyntax)c;
                    var token = classDeclaration.Identifier;
                    if (token.Text.IsSweetie()) continue;

                    var diagnostic = Diagnostic.Create(Rule, token.GetLocation(), token.Text);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
