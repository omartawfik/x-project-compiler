﻿namespace LanguageCompiler.Nodes.Expressions.Complex
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Irony.Parsing;
    using LanguageCompiler.Semantics;
    using LanguageCompiler.Semantics.ExpressionTypes;

    /// <summary>
    /// Holds all data related to a "ArrayExpression" rule.
    /// </summary>
    public class ArrayExpression : ExpressionNode
    {
        /// <summary>
        /// expression in LHS.
        /// </summary>
        private ExpressionNode lhs;

        /// <summary>
        /// Indexes of expression.
        /// </summary>
        private List<ExpressionNode> indexes = new List<ExpressionNode>();

        /// <summary>
        /// Forms a valid tree node representing this object.
        /// </summary>
        /// <returns>The formed tree node.</returns>
        public override TreeNode GetGUINode()
        {
            TreeNode result = new TreeNode("Array Expression");
            result.Nodes.Add(this.lhs.GetGUINode());

            TreeNode args = new TreeNode("Indexes: Count = " + this.indexes.Count);
            foreach (BaseNode child in this.indexes)
            {
                args.Nodes.Add(child.GetGUINode());
            }

            result.Nodes.Add(args);
            return result;
        }

        /// <summary>
        /// Recieves an irony ParseTreeNode and constructs its contents.
        /// </summary>
        /// <param name="node">The irony ParseTreeNode.</param>
        public override void RecieveData(ParseTreeNode node)
        {
            this.lhs = ExpressionsFactory.GetPrimaryExpr(node.ChildNodes[0]);
            foreach (ParseTreeNode child in node.ChildNodes[2].ChildNodes)
            {
                this.indexes.Add(ExpressionsFactory.GetBaseExpr(child));
            }

            this.StartLocation = this.lhs.StartLocation;
            this.EndLocation = node.ChildNodes[3].Token.Location;
        }

        /// <summary>
        /// Gets the expression type of this node.
        /// </summary>
        /// <param name="stack">Current Scope Stack.</param>
        /// <returns>The expression type of this node.</returns>
        public override ExpressionType GetExpressionType(ScopeStack stack)
        {
            return (this.lhs.GetExpressionType(stack) as ArrayExpressionType).ElementType;
        }

        /// <summary>
        /// Checks if this expression can be assigned to.
        /// </summary>
        /// <returns>True if this expression can be assigned to, false otherwise.</returns>
        public override bool IsAssignable()
        {
            return true;
        }
    }
}