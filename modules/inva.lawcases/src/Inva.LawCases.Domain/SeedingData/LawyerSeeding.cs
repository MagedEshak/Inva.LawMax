using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.SeedingData
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
            if (await _lawyerRepo.GetCountAsync() > 0)
                return;

            // جلب أول قضية موجودة من جدول القضايا (لو عايز تربط محامي بقضية)
            var firstCase = await _caseRepo.FirstOrDefaultAsync();

            var lawyer1 = new Lawyer
            {
                Name = "Samuel Ashraf",
                Email = "samuel@gmail.com",
                Phone = "01098765555",
                Address = "Minya",
                Speciality = "Defense Lawyer",
                CaseId = firstCase?.Id, // ممكن تبقى null
                TenantId = context.TenantId,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var lawyer2 = new Lawyer
            {
                Name = "Mona Abdallah",
                Email = "Mona@gmail.com",
                Phone = "01098765432",
                Address = "Tanta",
                Speciality = "Defense Lawyer",
                CaseId = null, // intentionally left null
                TenantId = context.TenantId,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            await _lawyerRepo.InsertManyAsync(new[] { lawyer1, lawyer2 }, autoSave: true);
        }

    }
}
