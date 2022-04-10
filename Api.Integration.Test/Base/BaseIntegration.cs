using Api.Data.Context;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Projeto1;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Integration.Test
{
    public class BaseIntegration : IDisposable
    {
        public MyContext myContext { get; set; }
        public HttpClient client { get; set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }

        public BaseIntegration()
        {
            hostApi = "http://localhost:39584/api/";
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            myContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;
            myContext.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            client = server.CreateClient();
        }

        public async Task<LoginResponseDto> GetToken(string email)
        {
            var loginDto = new LoginDto()
            {
                Email = email
            };

            var resultLogin = await PostJsonAsync(loginDto, hostApi + "Login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.accessToken);

            return loginResponse;
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object data, string url, HttpClient client)
        {
            return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"));
        }

        public static async Task<HttpResponseMessage> PutJsonAsync(object data, string url, HttpClient client)
        {
            return await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"));
        }

        public static async Task<HttpResponseMessage> GetAsync(string url, HttpClient client)
        {
            return await client.GetAsync(url);
        }
        public static async Task<HttpResponseMessage> DeleteAsync(string url, HttpClient client)
        {
            return await client.DeleteAsync(url);
        }

        
        public void Dispose()
        {
            myContext.Dispose();
            client.Dispose();
        }
    }
}
