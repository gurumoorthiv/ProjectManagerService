using ProjectManager.BL;
using ProjectManager.DL;
using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManager.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        private IProjectManagerContext _context;

        public TaskController()
        {
            _context = new ProjectManagerContext();
        }
        public TaskController(IProjectManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllTasks")]
        public IHttpActionResult GetAllTasks()
        {
            var taskBL = new TaskBL(_context);
            List<Task> allTasks = new List<Task>();
            try
            {
                allTasks = taskBL.GetAllTasks();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                taskBL = null;
            }

            var tasks = (from ts in allTasks
                        from pts in allTasks
                        .Where(x => x.TaskId == ts.ParentId)
                        .DefaultIfEmpty()
                        select new
                        {
                            TaskId = ts.TaskId,
                            TaskName = ts.TaskName,
                            Priority = ts.Priority,
                            StartDate = ts.StartDate.ToString("dd-MM-yyyy"),
                            EndDate = ts.EndDate.ToString("dd-MM-yyyy"),
                            ParentTaskName = pts?.TaskName,
                            EndTask = ts.EndTask,
                            IsParent = ts.IsParent
                        }).ToList();
            
            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetParentTasks")]
        public IHttpActionResult GetParentTasks()
        {
            var taskBL = new TaskBL(_context);
            List<Task> allTasks = new List<Task>();
            try
            {
                allTasks = taskBL.GetParentTasks();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                taskBL = null;
            }

            var tasks = (from ts in allTasks
                         select new
                         {
                             TaskId = ts.TaskId,
                             TaskName = ts.TaskName
                         }).ToList();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetTaskById")]
        public IHttpActionResult GetTaskById(int taskId)
        {
            var taskBL = new TaskBL(_context);
            var task = taskBL.GetById(taskId);
            var formatTask = new
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                Priority = task.Priority,
                StartDate = task.StartDate.ToString("yyyy-MM-dd"),
                EndDate = task.EndDate.ToString("yyyy-MM-dd"),
                ParentId = task.ParentId,
                IsParent = task.IsParent,
                ProjectId = task.ProjectId,
                UserId = task.UserId
            };
            return Ok(formatTask);
        }

        [HttpGet]
        [Route("GetTaskByProjectId")]
        public IHttpActionResult GetTaskByProjectId(int projectId)
        {
            var taskBL = new TaskBL(_context);
            var allTasks = taskBL.GetByProjectId(projectId);
            var tasks = (from ts in allTasks
                         from pts in allTasks
                             .Where(x => x.TaskId == ts.ParentId)
                             .DefaultIfEmpty()
                         select new
                         {
                             TaskId = ts.TaskId,
                             TaskName = ts.TaskName,
                             Priority = ts.Priority,
                             StartDate = ts.StartDate.ToString("dd-MM-yyyy"),
                             EndDate = ts.EndDate.ToString("dd-MM-yyyy"),
                             ParentTaskName = pts?.TaskName,
                             EndTask = ts.EndTask,
                             IsParent = ts.IsParent
                         }).ToList();
            return Ok(tasks);
        }

        [Route("AddTask")]
        [HttpPost]
        public IHttpActionResult AddTask(Task task)
        {
            var taskBL = new TaskBL(_context);
            taskBL.AddTask(task);
            return Ok();
        }

        [Route("UpdateTask")]
        [HttpPut]
        public IHttpActionResult UpdateTask(Task task)
        {
            var taskBL = new TaskBL(_context);
            taskBL.UpdateTask(task);
            return Ok();
        }

        [Route("EndTask")]
        [HttpDelete]
        public IHttpActionResult EndTask(int taskId)
        {
            var taskBL = new TaskBL(_context);
            taskBL.EndTask(taskId);
            return Ok();
        }
    }
}
