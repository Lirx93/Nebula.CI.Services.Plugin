using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace Nebula.CI.Services.Plugin
{
    class PluginApplicationAutoMapperProfile : Profile
    {
        public PluginApplicationAutoMapperProfile()
        {
            CreateMap<PluginParam, PluginParamDto>();
            CreateMap<PluginResource, PluginResourceDto>();
            CreateMap<Plugin, PluginDto>()
                .ForPath(d => d.Resources.Inputs, map => map.MapFrom(s => s.InputResources))
                .ForPath(d => d.Resources.Outputs, map => map.MapFrom(s => s.OutputResources));

            CreateMap<Plugin, PluginBaseDto>();

            CreateMap<Resource, ResourceDto>();
            CreateMap<List<Resource>, ResourceTypeDto>()
                .ForMember(d => d.Name, map => map.MapFrom(s => s.FirstOrDefault().Type))
                .ForMember(d => d.Resources, map => map.MapFrom(s => s));


        }
    }

}
