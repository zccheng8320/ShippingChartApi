using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingCartDetail : BaseEntity
    {
        [Key]
        public int ShoppingCartId { get; set; }
        [Key]
        public int ProductId { get; set; } 
        [Required]
        public int ProductCount { get; set; }
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}
