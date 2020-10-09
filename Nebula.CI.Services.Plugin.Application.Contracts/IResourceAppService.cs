using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Nebula.CI.Services.Plugin
{
    public interface IResourceAppService : IApplicationService
    {
        Task<List<ResourceTypeDto>> GetListAsync();
    }
}
