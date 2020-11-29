namespace Products.Core.Models
{
    using System;
    
    /// <summary>
    /// Base entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Date when the entity is created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Date when the entity was last updated
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
