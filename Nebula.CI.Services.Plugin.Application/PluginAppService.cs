using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Nebula.CI.Services.Plugin
{
    public class PluginAppService : ApplicationService, IPluginAppService
    {
        private readonly IPluginRepository _pluginRepository;

        public PluginAppService(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository;
        }

        /// <summary>
        /// 根据id获取插件详细信息
        /// </summary>
        public async Task<PluginDto> GetAsync(string id)
        {
            var plugin = await _pluginRepository.GetAsync(id);

            var pluginDto = ObjectMapper.Map<Plugin, PluginDto>(plugin);

            return pluginDto;
        }

        /// <summary>
        /// 获取插件列表
        /// </summary>
        public async Task<List<PluginBaseDto>> GetListAsync()
        {
            var plugins = await _pluginRepository.GetListAsync();

            var pluginDtos = ObjectMapper.Map<List<Plugin>, List<PluginBaseDto>>(plugins);

            return pluginDtos;
        }
    }
}
