using System.Collections.Generic;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using NBench;
using System;

namespace ProjectManager.PerTest
{
    public class PerTestProjectController
    {
        private const string GetAllProjectsCounterName = "GetAllProjectsCounter";
        private const string GetProjectByIdCounterName = "GetProjectByIdCounter";
        private const string AddProjectCounterName = "AddProjectCounter";
        private const string UpdateProjectCounterName = "UpdateProjectCounter";
        private const string DeleteProjectCounterName = "DeleteProjectCounter";

        private Counter getAllProjectsCounter;
        private Counter getProjectByIdCounter;
        private Counter addProjectCounter;
        private Counter updateProjectCounter;
        private Counter deleteProjectCounter;

        private const int AcceptableMinThroughput = 500;

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetAllProjectsCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetAllProjects_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getAllProjectsCounter = context.GetCounter(GetAllProjectsCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestProjectController();
            testController.Projects().ForEach(x =>
            {
                projectManagerContext.Projects.Add(x);
            });


            //Act
            var controller = new ProjectController(projectManagerContext);
            dynamic result = controller.GetAllProjects();

            getAllProjectsCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetProjectByIdCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetProjectById_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getProjectByIdCounter = context.GetCounter(GetProjectByIdCounterName);
            
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestProjectController();
            testController.Projects().ForEach(x =>
            {
                projectManagerContext.Projects.Add(x);
            });


            //Act
            var controller = new ProjectController(projectManagerContext);
            dynamic result = controller.GetProjectById(1);

            getProjectByIdCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddProjectCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void AddProject_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            addProjectCounter = context.GetCounter(AddProjectCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestProjectController();
            testController.Projects().ForEach(x =>
            {
                projectManagerContext.Projects.Add(x);
            });

            var task = new Project() { ProjectId = 3, Project_Name = "Test 3", Priority = 5, Start_Date = new DateTime(2018, 10, 1), End_Date = new DateTime(2018, 10, 2) };

            //Act
            var controller = new ProjectController(projectManagerContext);
            dynamic result = controller.AddProject(task);

            addProjectCounter.Increment();

        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(UpdateProjectCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void UpdateProject_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            updateProjectCounter = context.GetCounter(UpdateProjectCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestProjectController();
            testController.Projects().ForEach(x =>
            {
                projectManagerContext.Projects.Add(x);
            });

            var project = new Project() { ProjectId = 2, Project_Name = "Test Project 2", Priority = 25, Start_Date = new DateTime(2018, 10, 1), End_Date = new DateTime(2018, 10, 2) };

            //Act
            var controller = new ProjectController(projectManagerContext);
            dynamic result = controller.UpdateProject(project);

            updateProjectCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(DeleteProjectCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void DeleteProject_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            deleteProjectCounter = context.GetCounter(DeleteProjectCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestProjectController();
            testController.Projects().ForEach(x =>
            {
                projectManagerContext.Projects.Add(x);
            });

            var project = new Project() { ProjectId = 1 };

            //Act
            var controller = new ProjectController(projectManagerContext);
            dynamic result = controller.DeleteProject(project.ProjectId);

            deleteProjectCounter.Increment();

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
