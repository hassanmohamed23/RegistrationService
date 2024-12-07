using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Enums;

namespace Application.Dtos
{
    public class PrivacyPolicyAcceptanceDto
    {        
        public int Id { get; set; }
        public int UserId { get; set; }
        public PolicyVersion PolicyVersion { get; set; }
        public DateTime AcceptedAt { get; set; }
    }
}
