using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper : BaseTestService
    {
        [Fact(DisplayName = "Is possible mapping models")]
        public void IsPossibleMappingModels()
        {
            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var modelToEntity = Mapper.Map<UserEntity>(model);

            Assert.Equal(modelToEntity.Id, model.Id);
            Assert.Equal(modelToEntity.Name, model.Name);
            Assert.Equal(modelToEntity.Email, model.Email);
            Assert.Equal(modelToEntity.CreatedAt, model.CreateAt);
            Assert.Equal(modelToEntity.UpdatedAt, model.UpdateAt);

            var entityToDto = Mapper.Map<UserDto>(modelToEntity);

            Assert.Equal(entityToDto.Id, modelToEntity.Id);
            Assert.Equal(entityToDto.Name, modelToEntity.Name);
            Assert.Equal(entityToDto.Email, modelToEntity.Email);
            Assert.Equal(entityToDto.CreatedAt, modelToEntity.CreatedAt);

            var listEntity = new List<UserEntity>();

            for (int i = 0; i < 10; i++)
            {
                listEntity.Add(new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            var listDto = Mapper.Map<IEnumerable<UserDto>>(listEntity);
            Assert.True(listDto.Count() == listEntity.Count());
        }
    }
}
