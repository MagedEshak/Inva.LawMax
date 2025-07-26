using Inva.LawCases.Validations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.FluentValidation;
using FluentValidation;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesDomainModule),
    typeof(LawCasesApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpFluentValidationModule) 
)]

public class LawCasesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<LawCasesApplicationModule>();


        context.Services.AddValidatorsFromAssemblyContaining<LawyerEntityValidation>();
        context.Services.AddValidatorsFromAssemblyContaining<HearingEntityValidation>();
        context.Services.AddValidatorsFromAssemblyContaining<CaseEntityValidation>();


        context.Services.AddValidatorsFromAssembly(typeof(LawyerEntityValidation).Assembly);
        context.Services.AddValidatorsFromAssembly(typeof(HearingEntityValidation).Assembly);
        context.Services.AddValidatorsFromAssembly(typeof(CaseEntityValidation).Assembly);

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<LawCasesApplicationModule>(validate: true);
        });
    }
}
