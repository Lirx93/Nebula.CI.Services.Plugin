using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Nebula.CI.Services.Plugin
{
    public class Resource : AggregateRoot
    {
       
        public string Uid { get; protected set; }
        public string Name { get; protected set; }
        public string AnnoName { get; protected set; }
        public string Type { get; protected set; }

        protected Resource()
        {
        }

        public override object[] GetKeys()
        {
            return new object[]{ Uid};
        }
    }
}
