using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notification.Domain.Entity
{
    public class MobileFormatter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public required string TemplateName { get; set; }

        public required string Body { get; set; }

        public bool IsActive { get; set; } = false;

    }
}
