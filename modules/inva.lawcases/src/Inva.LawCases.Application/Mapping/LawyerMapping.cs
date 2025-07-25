using AutoMapper;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Mapping
{
    public class LawyerMapping :Profile
    {
        public LawyerMapping() {
            CreateMap<Lawyer, LawyerDto>();
            CreateMap<CreateUpdateLawyerDto, Lawyer>()
                .ForMember(dest => dest.Case, opt => opt.Ignore())
               .ForMember(dest => dest.TenantId, opt => opt.Ignore())
               .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
    .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
    .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
    .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
    .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore()); ;
        }
    }
}
