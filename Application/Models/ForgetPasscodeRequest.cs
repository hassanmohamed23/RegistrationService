using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class ForgetPasscodeRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "New Passcode is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Passcode must be a 6-digit number.")]
        public int NewPasscode { get; set; }
    }
}
