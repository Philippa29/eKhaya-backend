//using Abp.Application.Services;
//using Abp.Domain.Repositories;
//using AutoMapper;
//using eKhaya.Domain.Leases;
//using eKhaya.Domain.Units;
//using eKhaya.Domain.Users;
//using eKhaya.Services.Dtos;
//using eKhaya.Sessions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using static System.Collections.Specialized.BitVector32;

//namespace eKhaya.Services.LeaseAppService
//{
//    public class LeaseAppService : ApplicationService, ILeaseAppService
//    {
//        private const string BASE_FILE_PATH = "App_Data/Leases";

//        private readonly IRepository<Lease, Guid> _leaseRepository;
//        private readonly IMapper _mapper;
//        private readonly IRepository<Unit, Guid> _unitRepository;
//        private readonly IRepository<Resident, Guid> _residentRepository;
//        private readonly ISessionAppService _session;


//        public LeaseAppService(IRepository<Lease, Guid> leaseRepository, IMapper mapper, IRepository<Unit, Guid> unitRepository, IRepository<Resident, Guid> residentRepository, ISessionAppService session)
//        {
//            _leaseRepository = leaseRepository;
//            _mapper = mapper;
//            _unitRepository = unitRepository;
//            _residentRepository = residentRepository;
//            _session = session;
//        }

//        public async Task<ReturnLeaseDto> CreateLeaseAsync([FromForm] LeaseDto input)
//        {
//            var lease = _mapper.Map<Lease>(input);
//            lease.DocumentName = await SaveLeaseFile(input.File);
//            var insertedLease = await _leaseRepository.InsertAsync(lease);


//            var loginUser = await _session.GetCurrentLoginInformations();

//            if (loginUser.User == null)
//            {
//                throw new Exception("User not found");
//            }

//            // Retrieve owner details
//            var owner = await _unitRepository.FirstOrDefaultAsync(u => u.Id == input.OwnerID);
//            if (owner == null)
//            {
//                // Handle the case where owner is not found
//                throw new Exception("Owner not found");
//            }

//            // Retrieve tenant details
//            var tenant = await _residentRepository.FirstOrDefaultAsync(a => a.User.UserName == loginUser.User.UserName);
//            if (tenant == null)
//            {
//                // Handle the case where tenant is not found
//                throw new Exception("Tenant not found");
//            }

//            // Convert the uploaded file to Base64
//            string base64File = ConvertFileToBase64(input.File);

//            // Create a ReturnLeaseDto object and fill it with data
//            var returnLeaseDto = new ReturnLeaseDto
//            {
//                Id = insertedLease.Id,
//                OwnerId = owner.Id,
//                FileName = insertedLease.DocumentName,
//                Base64 = base64File, // Assign the Base64 string
//                RentAmount = insertedLease.RentAmount,
//                DepositPaid = insertedLease.DepositPaid,
//                TenantId = tenant.Id,
//                LeaseName = insertedLease.DocumentName,
//                ResidentName = tenant.Name,
//                ResidentSurname = tenant.Surname,
//                EmailAddress = tenant.EmailAddress
//            };

//            return returnLeaseDto;
//        }


//        private string ConvertFileToBase64(IFormFile file)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                file.CopyTo(memoryStream);
//                byte[] bytes = memoryStream.ToArray();
//                return Convert.ToBase64String(bytes);
//            }
//        }


//        public async Task<List<ReturnLeaseDto>> GetAllLeasesAsync(Guid ownerId)
//        {
//            var leases = await _leaseRepository
//                .GetAll()
//                .Where(x => x.OwnerID.Id == ownerId)
//                .ToListAsync();

//            var returnLeaseDtos = new List<ReturnLeaseDto>();

//            foreach (var lease in leases)
//            {
//                // Retrieve tenant details
//                var tenant = await _residentRepository.FirstOrDefaultAsync(r => r.Id == lease.Tenant.Id);
//                if (tenant == null)
//                {
//                    // Handle the case where tenant is not found
//                    throw new Exception("Tenant not found");
//                }

//                // Get the full file path
//                var filePath = Path.Combine(Directory.GetCurrentDirectory(), BASE_FILE_PATH, lease.DocumentName);

//                // Read the file content as bytes
//                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

//                // Convert file bytes to Base64 string
//                string base64File = Convert.ToBase64String(fileBytes);

//                // Create a ReturnLeaseDto object and fill it with data
//                var returnLeaseDto = new ReturnLeaseDto
//                {
//                    Id = lease.Id,
//                    OwnerId = lease.OwnerID.Id,
//                    FileName = lease.DocumentName,
//                    Base64 = base64File, // Assign the Base64 string
//                    RentAmount = lease.RentAmount,
//                    DepositPaid = lease.DepositPaid,
//                    TenantId = tenant.Id,
//                    LeaseName = "", // You can fill this with the name of the lease if needed
//                    ResidentName = tenant.Name,
//                    ResidentSurname = tenant.Surname,
//                    EmailAddress = tenant.EmailAddress
//                };

//                returnLeaseDtos.Add(returnLeaseDto);
//            }

//            return returnLeaseDtos;
//        }


//        public async Task<LeaseDto> GetLeaseAsync(Guid id)
//        {
//            var lease = await _leaseRepository.GetAsync(id);
//            return _mapper.Map<LeaseDto>(lease);
//        }

//        public async Task UpdateLeaseAsync(Guid id, LeaseDto input)
//        {
//            var existingLease = await _leaseRepository.GetAsync(id);
//            if (existingLease == null)
//            {
//                throw new Exception("Lease not found");
//            }

//            _mapper.Map(input, existingLease);
//            existingLease.DocumentName = await SaveLeaseFile(input.File);
//            await _leaseRepository.UpdateAsync(existingLease);
//        }

//        public async Task DeleteLeaseAsync(Guid id)
//        {
//            var lease = await _leaseRepository.GetAsync(id);
//            if (lease != null)
//            {
//                var filePath = $"{BASE_FILE_PATH}/{lease.DocumentName}";
//                if (File.Exists(filePath))
//                {
//                    File.Delete(filePath);
//                }
//                await _leaseRepository.DeleteAsync(lease);
//            }
//        }

//        private async Task<string> SaveLeaseFile(IFormFile file)
//        {
//            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
//            var filePath = $"{BASE_FILE_PATH}/{fileName}";
//            using (var fileStream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(fileStream);
//            }
//            return fileName;
//        }
//    }
//}
