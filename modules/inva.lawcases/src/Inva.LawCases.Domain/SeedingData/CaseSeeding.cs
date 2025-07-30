
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

            var case1 = new Case
            {
                Title = "Case of Theft",
                Description = "A suspected theft in the city center.",
                Status = Status.Open,
                TenantId = context.TenantId,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var case2 = new Case
            {
                Title = "Land Dispute",
                Description = "Dispute over land ownership in village.",
                Status = Status.New,
                TenantId = context.TenantId,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            await _caseRepo.InsertManyAsync(new[] { case1, case2 }, autoSave: true);
        }
    }
}
