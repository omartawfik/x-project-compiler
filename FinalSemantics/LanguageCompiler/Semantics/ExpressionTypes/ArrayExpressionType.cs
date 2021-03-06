﻿namespace LanguageCompiler.Semantics.ExpressionTypes
{
    /// <summary>
    /// Represents the type of an expression that returns an array.
    /// </summary>
    public class ArrayExpressionType : ExpressionType
    {
        /// <summary>
        /// The data type of each element in this array.
        /// </summary>
        private ExpressionType elementType;

        /// <summary>
        /// Number of dimensions in this array.
        /// </summary>
        private int numberOfDimensions;

        /// <summary>
        /// Initializes a new instance of the ArrayExpressionType class.
        /// </summary>
        /// <param name="elementType">The data type of each element in this array.</param>
        /// <param name="numberOfDimensions">Number of dimensions in this array.</param>
        public ArrayExpressionType(ExpressionType elementType, int numberOfDimensions)
        {
            this.elementType = elementType;
            this.numberOfDimensions = numberOfDimensions;
        }

        /// <summary>
        /// Gets the data type of each element in this array.
        /// </summary>
        public ExpressionType ElementType
        {
            get { return this.elementType; }
        }

        /// <summary>
        /// Checks two expression types for equality.
        /// </summary>
        /// <param name="other">Other type to check.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        public override bool IsEqualTo(ExpressionType other)
        {
            if (other is ArrayExpressionType)
            {
                ArrayExpressionType otherArray = other as ArrayExpressionType;
                return this.numberOfDimensions == otherArray.numberOfDimensions
                    && this.elementType.IsEqualTo(otherArray.elementType);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the name of this expression type.
        /// </summary>
        /// <returns>The name of this expression type.</returns>
        public override string GetName()
        {
            return this.elementType.GetName() + " Array";
        }
    }
}