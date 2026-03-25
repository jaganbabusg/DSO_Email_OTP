using DSO_Email_OTP.Data;
using DSO_Email_OTP.Models;
using Microsoft.EntityFrameworkCore;

namespace DSO_Email_OTP.Services
{
    public class EmailOtpService
    {
        private readonly AppDbContext _context;

        public EmailOtpService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateOtpAsync(string emailAddress)
        {
            if (!emailAddress.EndsWith(".dso.org.sg"))
                return "STATUS_EMAIL_INVALID";

            var otpCode = new Random().Next(100000, 999999).ToString();

            var entry = new EmailOtp
            {
                EmailAddress = emailAddress,
                OtpCode = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(1),
                AttemptCount = 0,
                IsUsed = false
            };

            _context.EmailOtps.Add(entry);
            await _context.SaveChangesAsync();

            bool emailSent = SendEmail(emailAddress, otpCode);

            return emailSent ? "STATUS_EMAIL_OK" : "STATUS_EMAIL_FAIL";
        }

        public async Task<string> VerifyOtpAsync(string emailAddress, string inputOtp)
        {
            var entry = await _context.EmailOtps
                .Where(x => x.EmailAddress == emailAddress && !x.IsUsed)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (entry == null)
                return "STATUS_OTP_FAIL";

            if (DateTime.UtcNow > entry.ExpiryTime)
                return "STATUS_OTP_TIMEOUT";

            if (entry.AttemptCount >= 10)
                return "STATUS_OTP_FAIL";

            entry.AttemptCount++;

            if (entry.OtpCode == inputOtp)
            {
                entry.IsUsed = true;
                await _context.SaveChangesAsync();
                return "STATUS_OTP_OK";
            }

            await _context.SaveChangesAsync();
            return entry.AttemptCount >= 10 ? "STATUS_OTP_FAIL" : "STATUS_OTP_FAIL";
        }

        private bool SendEmail(string emailAddress, string otpCode)
        {
            // Mock implementation
            Console.WriteLine($"Sending OTP {otpCode} to {emailAddress}");
            return true;
        }
    }
}
