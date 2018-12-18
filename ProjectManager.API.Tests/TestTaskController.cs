using System;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using System.Collections.Generic;
using NUnit.Framework;
using System.Web.Http;
using System.Web.Http.Results;

namespace TaskManager.Tests
{
    [TestFixture]
    public class TestTaskController
    {
        [Test]
        public void GetAllTasks_ReturnAllTasks()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.Tasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });
            

            //Act
            var controller = new TaskController(context);
            dynamic result = controller.GetAllTasks();
            
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(2, result.Content.Count);
        }

        [Test]
        public void GetParentTasks()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.ParentTasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(context);
            dynamic result = controller.GetParentTasks();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.Count);
        }

        [Test]
        public void GetTasksByProjectId_ReturnTask()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.Tasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(context);
            dynamic result = controller.GetTaskByProjectId(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.Count);
        }

        [Test]
        public void GetTaskById_ReturnTask()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.Tasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(context);
            dynamic result = controller.GetTaskById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.TaskId);
        }

        [Test]
        public void AddTask()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.Tasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });

            var task = new Task() {TaskId=3, TaskName = "Design", Priority = 5, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2) };

            //Act
            var controller = new TaskController(context);
            dynamic result = controller.AddTask(task);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, context.Tasks.Find(3).TaskId);

        }

        [Test]
        public void UpdateTask()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.Tasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });

            var task = new Task() { TaskId = 2, TaskName = "FS1", Priority = 5, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2) };

            //Act
            var controller = new TaskController(context);
            dynamic result = controller.UpdateTask(task);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, context.Tasks.Find(2).TaskId);
            Assert.AreEqual(task.TaskName, context.Tasks.Find(2).TaskName);
            Assert.AreEqual(task.Priority, context.Tasks.Find(2).Priority);
        }

        [Test]
        public void EndTask()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestTaskController();
            testController.Tasks().ForEach(x =>
            {
                context.Tasks.Add(x);
            });

            var task = new Task() { TaskId = 1};

            //Act
            var controller = new TaskController(context);
            dynamic result = controller.EndTask(task.TaskId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Tasks.Find(1).TaskId);
            Assert.AreEqual("Y", context.Tasks.Find(1).EndTask);
        }

        public List<Task> Tasks()
        {
            var tasks = new List<Task>();
            tasks.Add(new Task() { TaskId = 1, TaskName = "FS", Priority = 1, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2), ProjectId = 1 });
            tasks.Add(new Task() { TaskId = 2, TaskName = "TS", Priority = 10, ParentId = 1, StartDate = new DateTime(2018, 10, 3), EndDate = new DateTime(2018, 10, 4) });
            return tasks;
        }

        public List<Task> ParentTasks()
        {
            var tasks = new List<Task>();
            tasks.Add(new Task() { TaskId = 3, TaskName = "Parent", Priority = 21, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2), IsParent= true });
            return tasks;
        }
    }
}
