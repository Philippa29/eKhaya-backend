using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class CreateApplicantDto : PersonDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string[] RoleNames { get; set; }
    }
}
