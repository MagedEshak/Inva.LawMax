using Inva.LawCases.EntityFrameworkCore;
using Inva.LawCases.IRepositories;
using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawCases.Repositories
{
    public class LawyerRepositpry : EfCoreRepository<LawCasesDbContext, Lawyer, Guid>, ILawyerRepository
    {
        public LawyerRepositpry(IDbContextProvider<LawCasesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<Lawyer> GetLawyerWithCase(Guid id)
        {
            var db = await GetDbContextAsync();
            return db.Lawyers.Include(x => x.Cases).FirstOrDefault(l => l.Id == id);

        }
    }
}
