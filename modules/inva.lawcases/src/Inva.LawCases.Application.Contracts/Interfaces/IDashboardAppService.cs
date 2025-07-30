using Inva.LawCases.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Interfaces
{
    public interface IDashboardAppService
    {
        Task<IEnumerable<CaseStatusNumberDto>> GetListOfCaseStatusNumber();
        Task<IEnumerable<CaseByMonthDto>> GetListOfCaseByMonth();
    }
}
