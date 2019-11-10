using System.Collections.Generic;
using Users.Domain.Models;
using Users.Domain.Services.Communication;
using Users.Resources;

namespace Services.Tests.Utils.Generators
{
    public class UserGenerator
    {
        public static List<User> GetTestUsers()
        {
            return new List<User>
            {
                new User()
                {
                    Id = 1000,
                    Email = "firstUser@lala.com",
                    FirstName = "First",
                    LastName = "User"
                },
                new User()
                {
                    Id = 1001,
                    Email = "secondUser@lala.com",
                    FirstName = "Second",
                    LastName = "User"
                },
                new User()
                {
                    Id = 1002,
                    Email = "thirdUser@lala.com",
                    FirstName = "Third",
                    LastName = "User"
                }
            };
        }

        public static User GetTestUser()
        {
            return new User()
            {
                Id = 1000,
                Email = "firstUser@lala.com",
                FirstName = "First",
                LastName = "User"
            };
        }

        public static UserResponse GetTestUserResponse()
        {
            return new UserResponse(new User()
            {
                Id = 1000,
                Email = "firstUser@lala.com",
                FirstName = "First",
                LastName = "User"
            });
        }

        public static SaveUserResource GetTestSaveUserResource()
        {
            return new SaveUserResource()
            {
                Email = "thirdUser@lala.com",
                FirstName = "Third",
                LastName = "User"
            };
        }

        public static UpdateUserResource GetTestUpdateUserResource()
        {
            return new UpdateUserResource()
            {
                Email = "thirdUser@lala.com",
                FirstName = "Third",
                LastName = "User"
            };
        }
    }
}