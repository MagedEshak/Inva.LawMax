using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Inva.LawCases.Samples;

[Area(LawCasesRemoteServiceConsts.ModuleName)]
[RemoteService(Name = LawCasesRemoteServiceConsts.RemoteServiceName)]
[Route("api/law-cases/example")]
public class ExampleController : LawCasesController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public ExampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
