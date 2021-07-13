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
            Blogs = new HashSet<Blog>();
            Orders = new HashSet<Order>();
        }

        public int userID { get; set; }

        public int roleID { get; set; }

        [Required]
        [StringLength(100)]
        public string fullName { get; set; }

        [Required]
        [StringLength(55)]
        public string userName { get; set; }

        [StringLength(55)]
        public string password { get; set; }

        [Required]
        [StringLength(55)]
        public string email { get; set; }

        [StringLength(12)]
        public string phoneNumber { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string image { get; set; }

        public bool gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateOfBirth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Role Role { get; set; }
    }
}
