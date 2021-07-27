using System;
using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Enitities
{
    public sealed class Task : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TaskStatus_Enum Status { get; set; }
        public long MemberId { get; set; }

        public Member Member { get; set; }
    }
}
