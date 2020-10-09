using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Nebula.CI.Services.Plugin
{
    public interface IPluginAppService : IApplicationService
    {
        Task<List<PluginBaseDto>> GetListAsync();

        Task<PluginDto> GetAsync(string id);
    }
}
