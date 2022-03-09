using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test
{
    public abstract class BaseTestService
    {
        public IMapper Mapper;
        public BaseTestService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }
    }
}
