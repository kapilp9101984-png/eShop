using Auth.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entity
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }        
        public required string UserName { get; set; }
        public required string MobileNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsEmailVerified { get; set; }
        public int FailedLoginAttempts {  get; set; }
        public DateTime LockoutEnd { get; set; }
       
    }
}
