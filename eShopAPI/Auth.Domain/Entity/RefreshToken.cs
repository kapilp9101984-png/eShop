using Auth.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entity
{
    public class RefreshToken : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public int UserID { get; set;  }
        public string Token { get; set;  }
        public DateTime ExpireOn { get; set; }
        public  bool IsRevoke { get; set; }
        public string ReplacedByToken { get; set; }
        public DateTime RevokeOn { get; set; }
        public  string IPAddress { get; set; }
        public bool IsLogOut {  get; set; }

    }
}
