using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    [TestCaseOrderer("Api.Integration.Test", "UserTest")]
    public class UserTest : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }
        private Guid _id { get; set; }

        [Fact]
        public async Task IntegrationTest()
        {
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserCreateDto() { 
                Name = _name,
                Email = _email
            };

            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserCreateResultDto>(result);

            _id = user.Id;

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, user.Name);
            Assert.Equal(_email, user.Email);
            Assert.True(default(Guid) != user.Id);

            await GetToken(_email);

            //GET ALL
            response = await GetAsync($"{hostApi}users", client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == _id).Count() == 1);


            //GET
            response = await GetAsync($"{hostApi}users/{_id}", client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDto>(result);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Name, _name);
            Assert.Equal(registroSelecionado.Email, _email);

            //UPDATE
            var updateUserDto = new UserUpdateDto()
            {
                Id = _id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            response = await PutJsonAsync(updateUserDto, $"{hostApi}users", client);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<UserUpdateResultDto>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(_name, registroAtualizado.Name);
            Assert.NotEqual(_email, registroAtualizado.Email);


            //DELETE
            response = await DeleteAsync($"{hostApi}users/{_id}", client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await GetAsync($"{hostApi}users/{_id}", client);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
