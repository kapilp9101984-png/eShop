using Auth.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entity
{
    public class PasswordResetTokens : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public int UserID { get; set; }
        public required string Token { get; set;  }
        public DateTime ExpiredOn { get; set; }
        
    }
}
