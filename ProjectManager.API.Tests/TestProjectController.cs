using NUnit.Framework;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Tests;

namespace ProjectManager.API.Tests
{
    [TestFixture]
    class TestProjectController
    {
        [Test]
        public void GetAllProjects_ReturnAllProjects()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestProjectController();
            testController.Projects().ForEach(x =>
            {
                context.Projects.Add(x);
            });


            //Act
            var controller = new ProjectController(context);
            dynamic result = controller.GetAllProjects();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(2, result.Content.Count);
        }

        [Test]
        public void GetProjectById_ReturnProject()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestProjectController();
            testController.Projects().ForEach(x =>
            {
                context.Projects.Add(x);
            });


            //Act
            var controller = new ProjectController(context);
            dynamic result = controller.GetProjectById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.ProjectId);
        }

        [Test]
        public void AddProject()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestProjectController();
            testController.Projects().ForEach(x =>
            {
                context.Projects.Add(x);
            });

            var task = new Project() { ProjectId = 3, Project_Name = "Test 3", Priority = 5, Start_Date = new DateTime(2018, 10, 1), End_Date = new DateTime(2018, 10, 2) };

            //Act
            var controller = new ProjectController(context);
            dynamic result = controller.AddProject(task);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, context.Projects.Find(3).ProjectId);

        }

        [Test]
        public void UpdateProject()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestProjectController();
            testController.Projects().ForEach(x =>
            {
                context.Projects.Add(x);
            });

            var project = new Project() { ProjectId = 2, Project_Name = "Test Project 2", Priority = 25, Start_Date = new DateTime(2018, 10, 1), End_Date = new DateTime(2018, 10, 2) }; 

            //Act
            var controller = new ProjectController(context);
            dynamic result = controller.UpdateProject(project);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, context.Projects.Find(2).ProjectId);
            Assert.AreEqual(project.Project_Name, context.Projects.Find(2).Project_Name);
            Assert.AreEqual(project.Priority, context.Projects.Find(2).Priority);
        }

        [Test]
        public void DeleteProject()
        {
            //Arrange
            var context = new TestProjectManagerContext();
            var testController = new TestProjectController();
            testController.Projects().ForEach(x =>
            {
                context.Projects.Add(x);
            });

            var project = new Project() { ProjectId = 1 };

            //Act
            var controller = new ProjectController(context);
            Assert.AreEqual(2, context.Projects.Count());
            dynamic result = controller.DeleteProject(project.ProjectId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Projects.Count());

        }

        public List<Project> Projects()
        {
            var tasks = new List<Task>();
            tasks.Add(new Task() { TaskId = 1, TaskName = "FS", Priority = 1, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2), ProjectId = 1 });
            tasks.Add(new Task() { TaskId = 2, TaskName = "TS", Priority = 10, ParentId = 1, StartDate = new DateTime(2018, 10, 3), EndDate = new DateTime(2018, 10, 4) });
            
            var projects = new List<Project>();
            projects.Add(new Project() { ProjectId = 1, Project_Name = "Test 1", Priority = 10, Start_Date = new DateTime(2018, 09, 1), End_Date = new DateTime(2018, 10, 2), Tasks = tasks });
            projects.Add(new Project() { ProjectId = 2, Project_Name = "Test 2", Priority = 20, Start_Date = new DateTime(2018, 10, 3), End_Date = new DateTime(2018, 11, 4), Tasks = tasks });
            return projects;
        }

    }
}
