using AutoMapper;
using System;

namespace TaskManager.Application.Member.Query
{
    public class MemberDTO
    {
        public long Id { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Enitities.Member, MemberDTO>();
        }
    }
}