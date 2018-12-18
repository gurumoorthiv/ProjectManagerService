using System;
using System.Collections.Generic;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using NBench;

namespace ProjectManager.PerTest
{
    public class PerTestUserController
    {
        private const string GetAllUsersCounterName = "GetAllUsersCounter";
        private const string GetUserByIdCounterName = "GetUserByIdCounter";
        private const string AddUserCounterName = "AddUserCounter";
        private const string UpdateUserCounterName = "UpdateUserCounter";
        private const string DeleteUserCounterName = "DeleteUserCounter";

        private Counter getAllUsersCounter;
        private Counter getUserByIdCounter;
        private Counter addUserCounter;
        private Counter updateUserCounter;
        private Counter deleteUserCounter;

        private const int AcceptableMinThroughput = 500;

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetAllUsersCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetAllUsers_ThroughtputMode(BenchmarkContext context)
        {
            //Arrange
            getAllUsersCounter = context.GetCounter(GetAllUsersCounterName);
            
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestUserController();
            testController.Users().ForEach(x =>
            {
                projectManagerContext.Users.Add(x);
            });


            //Act
            var controller = new UserController(projectManagerContext);
            dynamic result = controller.GetAllUsers();

            getAllUsersCounter.Increment();
            
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetUserByIdCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetUserById_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getUserByIdCounter = context.GetCounter(GetUserByIdCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestUserController();
            testController.Users().ForEach(x =>
            {
                projectManagerContext.Users.Add(x);
            });


            //Act
            var controller = new UserController(projectManagerContext);
            dynamic result = controller.GetUserById(1);

            getUserByIdCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddUserCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void AddUser_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            addUserCounter = context.GetCounter(AddUserCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestUserController();
            testController.Users().ForEach(x =>
            {
                projectManagerContext.Users.Add(x);
            });

            var user = new User() { UserId = 3, First_Name = "Test First Name 3", Last_Name = "Test Last Name 3", Employee_Id = "13" };

            //Act
            var controller = new UserController(projectManagerContext);
            dynamic result = controller.AddUser(user);

            addUserCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(UpdateUserCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void Updateuser_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            updateUserCounter = context.GetCounter(UpdateUserCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestUserController();
            testController.Users().ForEach(x =>
            {
                projectManagerContext.Users.Add(x);
            });

            var user = new User() { UserId = 1, First_Name = "Mathan", Last_Name = "Test Last Name 1", Employee_Id = "12" };

            //Act
            var controller = new UserController(projectManagerContext);
            dynamic result = controller.UpdateUser(user);

            updateUserCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(DeleteUserCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void DeleteUser_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            deleteUserCounter = context.GetCounter(DeleteUserCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestUserController();
            testController.Users().ForEach(x =>
            {
                projectManagerContext.Users.Add(x);
            });

            var user = new User() { UserId = 1 };

            //Act
            var controller = new UserController(projectManagerContext);
            dynamic result = controller.DeleteUser(user.UserId);

            deleteUserCounter.Increment();

        }

        [PerfCleanup]
        public void CleanUp(BenchmarkContext context)
        {

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
