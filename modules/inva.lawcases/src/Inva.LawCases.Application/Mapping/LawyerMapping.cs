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
            CreateMap<CreateUpdateLawyerDto, Lawyer>();
        }
    }
}
