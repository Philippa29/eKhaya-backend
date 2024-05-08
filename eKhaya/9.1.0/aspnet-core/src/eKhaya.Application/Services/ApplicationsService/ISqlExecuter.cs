//using Abp.Dependency;
//using Abp.EntityFrameworkCore;
//using eKhaya.Services.Dtos;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace eKhaya.Services.ApplicationsService
//{
//    public interface ISqlExecuter
//    {
//        Task<List<GetApplicationsDto>> ExecuteQueryAsync(Guid agentId);
//    }

//    public class SqlExecuter : ISqlExecuter
//    {
//        private readonly DbContext _dbContext;

//        public SqlExecuter(DbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<List<GetApplicationsDto>> ExecuteQueryAsync(Guid agentId)
//        {
//            var sqlQuery = @"
//            SELECT ap.Id, ap.ApplicantId, p.PropertyName, p.Id
//            FROM Applications ap
//            INNER JOIN Properties p ON ap.PropertyId = p.Id
//            INNER JOIN propertyAgents pa ON pa.PropertyId = p.Id
//            WHERE pa.AgentId = @AgentId
//        ";

//            var applications = await _dbContext.ApplicationDto
//                .FromSqlRaw(sqlQuery, new SqlParameter("@AgentId", agentId))
//                .ToListAsync();

//            return applications;
//        }

//    }

//}
