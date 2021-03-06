﻿namespace LanguageCompiler.Nodes.Expressions.Basic
{
    using System.Windows.Forms;
    using Irony.Parsing;
    using LanguageCompiler.Semantics;
    using LanguageCompiler.Semantics.ExpressionTypes;

    /// <summary>
    /// Holds all data related to a "UnaryExpression" rule.
    /// </summary>
    public class UnaryExpression : ExpressionNode
    {
        /// <summary>
        /// Operator of expression.
        /// </summary>
        private string operatorDefined;

        /// <summary>
        /// RHS of expression.
        /// </summary>
        private ExpressionNode rhs;

        /// <summary>
        /// Gets the operator of expression.
        /// </summary>
        public string OperatorDefined
        {
            get { return this.operatorDefined; }
        }

        /// <summary>
        /// Gets the right hand side of the expression.
        /// </summary>
        public ExpressionNode RHS
        {
            get { return this.rhs; }
        }

        /// <summary>
        /// Forms a valid tree node representing this object.
        /// </summary>
        /// <returns>The formed tree node.</returns>
        public override TreeNode GetGUINode()
        {
            TreeNode result = new TreeNode(this.operatorDefined + " Unary Expression");
            result.Nodes.Add(this.rhs.GetGUINode());
            return result;
        }

        /// <summary>
        /// Recieves an irony ParseTreeNode and constructs its contents.
        /// </summary>
        /// <param name="node">The irony ParseTreeNode.</param>
        public override void RecieveData(ParseTreeNode node)
        {
            this.operatorDefined = node.ChildNodes[0].Token.Text;
            this.rhs = ExpressionsFactory.GetUnaryExpr(node.ChildNodes[1]);

            this.StartLocation = node.ChildNodes[0].Token.Location;
            this.EndLocation = this.rhs.EndLocation;
        }
        
        /// <summary>
        /// Checks for semantic errors within this node.
        /// </summary>
        /// <param name="scopeStack">The scope stack associated with this node.</param>
        /// <returns>True if errors are found, false otherwise.</returns>
        public override bool CheckSemanticErrors(ScopeStack scopeStack)
        {
            if (this.rhs.CheckSemanticErrors(scopeStack))
            {
                return true;
            }
            else
            {
                this.GetExpressionType(scopeStack);
                return false;
            }
        }

        /// <summary>
        /// Gets the expression type of this node.
        /// </summary>
        /// <param name="stack">Current Scope Stack.</param>
        /// <returns>The expression type of this node.</returns>
        public override ExpressionType GetExpressionType(ScopeStack stack)
        {
            return this.ExpressionType = this.rhs.GetExpressionType(stack);
        }

        /// <summary>
        /// Checks if this expression can be assigned to.
        /// </summary>
        /// <returns>True if this expression can be assigned to, false otherwise.</returns>
        public override bool IsAssignable()
        {
            return false;
        }
    }
}