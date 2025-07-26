using Inva.LawCases.Base;
using Inva.LawCases.Interfaces;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;


namespace Inva.LawCases.AppServices
{
    public class LawyerAppService : ApplicationService, ILawyerAppService
    {
        private readonly IRepository<Lawyer, Guid> _lawyerRepo;

        public LawyerAppService(IRepository<Lawyer, Guid> lawyerRepo)
        {
            _lawyerRepo = lawyerRepo;
        }



        public async Task<LawyerDto> CreateLawyerAsync(CreateUpdateLawyerDto lawyerDto)
        {
            try
            {
                var lawyerEntity = ObjectMapper.Map<CreateUpdateLawyerDto, Lawyer>(lawyerDto);

                var insertedLawyer = await _lawyerRepo.InsertAsync(lawyerEntity, autoSave: true);

                return ObjectMapper.Map<Lawyer, LawyerDto>(insertedLawyer);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in CreateLawyerAsync");
                throw;
            }
        }


        public async Task<bool> DeleteLawyerAsync(Guid lawyerGuid)
        {
            var lawyerEntityId = await _lawyerRepo.GetAsync(lawyerGuid);

            if (lawyerEntityId == null)
            {
                return false;
            }

            await _lawyerRepo.DeleteAsync(lawyerEntityId);

            return true;
        }

        public async Task<IEnumerable<LawyerDto>> GetAllLawyerAsync()
        {
            var lawyersQuery = await _lawyerRepo.WithDetailsAsync(l => l.Case);

            var lawyersList = lawyersQuery.ToList();

            return ObjectMapper.Map<List<Lawyer>, List<LawyerDto>>(lawyersList);
        }

        public async Task<LawyerDto> GetLawyerByIdAsync(Guid lawyerGuid)
        {
            var lawyerEntity = await _lawyerRepo.WithDetailsAsync(l => l.Case);

            var entity = await lawyerEntity.Where(l => l.Id == lawyerGuid).FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException("error");
            }

            return ObjectMapper.Map<Lawyer, LawyerDto>(entity);
        }

        public async Task<LawyerDto> UpdateLawyerAsync(Guid id, CreateUpdateLawyerDto lawyerDto)
        {
            var lawyerEntity = await _lawyerRepo.GetAsync(id);

            if (lawyerEntity == null)
            {
                throw new EntityNotFoundException("This Lawyer Not Found");
            }

            // تحديث جزئي حسب القيم المرسلة
            if (!string.IsNullOrWhiteSpace(lawyerDto.Name))
                lawyerEntity.Name = lawyerDto.Name;

            if (!string.IsNullOrWhiteSpace(lawyerDto.Email))
                lawyerEntity.Email = lawyerDto.Email;

            if (!string.IsNullOrWhiteSpace(lawyerDto.Phone))
                lawyerEntity.Phone = lawyerDto.Phone;

            if (!string.IsNullOrWhiteSpace(lawyerDto.Address))
                lawyerEntity.Address = lawyerDto.Address;

            if (!string.IsNullOrWhiteSpace(lawyerDto.Speciality))
                lawyerEntity.Speciality = lawyerDto.Speciality;

            // تحديث في الداتابيز
            await _lawyerRepo.UpdateAsync(lawyerEntity, autoSave: true);

            return ObjectMapper.Map<Lawyer, LawyerDto>(lawyerEntity);
        }

    }
}
