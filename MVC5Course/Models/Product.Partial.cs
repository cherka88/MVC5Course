namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
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
