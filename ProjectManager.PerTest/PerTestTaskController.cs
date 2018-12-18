using System.Collections.Generic;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using NBench;
using System;

namespace ProjectManager.PerTest
{
    public class PerTestTaskController
    {
        private const string GetAllTasksCounterName = "GetAllTasksCounter";
        private const string GetParentTasksCounterName = "GetParentTasksCounter";
        private const string GetTaskByProjectIdCounterName = "GetTaskByProjectIdCounter";
        private const string GetTaskByIdCounterName = "GetTaskByIdCounter";
        private const string AddTaskCounterName = "AddTaskCounter";
        private const string UpdateTaskCounterName = "UpdateTaskCounter";
        private const string EndTaskCounterName = "EndTaskCounter";

        private Counter getAllTasksCounter;
        private Counter getParentTasksCounter;
        private Counter getTaskByProjectIdCounter;
        private Counter getTaskByIdCounter;
        private Counter addTaskCounter;
        private Counter updateTaskCounter;
        private Counter endTaskCounter;

        private const int AcceptableMinThroughput = 500;

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetAllTasksCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetAllTasks_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getAllTasksCounter = context.GetCounter(GetAllTasksCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.Tasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.GetAllTasks();

            getAllTasksCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetParentTasksCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetParentTasks_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getParentTasksCounter = context.GetCounter(GetParentTasksCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.ParentTasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.GetParentTasks();

            getParentTasksCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetTaskByProjectIdCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetTasksByProjectId_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getTaskByProjectIdCounter = context.GetCounter(GetTaskByProjectIdCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.Tasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.GetTaskByProjectId(1);

            getTaskByProjectIdCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(GetTaskByIdCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void GetTaskById_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            getTaskByIdCounter = context.GetCounter(GetTaskByIdCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.Tasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });


            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.GetTaskById(1);

            getTaskByIdCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddTaskCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void AddTask_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            addTaskCounter = context.GetCounter(AddTaskCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.Tasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });

            var task = new Task() { TaskId = 3, TaskName = "Design", Priority = 5, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2) };

            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.AddTask(task);

            addTaskCounter.Increment();

        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(UpdateTaskCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void UpdateTask_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            updateTaskCounter = context.GetCounter(UpdateTaskCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.Tasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });

            var task = new Task() { TaskId = 2, TaskName = "FS1", Priority = 5, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2) };

            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.UpdateTask(task);

            updateTaskCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(EndTaskCounterName, MustBe.GreaterThanOrEqualTo, AcceptableMinThroughput)]
        public void EndTask_ThroughputMode(BenchmarkContext context)
        {
            //Arrange
            endTaskCounter = context.GetCounter(EndTaskCounterName);
            var projectManagerContext = new TestProjectManagerContext();
            var testController = new PerTestTaskController();
            testController.Tasks().ForEach(x =>
            {
                projectManagerContext.Tasks.Add(x);
            });

            var task = new Task() { TaskId = 1 };

            //Act
            var controller = new TaskController(projectManagerContext);
            dynamic result = controller.EndTask(task.TaskId);

            endTaskCounter.Increment();
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
            tasks.Add(new Task() { TaskId = 3, TaskName = "Parent", Priority = 21, StartDate = new DateTime(2018, 10, 1), EndDate = new DateTime(2018, 10, 2), IsParent = true });
            return tasks;
        }
    }
}
