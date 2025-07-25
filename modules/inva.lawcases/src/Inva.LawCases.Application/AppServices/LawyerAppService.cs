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


namespace Inva.LawCases.AppServices
{
    public class LawyerAppService : ApplicationService, ILawyerAppService
    {
        private readonly IRepository<Lawyer, Guid> _lawyerRepo;

        public LawyerAppService(IRepository<Lawyer, Guid> lawyerRepo)
        {
            _lawyerRepo = lawyerRepo;
        }

        public async Task<LawyerDto> CreateAsync(CreateUpdateLawyerDto input)
        {
            var lawyerEntity = ObjectMapper.Map<CreateUpdateLawyerDto, Lawyer>(input);

            var insertedLawyer = await _lawyerRepo.InsertAsync(lawyerEntity, autoSave: true);

            return ObjectMapper.Map<Lawyer, LawyerDto>(insertedLawyer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lawyerRepo.DeleteAsync(id);
        }

        public async Task<LawyerDto> GetAsync(Guid id)
        {
            var lawyer = await _lawyerRepo.GetAsync(id);
            return ObjectMapper.Map<Lawyer, LawyerDto>(lawyer);
        }

        public async Task<PagedResultDto<LawyerDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _lawyerRepo.GetQueryableAsync();
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var lawyers = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<LawyerDto>(
                totalCount,
                ObjectMapper.Map<List<Lawyer>, List<LawyerDto>>(lawyers)
            );
        }

        public async Task<LawyerDto> UpdateAsync(Guid id, CreateUpdateLawyerDto input)
        {
            var lawyer = await _lawyerRepo.GetAsync(id);
            ObjectMapper.Map(input, lawyer);
            await _lawyerRepo.UpdateAsync(lawyer);
            return ObjectMapper.Map<Lawyer, LawyerDto>(lawyer);
        }



        //public async Task<LawyerDto> CreateLawyerAsync(CreateUpdateLawyerDto lawyerDto)
        //{
        //    try
        //    {
        //        var lawyerEntity = ObjectMapper.Map<CreateUpdateLawyerDto, Lawyer>(lawyerDto);

        //        var insertedLawyer = await _lawyerRepo.InsertAsync(lawyerEntity, autoSave: true);

        //        return ObjectMapper.Map<Lawyer, LawyerDto>(insertedLawyer);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex, "Error in CreateLawyerAsync");
        //        throw;
        //    }
        //}


        //public async Task<bool> DeleteLawyerAsync(Guid lawyerGuid)
        //{
        //    var lawyerEntityId = await _lawyerRepo.GetAsync(lawyerGuid);

        //    if (lawyerEntityId == null)
        //    {
        //        return false;
        //    }

        //    await _lawyerRepo.DeleteAsync(lawyerEntityId);

        //    return true;
        //}

        //public async Task<IEnumerable<LawyerDto>> GetAllLawyerAsync()
        //{
        //    var lawyersQuery = await _lawyerRepo.WithDetailsAsync(l => l.Case);

        //    var lawyersList = lawyersQuery.ToList();

        //    return ObjectMapper.Map<List<Lawyer>, List<LawyerDto>>(lawyersList);
        //}

        //public Task<LawyerDto> GetAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<LawyerDto> GetLawyerByIdAsync(Guid lawyerGuid)
        //{
        //    var lawyerEntity = await _lawyerRepo.WithDetailsAsync(l => l.Case);

        //    var entity = await lawyerEntity.Where(l => l.Id == lawyerGuid).FirstOrDefaultAsync();

        //    if (entity == null)
        //    {
        //        throw new EntityNotFoundException("error");
        //    }

        //    return ObjectMapper.Map<Lawyer, LawyerDto>(entity);
        //}

        //public Task<PagedResultDto<LawyerDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<LawyerDto> UpdateAsync(Guid id, CreateUpdateLawyerDto input)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<LawyerDto> UpdateLawyerAsync(Guid id, CreateUpdateLawyerDto lawyerDto)
        //{
        //    var lawyerEntityId = await _lawyerRepo.GetAsync(id);

        //    if (lawyerEntityId == null)
        //    {
        //        throw new EntityNotFoundException("This Lawyer Not Found");
        //    }

        //    ObjectMapper.Map(lawyerDto, lawyerEntityId);

        //    await _lawyerRepo.UpdateAsync(lawyerEntityId, autoSave: true);

        //    return ObjectMapper.Map<Lawyer, LawyerDto>(lawyerEntityId);

        //}


    }
}
