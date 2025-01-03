using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagementAPI.Models
{
    /// <summary>
    /// Represents a product in the inventory.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        [SwaggerSchema("The unique ID of the product.")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [SwaggerSchema("The name of the product.")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        [SwaggerSchema("A brief description of the product.")]
        public string description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [SwaggerSchema("The price of the product.")]
        public decimal price { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the product.
        /// </summary>
        [SwaggerSchema("The date and time when the product was created.")]
        public DateTime createdat { get; set; }
    }
}
