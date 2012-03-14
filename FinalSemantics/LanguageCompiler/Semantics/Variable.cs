﻿namespace LanguageCompiler.Semantics
{
    using LanguageCompiler.Semantics.ExpressionTypes;

    /// <summary>
    /// Holds all data related to a variable, so it can be used in a scope stack.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Type of this variable.
        /// </summary>
        private ExpressionType type;

        /// <summary>
        /// Name of this variable.
        /// </summary>
        private string name;

        /// <summary>
        /// Indicates if this variable is initialized.
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the Variable class.
        /// </summary>
        /// <param name="type">Type of this variable.</param>
        /// <param name="name">Name of this variable.</param>
        /// <param name="isInitialized">Indicates if this variable is initialized.</param>
        public Variable(ExpressionType type, string name, bool isInitialized)
        {
            this.type = type;
            this.name = name;
            this.isInitialized = isInitialized;
        }

        /// <summary>
        /// Gets the type of this variable.
        /// </summary>
        public ExpressionType Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Gets the name of this variable.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this variable is initialized.
        /// </summary>
        public bool IsInitialized
        {
            get { return this.isInitialized; }
            set { this.IsInitialized = value; }
        }
    }
}
