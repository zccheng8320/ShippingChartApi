using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// ProductId
        /// </summary>
        [Key]
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// 貨號
        /// </summary>
        [Required]
        public string ProductNo { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 進貨數量
        /// </summary>
        [Required]
        public uint PurchaseCount { get; set; }
        /// <summary>
        /// 銷量
        /// </summary>
        [Required]
        public uint SalesVolume { get; set; }
        /// <summary>
        /// 價格
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}
