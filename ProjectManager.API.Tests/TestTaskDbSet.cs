using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Entities;

namespace TaskManager.Tests
{
    public class TestTaskDbSet : TestDbSet<Task>
    {
        public override Task Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.TaskId == (int)keyValues.Single());
        }
    }

    public class TestProjectDbSet : TestDbSet<Project>
    {
        public override Project Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.ProjectId == (int)keyValues.Single());
        }
    }

    public class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.UserId == (int)keyValues.Single());
        }
    }
}
