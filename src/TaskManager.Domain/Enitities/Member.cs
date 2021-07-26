using System.Collections.Generic;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Enitities
{
    public class Member : AuditableEntity
    {
        public Member()
        {
            Tasks = new List<Task>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<Task> Tasks { get; }
    }
}
