using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.LawyerRepo.IlawyerReepository
{
    public interface ILawyerRepository : IRepository<Lawyer, Guid>
    {
        Task<Lawyer> GetLawyerWithCase(Guid id);
    }
}
