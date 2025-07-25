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
    public class LawyerSeeding : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Lawyer, Guid> _lawyerRepo;
        private readonly IRepository<Case, Guid> _caseRepo;

        public LawyerSeeding(IRepository<Lawyer, Guid> lawyerRepo, IRepository<Case, Guid> caseRepo)
        {
            _lawyerRepo = lawyerRepo;
            _caseRepo = caseRepo;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            var existingCase = await _caseRepo.FirstOrDefaultAsync();
            if (existingCase == null)
            {
                return;
            }

            // Check if already seeded
            if (await _lawyerRepo.GetCountAsync() > 0)
            {
                return;
            }

            var lawyers = new List<Lawyer>
            {
                new()
                {
                    Name = "Mona Abdallah",
                    Position = "Defense Lawyer",
                    Mobile = "01098765432",
                    Address = "Tanta",
                    CaseId = existingCase.Id
                },
                new()
                {
                    Name = "Mahmoud Shawky",
                    Position = "Lead Attorney",
                    Mobile = "01012345678",
                    Address = "Alexandria",
                    CaseId = existingCase.Id
                }
                // Add more lawyers if needed
            };

            await _lawyerRepo.InsertManyAsync(lawyers, autoSave: true);
        }
    }
}
