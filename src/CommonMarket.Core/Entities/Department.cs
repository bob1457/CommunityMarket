namespace CommonMarket.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string DepartmentName { get; set; }

        [StringLength(450)]
        public string DepartmentDesc { get; set; }

        [StringLength(150)]
        public string DepartmentImgUrl { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
