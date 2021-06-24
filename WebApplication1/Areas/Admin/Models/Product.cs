namespace WebApplication1.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        
        public int productID { get; set; }

        public int categoryID { get; set; }

        public int brandID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        [StringLength(50)]
        public string productName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá tiền")]
        [Display(Name = "Giá tiền")]
        [Column(TypeName = "money")]
        public decimal productPrice { get; set; }

        [Display(Name = "Chi tiết sản phẩm")]
        [Column(TypeName = "text")]
        public string productDetail { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(55)]
        public string image { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? dateCreate { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
