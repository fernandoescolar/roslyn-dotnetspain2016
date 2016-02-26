using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotNetConference.TransformTool
{
    public class CustomVisitor : CSharpSyntaxRewriter
    {
        public CustomVisitor(SyntaxTree tree) {

        }

        public override SyntaxNode VisitWhileStatement(WhileStatementSyntax node)
        {
            //Generate For statement
            node = (WhileStatementSyntax)base.VisitWhileStatement(node);

            // Get the different syntax nodes components of the While Statement
            var whileKeyword = node.WhileKeyword;
            var openParen = node.OpenParenToken;
            var condition = node.Condition;
            var closeParen = node.CloseParenToken;
            var whileStatement = node.Statement;

            // Preserve as much trivia and formatting info as possible while constructing the new nodes.
            var newDoKeyword = SyntaxFactory.Token(whileKeyword.LeadingTrivia, SyntaxKind.DoKeyword, closeParen.TrailingTrivia);
            var newWhileKeyword = SyntaxFactory.Token(SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker), SyntaxKind.WhileKeyword, whileKeyword.TrailingTrivia);
            var semiColonToken = SyntaxFactory.Token(SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker), SyntaxKind.SemicolonToken, whileStatement.GetTrailingTrivia());
            var newCloseParen = SyntaxFactory.Token(closeParen.LeadingTrivia, SyntaxKind.CloseParenToken, SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker));
            var newDoStatement = whileStatement.ReplaceTrivia(whileStatement.GetTrailingTrivia().Last(), SyntaxFactory.TriviaList());

            return SyntaxFactory.DoStatement(newDoKeyword, newDoStatement, newWhileKeyword, openParen, condition, newCloseParen, semiColonToken);

        }

    }
}
