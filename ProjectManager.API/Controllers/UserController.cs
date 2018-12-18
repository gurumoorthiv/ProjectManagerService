using ProjectManager.BL;
using ProjectManager.DL;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManager.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private IProjectManagerContext _context;
        public UserController()
        {
            _context = new ProjectManagerContext();
        }
        public UserController(IProjectManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetAllUsers()
        {
            UserBL userBl = new UserBL(_context);
            List<Entities.User> users = new List<Entities.User>();
            try
            { 
                users = userBl.GetAllUsers();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                userBl = null;
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserById")]
        public IHttpActionResult GetUserById(int id)
        {
            UserBL userBl = new UserBL(_context);
            Entities.User user = new Entities.User();
            try
            {
                user = userBl.GetUserById(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                userBl = null;
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("AddUser")]
        public IHttpActionResult AddUser(Entities.User user)
        {
            UserBL userBl = new UserBL(_context);
            try
            {
                userBl.AddUser(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                userBl = null;
            }
            return Ok();
        }

        [HttpPut]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser(Entities.User user)
        {
            UserBL userBl = new UserBL(_context);
            try
            {
                userBl.UpdateUser(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                userBl = null;
            }
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            UserBL userBl = new UserBL(_context);
            try
            {
                userBl.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                userBl = null;
            }
            return Ok();
        }
    }
}
