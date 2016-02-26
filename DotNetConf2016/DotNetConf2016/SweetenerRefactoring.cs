namespace DotNetConf2016
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeRefactorings;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Rename;
    using System.Composition;
    using System.Threading;
    using System.Threading.Tasks;

    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(SweetenerRefactoring)), Shared]
    internal class SweetenerRefactoring : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var node = root.FindNode(context.Span);
            var classDeclaration = node as ClassDeclarationSyntax;

            if (classDeclaration == null) return;

            var token = classDeclaration.Identifier;
            if (token.Text.IsSweetie()) return;

            var action = CodeAction.Create("[Refactor] Sweeten it", c => SweetenWord(context.Document, token, c));
            context.RegisterRefactoring(action);
        }

        private static Task<Solution> SweetenWord(Document document, SyntaxToken token, CancellationToken cancellationToken)
        {
            var newName = token.Text.Sweeten();
            return RenameAsync(document, token, cancellationToken, newName);
        }

        private static async Task<Solution> RenameAsync(Document document, SyntaxToken token, CancellationToken cancellationToken, string newName)
        {
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(token.Parent, cancellationToken);

            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;
            var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

            return newSolution;
        }
    }
}
