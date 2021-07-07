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
        [Display(Name = "Mã")]
        public string discountCode { get; set; }

        [Display(Name = "Giá trị")]
        public int value { get; set; }

        [Display(Name = "Số tiền điều kiện")]
        public int conditionMoney { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Ghi chú")]
        public string note { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime startDate { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime endDate { get; set; }
    }
}
