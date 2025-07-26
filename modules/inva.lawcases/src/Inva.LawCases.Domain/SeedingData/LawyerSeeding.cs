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
                    Speciality = "Defense Lawyer",
                    Phone = "01098765432",
                    Address = "Tanta",
                    Email = "Mona@gmail.com"

                },
                new()
                {
                    Name = "Samuel Ashraf",
                    Speciality = "Defense Lawyer",
                    Phone = "01098765555",
                    Address = "Minya",
                    Email = "samuel@gmail.com"
                }
                // Add more lawyers if needed
            };

            await _lawyerRepo.InsertManyAsync(lawyers, autoSave: true);
        }
    }
}
