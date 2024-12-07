using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class VerifyOTPRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "OTP must be a 4-digit number.")]
        public int OTP { get; set; }
    }
}
