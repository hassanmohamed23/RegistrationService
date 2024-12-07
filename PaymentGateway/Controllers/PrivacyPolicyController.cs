using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrivacyPolicyController : Controller
    {
        private readonly IPrivacyPolicyAcceptanceService _privacyPolicyAcceptanceService;

        public PrivacyPolicyController(IPrivacyPolicyAcceptanceService privacyPolicyAcceptanceService)
        {
            _privacyPolicyAcceptanceService = privacyPolicyAcceptanceService;
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptPolicy([FromBody] PrivacyPolicyAcceptanceRequest request)
        {
            var isAccepted = await _privacyPolicyAcceptanceService.AcceptPolicyAsync(request.UserId, request.PolicyVersion);

            if (!isAccepted)
            {
                return BadRequest(new { IsSuccess = false, Message = "User has already accepted this version of the Privacy Policy." });
            }

            return Ok(new { IsSuccess = true, Message = "Privacy Policy accepted successfully." });
        }

        [HttpGet("acceptances/{userId}")]
        public async Task<IActionResult> GetUserAcceptances(int userId)
        {
            var acceptances = await _privacyPolicyAcceptanceService.GetUserAcceptancesAsync(userId);
            return Ok(acceptances);
        }
    }
}
