using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Nebula.CI.Services.Plugin
{
    public class ResourceAppService : ApplicationService, IResourceAppService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceAppService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        /// <summary>
        /// 获取资源列表
        /// </summary>
        public async Task<List<ResourceTypeDto>> GetListAsync()
        {
            var resources = await _resourceRepository.GetListAsync();
            var dic = new Dictionary<string, List<Resource>>();
            foreach (var r in resources) 
            {
                if (dic.ContainsKey(r.Type))
                {
                    dic[r.Type].Add(r);
                }
                else 
                {
                    var rs = new List<Resource>();
                    rs.Add(r);
                    dic.Add(r.Type, rs);
                }
            }
            var resourceDtos = new List<ResourceTypeDto>();
            foreach (var list in dic.Values.ToList()) 
            {
                var resourceDto = ObjectMapper.Map<List<Resource>, ResourceTypeDto>(list);
                resourceDtos.Add(resourceDto);
            }
            
            return resourceDtos;
        }
    }
}
