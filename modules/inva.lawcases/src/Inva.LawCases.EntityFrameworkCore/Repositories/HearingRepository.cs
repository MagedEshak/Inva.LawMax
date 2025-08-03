using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.EntityFrameworkCore;
using Inva.LawCases.IRepositories;
using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawCases.Repositories
{
    public class HearingRepository : EfCoreRepository<LawCasesDbContext, Hearing, Guid>, IHearingRepository
    {
        public HearingRepository(IDbContextProvider<LawCasesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<HearingDto>> GetHearingsByLawyer(Guid lawyerId)
        {
            var db = await GetDbContextAsync();

            return await db.Hearings
                .Include(h => h.Case)
                .Where(h => h.Case.LawyerId == lawyerId)
                .Select(h => new HearingDto
                {
                    Id = h.Id,
                    Date = h.Date,
                    Location = h.Location,
                    Decision = h.Decision,
                    CaseTitle = h.Case.CaseTitle,
                    CaseStatus = h.Case.Status,
                    CaseDescription = h.Case.Description,
                    CaseFinalVerdict = h.Case.FinalVerdict,
                    CaseLitigationDegree = h.Case.LitigationDegree,
                    ConcurrencyStamp = h.Case.ConcurrencyStamp
                })
                .ToListAsync();
        }


        public async Task<Hearing> GetHearingWithCase(Guid id)
        {
            var db = await GetDbContextAsync();
            return await db.Hearings.Include(c => c.Case).ThenInclude(l => l.Lawyer).FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<Hearing> GetHearingWithCaseID(Guid id)
        {
            var db = await GetDbContextAsync();
            return await db.Hearings.Include(c => c.Case).ThenInclude(l => l.Lawyer).FirstOrDefaultAsync(h => h.CaseId == id);
        }


    }
}
