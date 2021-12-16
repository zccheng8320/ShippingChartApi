using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order : BaseEntity
    {
        [Key]
        public int OrderId { get; set; }
        /// <summary>
        /// 訂單擁有者的電話號碼
        /// </summary>
        public string OwnerPhoneNumber { get; set; }
        /// <summary>
        /// 結帳金額(未包含折扣費用與運費)
        /// </summary>
        public decimal CheckoutAmount { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public decimal DiscountAmount { get; set; }
        /// <summary>
        /// 運費
        /// </summary>
        public int ShippingFee { get; set; }

        /// <summary>
        /// 訂單金額(含運費與折扣)
        /// </summary>
        public decimal OrderAmount => CheckoutAmount - DiscountAmount + ShippingFee;
        /// <summary>
        /// 此訂單是否使用折價卷
        /// </summary>
        public bool HasCoupon { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
