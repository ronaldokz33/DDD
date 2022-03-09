using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserCreateDto>()
                .ReverseMap();

            CreateMap<UserModel, UserUpdateDto>()
               .ReverseMap();

            CreateMap<UserModel, UserDto>()
               .ReverseMap();
            
        }
    }
}
