namespace CommonMarket.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DomainEntity")]
    public partial class DomainEntity
    {
        public DomainEntity()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        public int DomainEntityId { get; set; }

        [Required]
        [StringLength(150)]
        public string DomainEntityType { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
