using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class UserCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public UserCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        [Fact(DisplayName = "User Insert")]
        [Trait("User CRUD", "User Insert")]
        public async Task UserInsert()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                await Insert(_repository);
            }
        }

        [Fact(DisplayName = "User Update")]
        [Trait("User CRUD", "User Update")]
        public async Task UserUpdate()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                var _entity = await Insert(_repository);

                _entity.Name = Faker.Name.First();

                var _register = await _repository.UpdateAsync(_entity);

                Assert.NotNull(_register);
                Assert.Equal(_register.Name, _entity.Name);
                Assert.Equal(_register.Email, _entity.Email);
                Assert.True(_register.Id != Guid.Empty);
            }
        }

        [Fact(DisplayName = "User Exists")]
        [Trait("User CRUD", "User Exists")]
        public async Task UserExists()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                var _entity = await Insert(_repository);

                var _register = await _repository.ExistsAsync(_entity.Id);

                Assert.True(_register);
            }
        }

        [Fact(DisplayName = "User By Id")]
        [Trait("User CRUD", "User By Id")]
        public async Task UserById()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                var _entity = await Insert(_repository);

                var _register = await _repository.SelectAsync(_entity.Id);

                Assert.NotNull(_register);
                Assert.Equal(_register.Name, _entity.Name);
                Assert.Equal(_register.Email, _entity.Email);
                Assert.True(_register.Id != Guid.Empty);
            }
        }


        [Fact(DisplayName = "User Delete")]
        [Trait("User CRUD", "User Delete")]
        public async Task UserDelete()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                var _entity = await Insert(_repository);

                var _register = await _repository.DeleteAsync(_entity.Id);

                Assert.True(_register);
            }
        }

        [Fact(DisplayName = "User Find by login")]
        [Trait("User CRUD", "User Find by login")]
        public async Task UserFindByLogin()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                var _entity = await Insert(_repository);

                var _register = await _repository.FindByLogin(_entity.Email);

                Assert.NotNull(_register);
                Assert.Equal(_register.Name, _entity.Name);
                Assert.Equal(_register.Email, _entity.Email);
                Assert.True(_register.Id != Guid.Empty);
            }
        }

        private async Task<UserEntity> Insert(UserImplementation _repository)
        {
            UserEntity _entity = new UserEntity()
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName()
            };

            var _register = await _repository.InsertAsync(_entity);

            Assert.NotNull(_register);
            Assert.Equal(_entity.Email, _register.Email);
            Assert.Equal(_entity.Name, _register.Name);
            Assert.False(_register.Id == Guid.Empty);

            return _register;
        }
    }
}
