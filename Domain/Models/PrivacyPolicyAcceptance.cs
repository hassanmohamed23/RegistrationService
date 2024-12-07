using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Enums;

namespace Domain.Models
{
    public class PrivacyPolicyAcceptance
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public PolicyVersion PolicyVersion { get; set; } 
        public DateTime AcceptedAt { get; set; }
        public virtual User User { get; set; }
    }
}
