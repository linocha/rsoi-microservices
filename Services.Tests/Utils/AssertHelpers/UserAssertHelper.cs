using Users.Domain.Models;
using Users.Domain.Services.Communication;
using Users.Resources;
using Xunit;

namespace Services.Tests.Utils.AssertHelpers
{
    public class UserAssertHelper : AssertHelperBase
    {
        public static void AssertEquals(User user, UserResource userResource)
        {
            Assert.Equal(user.Id, userResource.Id);
            Assert.Equal(user.Email, userResource.Email);
            Assert.Equal(user.FirstName, userResource.FirstName);
            Assert.Equal(user.LastName, userResource.LastName);
        }

        public static void AssertEquals(SaveUserResource saveUserResource, UserResource userResource)
        {
            Assert.Equal(saveUserResource.Email, userResource.Email);
            Assert.Equal(saveUserResource.FirstName, userResource.FirstName);
            Assert.Equal(saveUserResource.LastName, userResource.LastName);
        }
        
        public static void AssertEquals(UpdateUserResource updateUserResource, UserResource userResource)
        {
            Assert.Equal(updateUserResource.Email, userResource.Email);
            Assert.Equal(updateUserResource.FirstName, userResource.FirstName);
            Assert.Equal(updateUserResource.LastName, userResource.LastName);
        }
        
        public static void AssertEquals(UserResponse userResponse, UserResource userResource)
        {
            Assert.Equal(userResponse.User.Email, userResource.Email);
            Assert.Equal(userResponse.User.FirstName, userResource.FirstName);
            Assert.Equal(userResponse.User.LastName, userResource.LastName);
        }
    }
}