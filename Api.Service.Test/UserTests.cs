using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Test.Mocks.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test
{
    public class UserTests : UsersTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;
        
        [Fact(DisplayName = "Is possible execution get method")]
        public async Task Get()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(Id)).ReturnsAsync(user);
            _service = _serviceMock.Object;

            var _register = await _service.Get(Id);


            Assert.NotNull(_register);
            Assert.Equal(_register.Name, user.Name);
            Assert.Equal(_register.Email, user.Email);
            Assert.True(_register.Id == user.Id);
        }

        [Fact(DisplayName = "Is possible execution getall method")]
        public async Task GetAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(userList);
            _service = _serviceMock.Object;

            var _register = await _service.GetAll();


            Assert.NotNull(_register);
            Assert.True(_register.Count() == 10);
        }

        [Fact(DisplayName = "Is possible execution post method")]
        public async Task Post()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userCreateDto)).ReturnsAsync(userCreateResultDto);
            _service = _serviceMock.Object;

            var _register = await _service.Post(userCreateDto);


            Assert.NotNull(_register);
            Assert.Equal(_register.Name, userCreateDto.Name);
            Assert.Equal(_register.Email, userCreateDto.Email);
            Assert.True(_register.Id != Guid.Empty);
        }

        [Fact(DisplayName = "Is possible execution put method")]
        public async Task Put()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(userUpdateDto)).ReturnsAsync(userUpdateResultDto);
            _service = _serviceMock.Object;

            var _register = await _service.Put(userUpdateDto);


            Assert.NotNull(_register);
            Assert.Equal(_register.Name, userUpdateDto.Name);
            Assert.Equal(_register.Email, userUpdateDto.Email);
            Assert.True(_register.Id == userUpdateDto.Id);
        }

        [Fact(DisplayName = "Is possible execution delete method")]
        public async Task Delete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(Id)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var _register = await _service.Delete(Id);

            Assert.True(_register);
        }

        [Fact(DisplayName = "Is possible find by login")]
        public async Task FindByLogin()
        {
            var email = Faker.Internet.Email();
            var returnObject = new
            {
                authenticated = true,
                created = DateTime.Now,
                expiration = DateTime.Now.AddHours(8),
                accessToken = Guid.NewGuid(),
                userName = user.Email,
                message = "Usuário logado com sucesso"
            };

            var loginDto = new LoginDto()
            {
                Email = email
            };

            var _loginServiceMock = new Mock<ILoginService>();
            _loginServiceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(returnObject);

            var _loginService = _loginServiceMock.Object;

            var _user = await _loginService.FindByLogin(loginDto);

            Assert.NotNull(_user);
        }
    }
}
