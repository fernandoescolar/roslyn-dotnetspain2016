namespace DotNetConf2016
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Immutable;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SweetenerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SweetyRule";
        private const string Title = "It's no so sweetie";
        private const string MessageFormat = "The name: '{0}' isn't so sweetie";
        private const string Description = "This name isn't so sweetie";
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSweetSymbolName, new[] { SymbolKind.Namespace, SymbolKind.NamedType, SymbolKind.Method, SymbolKind.Property, SymbolKind.Event, SymbolKind.Field });
            context.RegisterSyntaxNodeAction(AnalyzeSweetSytaxNode, new[] { SyntaxKind.Parameter, SyntaxKind.LocalDeclarationStatement });
        }

        private static void AnalyzeSweetSymbolName(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = context.Symbol;
            var name = namedTypeSymbol.Name;
            AnalyzeName(context, name, namedTypeSymbol.Locations[0]);
        }

        private static void AnalyzeSweetSytaxNode(SyntaxNodeAnalysisContext context)
        {
            var parameter = context.Node as ParameterSyntax;
            if (parameter != null)
            {
                AnalyzeSweetSytaxNode(context, parameter);
            }

            var declaration = context.Node as LocalDeclarationStatementSyntax;
            if (declaration != null)
            {
                AnalyzeSweetSytaxNode(context, declaration);
            }
        }

        private static void AnalyzeSweetSytaxNode(SyntaxNodeAnalysisContext context, ParameterSyntax parameter)
        {
            var name = parameter.Identifier.Text;
            var location = parameter.Identifier.GetLocation();
            AnalyzeName(context, name, location);
        }

        private static void AnalyzeSweetSytaxNode(SyntaxNodeAnalysisContext context, LocalDeclarationStatementSyntax declaration)
        {
            foreach (var variable in declaration.Declaration.Variables)
            {
                var name = variable.Identifier.Text;
                var location = variable.Identifier.GetLocation();
                AnalyzeName(context, name, location);
            }
        }

        private static void AnalyzeName(SymbolAnalysisContext context, string name, Location location)
        {
            if (!name.IsSweetie())
            {
                var diagnostic = Diagnostic.Create(Rule, location, name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static void AnalyzeName(SyntaxNodeAnalysisContext context, string name, Location location)
        {
            if (!name.IsSweetie())
            {
                var diagnostic = Diagnostic.Create(Rule, location, name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
