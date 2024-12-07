using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class PasscodeRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Passcode is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Passcode must be a 6-digit number.")]
        public int Passcode { get; set; }
    }
}
