using NUnit.Framework;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Tests;

namespace ProjectManager.API.Tests
{
    [TestFixture]
    public class TestUserController
    {
        [Test]
        public void GetAllUsers_ReturnAllUsers()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestUserController();
            testController.Users().ForEach(x =>
            {
                context.Users.Add(x);
            });


            //Act
            var controller = new UserController(context);
            dynamic result = controller.GetAllUsers();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(2, result.Content.Count);
        }

        [Test]
        public void GetUserById_ReturnUser()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestUserController();
            testController.Users().ForEach(x =>
            {
                context.Users.Add(x);
            });


            //Act
            var controller = new UserController(context);
            dynamic result = controller.GetUserById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.UserId);
        }

        [Test]
        public void AddUser()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestUserController();
            testController.Users().ForEach(x =>
            {
                context.Users.Add(x);
            });

            var user = new User() { UserId = 3, First_Name = "Test First Name 3", Last_Name = "Test Last Name 3", Employee_Id = "13"};

            //Act
            var controller = new UserController(context);
            dynamic result = controller.AddUser(user);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, context.Users.Find(3).UserId);

        }

        [Test]
        public void Updateuser()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestUserController();
            testController.Users().ForEach(x =>
            {
                context.Users.Add(x);
            });

            var user = new User() { UserId = 1, First_Name = "Mathan", Last_Name = "Test Last Name 1", Employee_Id = "12" };

            //Act
            var controller = new UserController(context);
            dynamic result = controller.UpdateUser(user);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Users.Find(1).UserId);
            Assert.AreEqual(user.First_Name, context.Users.Find(1).First_Name);
        }

        [Test]
        public void DeleteUser()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestUserController();
            testController.Users().ForEach(x =>
            {
                context.Users.Add(x);
            });

            var user = new User() { UserId = 1 };

            //Act
            var controller = new UserController(context);
            Assert.AreEqual(2, context.Users.Count());
            dynamic result = controller.DeleteUser(user.UserId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Users.Count());

        }

        public List<User> Users()
        {
            var users = new List<User>();
            users.Add(new User() { UserId = 1, First_Name = "Test First Name 1", Last_Name = "Test Last Name 1", Employee_Id = "12" });
            users.Add(new User() { UserId = 2, First_Name = "Test First Name 2", Last_Name = "Test Last Name 2", Employee_Id = "13" });
            return users;
        }
    }
}
