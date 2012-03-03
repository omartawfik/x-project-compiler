﻿namespace LanguageCompiler.Nodes.ClassMembers
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Irony.Parsing;
    using LanguageCompiler.Errors;
    using LanguageCompiler.Nodes.Statements;

    /// <summary>
    /// Holds all data related to a "Operator Definition" rule.
    /// </summary>
    public class OperatorDefinition : MemberDefinition
    {
        /// <summary>
        /// Operator of this method.
        /// </summary>
        private string operatorDefined;

        /// <summary>
        /// Parameters of this method.
        /// </summary>
        private List<Parameter> parameters = new List<Parameter>();

        /// <summary>
        /// Code block of this method.
        /// </summary>
        private Block block;

        /// <summary>
        /// Gets the operator of this method.
        /// </summary>
        public string OperatorDefined
        {
            get { return this.operatorDefined; }
        }

        /// <summary>
        /// Forms a valid tree node representing this object.
        /// </summary>
        /// <returns>The formed tree node.</returns>
        public override TreeNode GetGUINode()
        {
            TreeNode result = base.GetGUINode();
            result.Text = "Operator Declaration";
            result.Nodes.Add(new TreeNode(this.operatorDefined));

            TreeNode parameters = new TreeNode("Parameters: Count = " + this.parameters.Count);
            foreach (Parameter p in this.parameters)
            {
                parameters.Nodes.Add(p.GetGUINode());
            }

            result.Nodes.Add(parameters);
            if (this.block == null)
            {
                result.Nodes.Add("No Body!");
            }
            else
            {
                result.Nodes.Add(this.block.GetGUINode());
            }

            return result;
        }

        /// <summary>
        /// Recieves an irony ParseTreeNode and constructs its contents.
        /// </summary>
        /// <param name="node">The irony ParseTreeNode.</param>
        public override void RecieveData(ParseTreeNode node)
        {
            base.RecieveData(node);
            this.operatorDefined = node.ChildNodes[5].Token.Text;
            foreach (ParseTreeNode child in node.ChildNodes[6].ChildNodes[1].ChildNodes)
            {
                Parameter p = new Parameter();
                p.RecieveData(child);
                this.parameters.Add(p);
            }

            if (node.ChildNodes[7].Term.Name == LanguageGrammar.Block.Name)
            {
                this.block = new Block();
                this.block.RecieveData(node.ChildNodes[7]);
                this.EndLocation = this.block.EndLocation;
            }
            else
            {
                this.block = null;
                this.EndLocation = node.ChildNodes[6].Token.Location;
            }
        }

        /// <summary>
        /// Checks for semantic errors within this node.
        /// </summary>
        public override void CheckSemantics()
        {
            if (this.parameters.Count > 0 && (this.operatorDefined == "++" || this.operatorDefined == "--"))
            {
                this.AddError(ErrorType.PostfixOperatorsCannotHaveParameters, this.operatorDefined);
            }
        }
    }
}
