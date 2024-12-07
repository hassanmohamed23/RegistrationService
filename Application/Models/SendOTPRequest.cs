using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class SendOTPRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
