using Api.CrossCutting.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Integration.Test
{
    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());

            });

            return config.CreateMapper();
        }

        public void Dispose()
        {
        }
    }
}
