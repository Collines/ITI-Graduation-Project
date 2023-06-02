using GraduationProject_DAL.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GraduationProject_DAL.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z ]+$")]
        [MaxLength(50)]
        public required string SenderName { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public required string Email { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z 0-9%]+$",ErrorMessage ="Enter a valid Subject")]
        [MaxLength(255)]
        public required string Subject { get; set; }

        [DefaultValue(MessageStatus.Pending)]
        [EnumDataType(typeof(MessageStatus))]
        public MessageStatus? Status { get; set; } = MessageStatus.Pending;
        [Required]
        public required string Body { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}
