using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 一個Device 只會有一個 購物車
    /// </summary>
    public class ShoppingCart : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        public int ShoppingCartId { get; set; }
        /// <summary>
        /// 此購物車擁有者的Guid(裝置guid會存在Session之中)
        /// </summary>
        [Required]
        public string OwnerDeviceGuid { get; set; }                
        public virtual ICollection<ShoppingCartDetail> ShoppingCartDetails { get; set; }
    }    
}
