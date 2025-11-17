using System.ComponentModel.DataAnnotations.Schema;

namespace Medinexus.Domain.Entities
{
    [Table("usermaster")]
    public class UserMaster
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("mobileno")]
        public string? MobileNo { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("username")]
        public string? UserName { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("createdat")]
        public DateTime? CreatedAt { get; set; }

        [Column("modifiedat")]
        public DateTime? ModifiedAt { get; set; }

        [Column("refereshtoken")]
        public string? RefreshToken { get; set; }

        [Column("refereshtokenexpiry")]
        public DateTime? RefreshTokenExpiry { get; set; }
        // Foreign key to Chemist
        [Column("chemistid")]
        public int? ChemistId { get; set; }

        // Navigation property
        [ForeignKey("ChemistId")]
        public virtual ChemistMaster? Chemist { get; set; }

    }

}
