using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Project_Name { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        //public virtual Task Task { get; set; }
    }
}
