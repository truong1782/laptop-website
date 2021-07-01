namespace WebApplication1.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int userID { get; set; }

        public int roleID { get; set; }

        [Required]
        [StringLength(55)]
        public string fullName { get; set; }

        [Required]
        [StringLength(55)]
        public string userName { get; set; }

        [Required]
        [StringLength(55)]
        public string password { get; set; }

        [StringLength(55)]
        public string email { get; set; }

        [StringLength(10)]
        public string phoneNumber { get; set; }

        [StringLength(55)]
        public string address { get; set; }

        [StringLength(55)]
        public string image { get; set; }

        public bool? gender { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateOfBirth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Role Role { get; set; }
    }
}
