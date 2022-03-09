using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserCreateDto, UserEntity>()
                .ReverseMap();

            CreateMap<UserCreateResultDto, UserEntity>()
                .ReverseMap();

            CreateMap<UserUpdateResultDto, UserEntity>()
                .ReverseMap();
        }
    }
}
