using System.Collections.Generic;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Enitities
{
    /// <summary>
    /// Member user meta data
    /// </summary>
    public class Member : AuditableEntity
    {
        public Member()
        {
            Tasks = new List<Task>();
        }
        public long UserId { get; set; }

        public IList<Task> Tasks { get; }
    }
}
