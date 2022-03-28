using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using StampinUp.Service.Controllers;
using StampinUp.Service.Models;
using StampinUp.Service.Services;
using Xunit;

namespace Tests
{
    public class UsersControllerTest
    {
        private readonly IGoRESTApiService goRestService;
        private readonly IUsersService usersService;
        private readonly UsersController usersController;        

        public UsersControllerTest()
        {
            /* TODO: Tests project; having trouble getting dependency injection to work:
             * got as far as getting this error: 
             * The following constructor parameters did not have matching fixture data: IGoRESTApiService goRestService
             * can't instantiate IHttpClientFactory clientFactory, but in the StampinUp.Service project, the UserController works with it injected
             * but in the Tests project it doesn't work the same.  Not sure what is required but I think it has something to do with Startup.cs etc.             * 
             */
            goRestService = new GoRESTApiService();
            usersService = new UsersServiceFake(goRestService);
            usersController = new UsersController(usersService);
        }


        [Fact]
        public void Get_ReturnsOkResult()
        {
            //Act
            var okResult = usersController.Get();

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_ReturnsAllUsers()
        {
            //Act
            var okResult = usersController.Get() as OkObjectResult;

            //Assert
            var users = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(4, users.Count);
        }

        [Fact]
        public void GetById_NonExisting_ReturnsNotFoundResult()
        {
            //Act
            var notFoundResult = usersController.Get(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_Existing_ReturnsOkResult()
        {

            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");

            //Act
            var okResult = usersController.Get(id);

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_Existing_ReturnsCorrectUser()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");

            //Act
            var okResult = usersController.Get(id) as OkObjectResult;

            //Assert
            var user = Assert.IsType<User>(okResult.Value);
            Assert.Equal(id, (okResult.Value as User).Id);
        }

        [Fact]
        public void Insert_InvalidUser_ReturnsBadRequestResult()
        {
            //Arrange
            User userMissingName = new User()
            {
                Id = Guid.NewGuid(),
                Email = "elmo@sesamestreet.com",
                Name = "", //Required field
                Country = "Elmo's World",
                CatchPhrase = "la la la la",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate = new DateTime(2020, 01, 01, 0, 0, 0)
                    }
                }
            };

            //Act
            var badRequestResult = usersController.Insert(userMissingName);

            //Assert
            Assert.IsType<NotFoundResult>(badRequestResult);
        }

        [Fact]
        public void Insert_ValidUser_ReturnsCreatedAtActionResult()
        {
            //Arrange
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "elmo@sesamestreet.com",
                Name = "Elmo",
                Country = "Elmo's World",
                CatchPhrase = "la la la la",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate = new DateTime(2020, 01, 01, 0, 0, 0)
                    }
                }
            };

            //Act
            var createdAtActionResult = usersController.Insert(user);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        }

        [Fact]
        public void Insert_ValidUser_ReturnsCreatedUser()
        {
            //Arrange
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "elmo@sesamestreet.com",
                Name = "Elmo",
                Country = "Elmo's World",
                CatchPhrase = "la la la la",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate = new DateTime(2020, 01, 01, 0, 0, 0)
                    }
                }
            };

            //Act
            var createdAtActionResult = usersController.Insert(user) as CreatedAtActionResult;
            var createdUser = createdAtActionResult.Value as User;

            //Assert
            Assert.IsType<User>(createdUser);
            Assert.Equal("Elmo", user.Name);
        }

        [Fact]
        public void Replace_InvalidUser_ReturnsBadRequestResult()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");
            User userMissingName = new User()
            {
                Id = Guid.NewGuid(),
                Email = "elmo@sesamestreet.com",
                Name = "", //Required field
                Country = "Elmo's World",
                CatchPhrase = "la la la la",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate = new DateTime(2020, 01, 01, 0, 0, 0)
                    }
                }
            };

            //Act
            var badRequestResult = usersController.Replace(id, userMissingName);

            //Assert
            Assert.IsType<NotFoundResult>(badRequestResult);
        }

        [Fact]
        public void Replace_ValidUser_ReturnsCreatedAtActionResult()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "elmo@sesamestreet.com",
                Name = "Elmo",
                Country = "Elmo's World",
                CatchPhrase = "la la la la",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate = new DateTime(2020, 01, 01, 0, 0, 0)
                    }
                }
            };

            //Act
            var createdAtActionResult = usersController.Replace(id, user);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        }

        [Fact]
        public void Replace_ValidUser_ReturnsCreatedUser()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "elmo@sesamestreet.com",
                Name = "Elmo",
                Country = "Elmo's World",
                CatchPhrase = "la la la la",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate = new DateTime(2020, 01, 01, 0, 0, 0)
                    }
                }
            };

            //Act
            var createdAtActionResult = usersController.Replace(id, user) as CreatedAtActionResult;
            var createdUser = createdAtActionResult.Value as User;

            //Assert
            Assert.IsType<User>(createdUser);
            Assert.Equal("Elmo", user.Name);
        }

        [Fact]
        public void Update_InvalidUser_ReturnsBadRequestResult()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");
            User userMissingName = new User()
            {
                Email = "elmo@sesamestreet.com",
                Name = "" //Required field
            };

            //Act
            var badRequestResult = usersController.Update(id, userMissingName);

            //Assert
            Assert.IsType<NotFoundResult>(badRequestResult);
        }

        [Fact]
        public void Update_ValidUser_ReturnsCreatedAtActionResult()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");
            User user = new User()
            {
                Email = "elmo@sesamestreet.com",
                Name = "Elmo"
            };

            //Act
            var createdAtActionResult = usersController.Update(id, user);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        }

        [Fact]
        public void Update_ValidUser_ReturnsCreatedUser()
        {
            //Arrange
            Guid id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3");
            User user = new User()
            {
                Email = "elmo@sesamestreet.com",
                Name = "Elmo"
            };

            //Act
            var createdAtActionResult = usersController.Update(id, user) as CreatedAtActionResult;
            var createdUser = createdAtActionResult.Value as User;

            //Assert
            Assert.IsType<User>(createdUser);
            Assert.Equal("Elmo", user.Name);
        }

    }
}
