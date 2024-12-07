using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ICNumber { get; set; }
        public string MobileNumber { get; set; }
        public int? MobileVerificationCode { get; set; }
        public string Email { get; set; }
        public int? EmailVerificationCode { get; set; }
        public string? Passcode { get; set; }

        public virtual ICollection<PrivacyPolicyAcceptance> PrivacyPolicyAcceptanceList { get; set; }

    }
}
