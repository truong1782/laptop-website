namespace WebApplication1.Models.Data
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

        public int value { get; set; }

        public int conditionMoney { get; set; }

        [Column(TypeName = "ntext")]
        public string note { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }
}
