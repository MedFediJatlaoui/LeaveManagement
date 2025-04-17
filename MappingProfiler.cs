   using AutoMapper;

    using LeaveManagement.DTOs;
    using LeaveManagement.Models;

    namespace LeaveManagementSystem
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // Mapping between LeaveRequest and LeaveRequestDto
                CreateMap<LeaveRequest, LeaveRequestDto>();
                CreateMap<LeaveRequestDto, LeaveRequest>();
            }
        }
    }


