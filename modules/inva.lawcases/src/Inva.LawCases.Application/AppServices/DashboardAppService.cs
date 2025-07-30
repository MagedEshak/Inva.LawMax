using Inva.LawCases.Base;
using Inva.LawCases.DTOs.Dashboard;
using Inva.LawCases.Enums;
using Inva.LawCases.Interfaces;
using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.AppServices
{
    public class DashboardAppService : BaseApplicationService, IDashboardAppService
    {
        private readonly IRepository<Case, Guid> _caseRepo;

        public DashboardAppService(IRepository<Case, Guid> caseRepo)
        {
            _caseRepo = caseRepo;
        }

        public async Task<IEnumerable<CaseStatusNumberDto>> GetListOfCaseStatusNumber()
        {
            var query = await _caseRepo.GetQueryableAsync();

            var dbStasus = await query.GroupBy(s => s.Status).Select(g => new
            {
                Status = g.Key,
                Couunt = g.Count()
            }).ToListAsync();


            var allStatus = Enum.GetValues(typeof(Status)).Cast<Status>();
            var result = allStatus.Select(s => new CaseStatusNumberDto
            {
                Status = s,
                Couunt = dbStasus.FirstOrDefault(res => res.Status == s)?.Couunt ?? 0
            });
            return result;
        }

        public async Task<IEnumerable<CaseByMonthDto>> GetListOfCaseByMonth()
        {
            var query = await _caseRepo.GetQueryableAsync();

            var dbStasus = await query.GroupBy(s => s.CreationTime.Month).Select(g => new
            {
                Month = g.Key,
                Couunt = g.Count()
            }).ToListAsync();


            var allMonths = Enumerable.Range(1, 12);

            var result = allMonths.Select(month => new CaseByMonthDto
            {
                Month = month,
                Couunt = dbStasus.FirstOrDefault(res => res.Month == month)?.Couunt ?? 0
            });
            return result;
        }

    }
}
