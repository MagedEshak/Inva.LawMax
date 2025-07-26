
using Inva.LawCases.Enums;
using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases._ٍSeedingData
{
    public class CaseSeeding : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Case, Guid> _caseRepo;

        public CaseSeeding(IRepository<Case, Guid> caseRepo)
        {
            _caseRepo = caseRepo;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            if (await _caseRepo.GetCountAsync() > 0)
                return;

            var cases = new List<Case>
            {
                new Case
                {
                  Title = "Financial Compensation Case",
                  Description = "A case regarding monetary compensation for damages.",
                  Status = Status.Open, // غيّر حسب enum عندك
                }
            };

            await _caseRepo.InsertManyAsync(cases, autoSave: true);
        }
    }
}
