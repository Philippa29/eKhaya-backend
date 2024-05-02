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

        public  Guid Unit { get; set; }

        public Guid Applicant { get; set; }


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

        public bool Evicited { get; set; }
        public string Declaration { get; set; }
        public string TermsandConditions { get; set; }


        public ApplicationType ApplicationType { get; set; }


    }
}
