using ProjectManager.DL;
using ProjectManager.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.BL
{
    public class UserBL
    {
        private IProjectManagerContext _context;

        public UserBL(IProjectManagerContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
           return  _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.UserId == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var userModel = _context.Users.Where(x => x.UserId == user.UserId).SingleOrDefault();
            if(userModel != null)
            {
                userModel.First_Name = user.First_Name;
                userModel.Last_Name = user.Last_Name;
                userModel.Employee_Id = user.Employee_Id;
                //_context.Entry(userModel).State = System.Data.Entity.EntityState.Modified;
                _context.MarkAsModified(userModel);
                _context.SaveChanges();
            }
           
        }

        public void DeleteUser(int userId)
        {
            var userModel = _context.Users.Where(x => x.UserId == userId).SingleOrDefault();
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
                //_context.Entry(userModel).State = System.Data.Entity.EntityState.Deleted;
                _context.MarkAsDeleted(userModel);
                _context.SaveChanges();
            }

        }
    }
}
