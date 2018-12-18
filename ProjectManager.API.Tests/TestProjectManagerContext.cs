using ProjectManager.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ProjectManager.Entities;

namespace TaskManager.Tests
{
    public class TestProjectManagerContext : IProjectManagerContext
    {
        public DbSet<Task> Tasks { get; set ; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public TestProjectManagerContext()
        {
            Tasks = new TestTaskDbSet();

            Projects = new TestProjectDbSet();
            Users = new TestUserDbSet();
        }
        public void MarkAsModified(Task task)
        {
            
        }

        public void MarkAsModified(Project project)
        {
            
        }

        public void MarkAsModified(User user)
        {
            
        }

        public void MarkAsDeleted(Project project)
        {
            
        }

        public void MarkAsDeleted(User user)
        {
            
        }

        public int SaveChanges()
        {
            return 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TestTaskManagerContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
