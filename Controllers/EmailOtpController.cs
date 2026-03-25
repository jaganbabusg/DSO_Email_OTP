using DSO_Email_OTP.DTO;
using DSO_Email_OTP.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSO_Email_OTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailOtpController : ControllerBase
    {
        private readonly EmailOtpService _service;

        public EmailOtpController(EmailOtpService service)
        {
            _service = service;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateOtp([FromBody] GenerateEmailOtp request)
        {
            var result = await _service.GenerateOtpAsync(request.EmailAddress);
            return Ok(result);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyEmailOtp request)
        {
            var result = await _service.VerifyOtpAsync(request.EmailAddress, request.OtpCode);
            return Ok(result);
        }
    }
}
