using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Test.Mocks.Users;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test
{
    public class LoginTests : UsersTests
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

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

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(returnObject);

            _service = _serviceMock.Object;

            var _result = await _service.FindByLogin(loginDto);
            Assert.NotNull(_result);
        }
    }
}
