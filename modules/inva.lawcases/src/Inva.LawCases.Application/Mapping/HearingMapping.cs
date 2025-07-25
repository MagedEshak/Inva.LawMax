using AutoMapper;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Mapping
{
    public class HearingMapping : Profile
    {
        public HearingMapping()
        {
            CreateMap<Hearing, HearingDto>();
            CreateMap<CreateUpdateHearingDto, Hearing>()
                .ForMember(dest => dest.Case, opt => opt.Ignore())
               .ForMember(dest => dest.TenantId, opt => opt.Ignore());
        }
    }
}
