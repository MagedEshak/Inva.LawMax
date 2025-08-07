using Inva.LawCases.IRepositories;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.VirtualFileSystem;

namespace Inva.LawCases.SeedingData
{
    public class LawyerDataSeeder : ITransientDependency
    {
        protected ILawyerRepository _lawyerRepository { get; }
        protected IGuidGenerator _GuidGenerator { get; }
        protected IDataFilter<ISoftDelete> _SoftDeleteFilter { get; }
        protected ICurrentTenant _CurrentTenant { get; }
        //protected AppSettingsOptions Options { get; }
        protected IVirtualFileProvider _VirtualFileProvider { get; }

        public LawyerDataSeeder
            (
            ILawyerRepository lawyerRepository, 
            IGuidGenerator guidGenerator,
            IDataFilter dataFilter,
            ICurrentTenant currentTenant,
            IVirtualFileProvider fileProvider,
            IDataFilter<ISoftDelete> softDeleteFilter
            )
        {
            _lawyerRepository = lawyerRepository;
            _CurrentTenant = currentTenant;
            _VirtualFileProvider = fileProvider;
            _SoftDeleteFilter = softDeleteFilter;
            _GuidGenerator = guidGenerator;
           
        }

        public virtual async Task SeedAsync()
        {
            using (_SoftDeleteFilter.Disable())
            {
              //  var file = _VirtualFileProvider.GetFileInfo();
            }
        }
    }
}
