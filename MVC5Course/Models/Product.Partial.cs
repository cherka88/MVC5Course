namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量
        {
            get
            {
                return this.OrderLine.Count;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Price>300 && this.Stock < 1)
            {
                yield return new ValidationResult("商品價格數量異常", new[] { "Price", "Stock" });
            }
            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        [DisplayName("商品名稱")]
       
        public string ProductName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        [DisplayName("價格")]
        public Nullable<decimal> Price { get; set; }
        [DisplayName("啟用")]
        public Nullable<bool> Active { get; set; }
        [DisplayName("數量")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
