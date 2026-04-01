using Microsoft.AspNetCore.Mvc;
using PasswordValidatorApi.Models;
using PasswordValidatorApi.Services;

namespace PasswordValidatorApi.Controllers
{
    [ApiController]
    [Route("api/password")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _service;

        public PasswordController(IPasswordService service)
        {
            _service = service;
        }

        [HttpPost("validate")]
        public ActionResult<PasswordResponse> Validate([FromBody] PasswordRequest request)
        {
            var isValid = _service.Validate(request.Password);
            return Ok(new PasswordResponse { IsValid = isValid });
        }
    }
}
