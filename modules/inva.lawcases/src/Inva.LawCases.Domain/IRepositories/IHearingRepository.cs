using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.IRepositories
{
    public interface IHearingRepository : IRepository<Hearing, Guid>
    {
        Task<Hearing> GetHearingWithCase(Guid id);
        Task<Hearing> GetHearingWithCaseID(Guid id);
    }
}
