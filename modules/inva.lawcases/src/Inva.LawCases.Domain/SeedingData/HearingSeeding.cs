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
    public class HearingSeeding : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Hearing, Guid> _hearingRepo;
        private readonly IRepository<Case, Guid> _caseRepo;

        public HearingSeeding(IRepository<Hearing, Guid> hearingRepo, IRepository<Case, Guid> caseRepo)
        {
            _hearingRepo = hearingRepo;
            _caseRepo = caseRepo;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var existingCase = await _caseRepo.FirstOrDefaultAsync();
            if (existingCase == null)
            {
                return;
            }

            if (await _hearingRepo.GetCountAsync() > 0)
            {
                return;
            }

            var hearings = new List<Hearing>
            {
                new()
                {
                    Date = DateTime.Now.AddDays(-30),
                    Location = "Cairo",
                  
                },
                new()
                {
                    Date = DateTime.Now.AddDays(-15),
                    Location = "Assuit",
                   
                },
                new()
                {
                    Date = DateTime.Now,
                    Location = "Minnya",
                    
                }
            };

            await _hearingRepo.InsertManyAsync(hearings, autoSave: true);
            
        }
    }
}
