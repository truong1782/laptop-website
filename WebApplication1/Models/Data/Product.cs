namespace WebApplication1.Models.Data
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

        [Required]
        [StringLength(50)]
        public string productName { get; set; }

        [Column(TypeName = "money")]
        public decimal productPrice { get; set; }

        [Column(TypeName = "text")]
        public string productDetail { get; set; }

        [StringLength(55)]
        public string image { get; set; }

        public DateTime? dateCreate { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
