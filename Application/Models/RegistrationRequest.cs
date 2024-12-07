using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RegistrationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ICNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
