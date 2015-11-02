namespace CommonMarket.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        [StringLength(550)]
        public string CommentBody { get; set; }

        public int DomainEntityId { get; set; }

        public int EntityRecordId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(250)]
        public string CommentedBy { get; set; }
    }
}
