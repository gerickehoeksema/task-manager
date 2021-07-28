using AutoMapper;
using System;
using System.Collections.Generic;
using TaskManager.Application.Mappings;
using TaskManager.Application.Tasks.Query;

namespace TaskManager.Application.Member.Query
{
    public class MemberDTO : IMapFrom<Domain.Enitities.Member>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }

        public IList<TaskDTO> Tasks { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Enitities.Member, MemberDTO>();
        }
    }
}