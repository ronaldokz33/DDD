using Api.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test.Mocks.Users
{
    public class UsersTests
    {
        public static string Name { get; set; }
        public static string Email { get; set; }
        public static string ChangedName { get; set; }
        public static string ChangedEmail { get; set; }
        public static Guid Id { get; set; }

        public List<UserDto> userList = new List<UserDto>();
        public UserDto user;
        public UserCreateDto userCreateDto;
        public UserUpdateDto userUpdateDto;
        public UserCreateResultDto userCreateResultDto;
        public UserUpdateResultDto userUpdateResultDto;

        public UsersTests()
        {
            Id = Guid.NewGuid();
            Name = Faker.Name.FullName();
            Email = Faker.Internet.Email();
            ChangedName = Faker.Name.FullName();
            ChangedEmail = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                userList.Add(new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                });
            }

            user = new UserDto()
            {
                Id = Id,
                Name = Name,
                Email = Email
            };

            userCreateDto = new UserCreateDto()
            {
                Name = Name,
                Email = Email
            };

            userCreateResultDto = new UserCreateResultDto()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                CreatedAt = DateTime.UtcNow
            };

            userUpdateDto = new UserUpdateDto()
            {
                Id = Id,
                Name = Name,
                Email = Email
            };

            userUpdateResultDto = new UserUpdateResultDto()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
