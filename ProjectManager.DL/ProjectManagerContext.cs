using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ProjectManager.DL
{
    public class ProjectManagerContext : DbContext, IProjectManagerContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ProjectManagerContext() : base("name=projectdbconnectionstring")
        {

        }

        public void MarkAsModified(Task task)
        {
            Entry(task).State = EntityState.Modified;
        }

        public void MarkAsModified(Project project)
        {
            Entry(project).State = EntityState.Modified;
        }

        public void MarkAsModified(User user)
        {
            Entry(user).State = EntityState.Modified;
        }

        public void MarkAsDeleted(Project project)
        {
            Entry(project).State = EntityState.Deleted;
        }

        public void MarkAsDeleted(User user)
        {
            Entry(user).State = EntityState.Deleted;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().Property(p => p.TaskId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Task>().Property(p => p.TaskName).IsRequired();
            modelBuilder.Entity<Task>().Property(p => p.ParentId).IsOptional();
            modelBuilder.Entity<Task>().Property(p => p.Priority).IsOptional();
            modelBuilder.Entity<Task>().Property(p => p.StartDate).HasColumnType("Date").IsRequired();
            modelBuilder.Entity<Task>().Property(p => p.EndDate).HasColumnType("Date").IsRequired();
            modelBuilder.Entity<Task>().Property(p => p.EndTask).IsOptional();

            modelBuilder.Entity<User>().Property(p => p.UserId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(p => p.First_Name).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Last_Name).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Employee_Id).IsRequired();

            modelBuilder.Entity<Project>().Property(p => p.ProjectId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Project>().Property(p => p.Project_Name).IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.Start_Date).IsOptional();
            modelBuilder.Entity<Project>().Property(p => p.End_Date).IsOptional();

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
