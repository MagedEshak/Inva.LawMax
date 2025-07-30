using AutoMapper;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.DTOs.Lawyer;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;

namespace Inva.LawCases.Mapping
{
    public class LawyerMapping : Profile
    {
        public LawyerMapping()
        {
            CreateMap<Lawyer, LawyerDto>()
             .ForMember(dest => dest.CaseId, opt => opt.MapFrom(src => src.CaseId));

            CreateMap<CreateUpdateLawyerDto, Lawyer>()
                .ForMember(dest => dest.Case, opt => opt.Ignore())
               .ForMember(dest => dest.TenantId, opt => opt.Ignore())
               .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
                .IgnoreCreationAuditedObjectProperties().IgnoreAuditedObjectProperties();

        }
    }
}
