using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.IRepositories
{
    public interface ILawyerRepository : IRepository<Lawyer, Guid>
    {
        Task<Lawyer> GetLawyerWithCase(Guid id);
        Task<bool> CheckEmailAsync(string email);
        Task<bool> CheckPhoneAsync(string phone);
    }
}
