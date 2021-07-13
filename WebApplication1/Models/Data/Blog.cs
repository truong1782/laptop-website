namespace WebApplication1.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        public int IDBlog { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime? DateCreate { get; set; }

        public int IDTopic { get; set; }

        public int UserID { get; set; }

        public virtual Topic Topic { get; set; }

        public virtual User User { get; set; }
    }
}
