using static Domain.Common.Enums;

namespace PaymentGateway.Models
{
    public class PrivacyPolicyAcceptanceRequest
    {
        public int UserId { get; set; }
        public PolicyVersion PolicyVersion { get; set; }
    }
}
