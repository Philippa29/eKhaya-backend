using AutoMapper;
using eKhaya.Domain.Users;
using eKhaya.Domain.Workers;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.WorkerService
{
    public class WorkerMappingProfile : Profile
    {
        public WorkerMappingProfile()
        {
            CreateMap<Worker, WorkerDto>();
            CreateMap<WorkerDto, Worker>();
            CreateMap<CreateWorkerDto, Worker>();
        }
    }
}
