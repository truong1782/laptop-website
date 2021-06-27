namespace WebApplication1.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int contactID { get; set; }

        public int userID { get; set; }

        [Required]
        [StringLength(30)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string detail { get; set; }

        public DateTime dateCreate { get; set; }

        public bool status { get; set; }

        public virtual User User { get; set; }
    }
}
