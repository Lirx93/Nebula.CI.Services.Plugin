using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Nebula.CI.Services.Plugin
{
    public interface IResourceRepository : ITransientDependency
    {
        Task<List<Resource>> GetListAsync();
    }
}
