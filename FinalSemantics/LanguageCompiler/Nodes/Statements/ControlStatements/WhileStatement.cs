﻿namespace LanguageCompiler.Nodes.Statements.ControlStatements
{
    using System.Windows.Forms;
    using Irony.Parsing;
    using LanguageCompiler.Errors;
    using LanguageCompiler.Nodes.Expressions;
    using LanguageCompiler.Nodes.Types;
    using LanguageCompiler.Semantics;

    /// <summary>
    /// Holds all data related to a "WhileStatement" rule.
    /// </summary>
    public class WhileStatement : BaseNode
    {
        /// <summary>
        /// The expression for this while loop.
        /// </summary>
        private ExpressionNode expression;

        /// <summary>
        /// The body for this while loop.
        /// </summary>
        private Block body;

        /// <summary>
        /// Gets the expression of the while statement.
        /// </summary>
        public ExpressionNode Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// Gets the body of the while statement.
        /// </summary>
        public Block Body
        {
            get { return this.body; }
        }

        /// <summary>
        /// Forms a valid tree node representing this object.
        /// </summary>
        /// <returns>The formed tree node.</returns>
        public override TreeNode GetGUINode()
        {
            TreeNode result = new TreeNode("While Statement");
            result.Nodes.Add(this.expression.GetGUINode());
            if (this.body != null)
            {
                result.Nodes.Add(this.body.GetGUINode());
            }
            else
            {
                result.Nodes.Add("No Body!");
            }

            return result;
        }

        /// <summary>
        /// Recieves an irony ParseTreeNode and constructs its contents.
        /// </summary>
        /// <param name="node">The irony ParseTreeNode.</param>
        public override void RecieveData(ParseTreeNode node)
        {
            this.expression = ExpressionsFactory.GetBaseExpr(node.ChildNodes[2]);
            if (node.ChildNodes[4].Term.Name == LanguageGrammar.Block.Name)
            {
                this.body = new Block();
                this.body.RecieveData(node.ChildNodes[4]);
                this.EndLocation = this.body.EndLocation;
            }
            else
            {
                this.body = null;
                this.EndLocation = node.ChildNodes[3].Token.Location;
            }

            this.StartLocation = node.ChildNodes[0].Token.Location;
        }

        /// <summary>
        /// Checks for semantic errors within this node.
        /// </summary>
        /// <param name="scopeStack">The scope stack associated with this node.</param>
        /// <returns>True if errors are found, false otherwise.</returns>
        public override bool CheckSemanticErrors(ScopeStack scopeStack)
        {
            bool foundErrors = false;

            scopeStack.AddLevel(ScopeType.Loop, this);
            foundErrors |= this.expression.CheckSemanticErrors(scopeStack);
            foundErrors |= this.body.CheckSemanticErrors(scopeStack);
            scopeStack.DeleteLevel();

            if (foundErrors)
            {
                return foundErrors;
            }

            if (this.expression.GetExpressionType(scopeStack).IsEqualTo(Literal.ConstructExpression(Literal.Bool)) == false)
            {
                this.AddError(ErrorType.ExpressionNotBoolean);
                foundErrors = true;
            }

            return foundErrors;
        }

        /// <summary>
        /// Checks if a statement or block of code returns a value.
        /// </summary>
        /// <returns>True if it returns a value, false otherwise.</returns>
        public override bool ReturnsAValue()
        {
            return false;
        }
    }
}