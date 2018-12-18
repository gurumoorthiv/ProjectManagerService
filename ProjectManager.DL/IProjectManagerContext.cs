using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace ProjectManager.DL
{
    public interface IProjectManagerContext : IDisposable
    {
        DbSet<Task> Tasks { get; set; }

        DbSet<Project> Projects { get; set; }

        DbSet<User> Users { get; set; }
        void MarkAsModified(Task task);
        void MarkAsModified(Project task);
        void MarkAsModified(User task);
        void MarkAsDeleted(Project task);
        void MarkAsDeleted(User task);
        int SaveChanges();

    }
}
