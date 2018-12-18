using ProjectManager.BL;
using ProjectManager.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManager.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectController : ApiController
    {
        private IProjectManagerContext _context;

        public ProjectController()
        {
            _context = new ProjectManagerContext();
        }
        public ProjectController(IProjectManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetProjects")]
        public IHttpActionResult GetAllProjects()
        {
            ProjectBL projectBl = new ProjectBL(_context);
            List<Entities.Project> projects = new List<Entities.Project>();
            try
            { 
                projects = projectBl.GetAllProjects();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                projectBl = null;
            }

            var pjts = (from p in projects
                       select new
                       {
                           ProjectId = p.ProjectId,
                           Project_Name = p.Project_Name,
                           Priority = p.Priority,
                           Start_Date = p.Start_Date?.ToString("dd-MM-yyyy"),
                           End_Date = p.End_Date?.ToString("dd-MM-yyyy"),
                           UserId = p.UserId,
                           Total_Tasks = p.Tasks.Count,
                           Completed_Tasks = p.Tasks.Count(x => x.EndTask == "Y")
                       }).ToList();
            return Ok(pjts);
        }

        [HttpGet]
        [Route("GetProjectById")]
        public IHttpActionResult GetProjectById(int id)
        {
            ProjectBL projectBl = new ProjectBL(_context);
            Entities.Project project = new Entities.Project();
            try
            {
                project = projectBl.GetProjectById(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                projectBl = null;
            }
            var formatProject = new
            {
                ProjectId = project.ProjectId,
                Project_Name = project.Project_Name,
                Priority = project.Priority,
                Start_Date = project.Start_Date?.ToString("yyyy-MM-dd"),
                End_Date = project.End_Date?.ToString("yyyy-MM-dd"),
                UserId = project.UserId
            };
            return Ok(formatProject);
        }

        [HttpPost]
        [Route("AddProject")]
        public IHttpActionResult AddProject(Entities.Project project)
        {
            ProjectBL projectBl = new ProjectBL(_context);
            
            try
            {
                projectBl.AddProject(project);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                projectBl = null;
            }
            return Ok();
        }

        [HttpPut]
        [Route("UpdateProject")]
        public IHttpActionResult UpdateProject(Entities.Project project)
        {
            ProjectBL projectBl = new ProjectBL(_context);

            try
            {
                projectBl.UpdateProject(project);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                projectBl = null;
            }
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteProject")]
        public IHttpActionResult DeleteProject(int id)
        {
            ProjectBL projectBl = new ProjectBL(_context);

            try
            {
                projectBl.DeleteProject(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                projectBl = null;
            }
            return Ok();
        }
    }
}
