using System;
using System.Collections.Generic;
using System.Text;

namespace Nebula.CI.Services.Plugin
{
    public class ResourceTypeDto
    {
        public string Name { get; set; }
        public List<ResourceDto> Resources { get; set; }
    }
}
