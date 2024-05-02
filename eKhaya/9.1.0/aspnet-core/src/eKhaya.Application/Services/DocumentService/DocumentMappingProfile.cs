using AutoMapper;
using eKhaya.Domain.Documents;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.DocumentAppService
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<DocumentDto, Document>()

                .ForMember(x=> x.OwnerID, e => e.Ignore())
                .ForMember(x => x.Id, e => e.Ignore());

        }
    }
}
