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
    public class CaseMapping : Profile
    {
        public CaseMapping()
        {
            CreateMap<Case, CaseDto>()
              .ForMember(dest => dest.HearingDtos, opt => opt.MapFrom(src => src.Hearings));

            CreateMap<CreateUpdateCaseDto, Case>()
                .ForMember(dest => dest.Lawyer, opt => opt.Ignore())
                .ForMember(dest => dest.Hearings, opt => opt.Ignore())
                .ForMember(dest => dest.TenantId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore());
        }
    }
}
