using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notification.Domain.Entity
{
    public class EmailFormatter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public required string TemplateName { get; set; }

        public string Subject { get; set; } = string.Empty;

        public required string Body { get; set; }

        public bool IsActive { get; set; } = false;

    }
}
