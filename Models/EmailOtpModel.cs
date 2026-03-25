namespace DSO_Email_OTP.Models
{
    public class EmailOtpModel
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpiryTime { get; set; }
        public int AttemptCount { get; set; }
        public bool IsUsed { get; set; }
    }
}
