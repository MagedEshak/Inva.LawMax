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
    /// <summary>
    /// Provides application services for retrieving dashboard-related data,  such as case status counts and case counts
    /// by month.
    /// </summary>
    /// <remarks>This service is designed to aggregate and return data for use in dashboard visualizations. It
    /// includes methods for retrieving case statistics grouped by status and by month.</remarks>
    public class DashboardAppService : BaseApplicationService, IDashboardAppService
    {
        private readonly IRepository<Case, Guid> _caseRepo;

        public DashboardAppService(IRepository<Case, Guid> caseRepo)
        {
            _caseRepo = caseRepo;
        }
        /// <summary>
        /// Retrieves a list of case statuses along with their respective counts.
        /// </summary>
        /// <remarks>This method groups cases by their status, calculates the count for each status, and
        /// returns a collection of all possible statuses with their associated counts. If a status has no associated
        /// cases, its count will be zero.</remarks>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="CaseStatusNumberDto"/> objects, where each object represents a
        /// case status and its count.</returns> 
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
        /// <summary>
        /// Retrieves a list of cases grouped by month, including all months of the year.
        /// </summary>
        /// <remarks>This method returns a collection of <see cref="CaseByMonthDto"/> objects, where each
        /// object represents a month of the year and the corresponding count of cases created during that month. If no
        /// cases exist for a particular month, the count will be zero.</remarks>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="CaseByMonthDto"/> objects, where each object contains the
        /// month (as an integer from 1 to 12) and the count of cases for that month.</returns>
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
