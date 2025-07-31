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

        public async Task<Hearing> GetHearingWithCase(Guid id)
        {
            var db = await GetDbContextAsync();
            return db.Hearings.Include(c => c.Case).FirstOrDefault(h => h.Id == id);
        }
        public async Task<Hearing> GetHearingWithCaseID(Guid id)
        {
            var db = await GetDbContextAsync();
            return db.Hearings.Include(c => c.Case).FirstOrDefault(h => h.CaseId == id);
        }
    }
}
