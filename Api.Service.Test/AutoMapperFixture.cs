using Api.CrossCutting.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test
{
    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });

            return config.CreateMapper();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
