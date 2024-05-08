using Abp.Application.Services.Dto;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class ApplicationsDto : EntityDto<Guid>
    {

        public string Name { get; set;  }

        public string Surname { get; set; }

        public Guid Applicant { get; set; }

        public Guid Property { get; set; }


        public ApplicationStatus ApplicationStatus { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public CommunityType ComunityType { get; set; }
        public  string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyContactNumber { get; set; }

        public string Occupation { get; set; }

        public int Salary { get; set; }

        public int MonthsWorked { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Insolvent { get; set; }

        public bool Evicted { get; set; }
        


        public ApplicationType ApplicationType { get; set; }


    }
}
