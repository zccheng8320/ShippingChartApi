using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class OrderDetail : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        [Required]
        public int ProductCount { get; set; }
        /// <summary>
        /// 結帳時的產品單價
        /// </summary>
        [Required]
        public decimal CheckoutPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
