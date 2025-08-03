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
    public class CaseRepository : EfCoreRepository<LawCasesDbContext, Case, Guid>, ICaseRepository
    {
        public CaseRepository(IDbContextProvider<LawCasesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Case> GetCaseWithHearing(Guid id)
        {
            var db = await GetDbContextAsync();
            return await db.Cases
                .Include(c => c.Hearings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Case> GetCaseWithLawyer(Guid id)
        {
            var db = await GetDbContextAsync();

            return await db.Cases
                .Include(c => c.Lawyer)
                .Include(c => c.Hearings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


    }
}
