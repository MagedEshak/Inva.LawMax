using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.ResponseModels
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
        public object? Data { get; set; }

        public static ServiceResult Ok(string successMessage) =>
            new ServiceResult { Success = true, SuccessMessage = successMessage };

        public static ServiceResult OkWithData(object data) =>
            new ServiceResult { Success = true, Data = data };

        public static ServiceResult Fail(string error) =>
            new ServiceResult { Success = false, ErrorMessage = error };


        public static ServiceResult FailWithData(object data) => new ServiceResult { Success = false, Data = data };

    }
}
