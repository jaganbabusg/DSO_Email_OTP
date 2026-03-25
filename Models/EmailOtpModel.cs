using System.ComponentModel.DataAnnotations;

namespace DSO_Email_OTP.Models
{
    public class EmailOtpModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(6)]
        public string OtpCode { get; set; }

        public DateTime ExpiryTime { get; set; }

        public int AttemptCount { get; set; } = 0;

        public bool IsUsed { get; set; } = false;
    }
}
