using ProjectManager.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BL
{
    public class ProjectBL
    {
        private IProjectManagerContext _context;

        public ProjectBL(IProjectManagerContext context)
        {
            _context = context;
        }
        public List<Entities.Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public Entities.Project GetProjectById(int id)
        {
            return _context.Projects.SingleOrDefault(x => x.ProjectId == id);
        }

        public void AddProject(Entities.Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void UpdateProject(Entities.Project project)
        {
            var projectModel = _context.Projects.Where(x => x.ProjectId == project.ProjectId).SingleOrDefault();
            if (projectModel != null)
            {
                projectModel.Project_Name = project.Project_Name;
                projectModel.Priority = project.Priority;
                projectModel.Start_Date = project.Start_Date;
                projectModel.End_Date = project.End_Date;
                //_context.Entry(projectModel).State = System.Data.Entity.EntityState.Modified;
                _context.MarkAsModified(projectModel);
                _context.SaveChanges();
            }
        }

        public void DeleteProject(int id)
        {
            var projectModel = _context.Projects.Where(x => x.ProjectId == id).SingleOrDefault();
            if (projectModel != null)
            {
                _context.Projects.Remove(projectModel);
                // _context.Entry(projectModel).State = System.Data.Entity.EntityState.Deleted;
                _context.MarkAsDeleted(projectModel);
                _context.SaveChanges();
            }
        }
    }
}
