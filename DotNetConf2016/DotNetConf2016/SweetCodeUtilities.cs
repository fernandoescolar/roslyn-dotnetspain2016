namespace DotNetConf2016
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Rename;
    using System.Threading;
    using System.Threading.Tasks;

    public static class SweetCodeUtilities
    {
        public static Task<Solution> Sweeten(Document document, SyntaxToken token, CancellationToken cancellationToken)
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
