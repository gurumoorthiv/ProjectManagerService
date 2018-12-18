using ProjectManager.DL;
using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.BL
{
    public class TaskBL
    {
        private IProjectManagerContext _context;

        public TaskBL(IProjectManagerContext context)
        {
            _context = context;
        }
        public List<Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public List<Task> GetParentTasks()
        {
            return _context.Tasks.Where(x => x.IsParent).ToList();
        }

        public Task GetTaskById(int id)
        {
            return _context.Tasks.SingleOrDefault(x => x.TaskId == id);
        }

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            var updateTask = _context.Tasks.Where(x => x.TaskId == task.TaskId).SingleOrDefault();
            updateTask.TaskName = task.TaskName;
            updateTask.ParentId = task.ParentId;
            updateTask.Priority = task.Priority;
            updateTask.StartDate = task.StartDate;
            updateTask.EndDate = task.EndDate;
            updateTask.ProjectId = task.ProjectId;
            updateTask.UserId = task.UserId;
            _context.MarkAsModified(updateTask);
            _context.SaveChanges();
        }

        public void EndTask(int taskId)
        {
            var task = _context.Tasks.Where(x => x.TaskId == taskId).SingleOrDefault();
            task.EndTask = "Y";
            _context.MarkAsModified(task);
            _context.SaveChanges();
        }

        public Task GetById(int taskId)
        {
            return _context.Tasks.SingleOrDefault(x => x.TaskId == taskId);
        }

        public List<Task> GetByProjectId(int projectId)
        {
            return _context.Tasks.Where(x => x.ProjectId == projectId).ToList();
        }
    }
}
