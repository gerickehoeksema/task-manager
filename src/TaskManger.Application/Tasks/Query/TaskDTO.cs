using AutoMapper;
using System;
using TaskManager.Application.Mappings;
using TaskManager.Application.Member.Query;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Query
{
    public class TaskDTO : IMapFrom<Domain.Enitities.Task>
    {
        public long Id { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TaskStatus_Enum Status { get; set; }
        public long? MemberId { get; set; }

        public MemberDTO Member { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Enitities.Task, TaskDTO>()
                .ForMember(d => d.Member, opt => opt.Ignore());
        }
    }
}