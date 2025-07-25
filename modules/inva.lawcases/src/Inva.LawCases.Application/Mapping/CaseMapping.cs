using AutoMapper;
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Mapping
{
    public class CaseMapping:Profile
    {
        public CaseMapping()
        {
            CreateMap<Case,CaseDto>();
            CreateMap<CreateUpdateCaseDto,Case>();
        }
    }
}
