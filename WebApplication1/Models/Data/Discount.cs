namespace WebApplication1.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        public int discountID { get; set; }

        [Required]
        [StringLength(30)]
        public string discountCode { get; set; }

        [Column(TypeName = "money")]
        public decimal value { get; set; }

        [Column(TypeName = "money")]
        public decimal conditionMoney { get; set; }

        [Column(TypeName = "text")]
        public string note { get; set; }
    }
}
