using AutoMapper;
using CSP_NET.Work.
    .Dtos;
using CSP_NET.Work.ProjectGenerals.Dtos;
using CSP_NET.Work.ProjectRole.Dtos;
using CSP_NET.Work.SyncCtin.DTO;
using CSP_NET.Work.DepartmentTeams;
using CSP_NET.Work.DepartmentTeams.Dtos;
using CTIN.Abp.Identity;
using System.Linq;
using CTIN.Abp;
using CSP_NET.Work.ProjectState;
using CSP_NET.Work.ProjectState.Dtos;
using System;
using System.Collections.Generic;
using CSP_NET.Work.WorkManagement;
using CSP_NET.Work.Common;
using CSP_NET.Work.ConfigworkAccepts;
using CSP_NET.Work.ConfigworkAccepts.Dtos;
using CSP.Category.CategoryManagement;
using CSP_NET.Work.LogWorks.Dtos;
using CSP_NET.Work.LogWorks;
using CSP_NET.Work.ResourseManagement.Dtos;
using CSP_NET.Work.ResourseManagement;
using System.Reflection.Metadata;

namespace CSP_NET.Work;

public class WorkApplicationAutoMapperProfile : Profile
{
    public WorkApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Menu.Menu, MenuDto>().ReverseMap();
        CreateMap<Menu.Menu, CreateUpdateMenuDto>().ReverseMap();
        CreateMap<ProjectRole.ProjectRole, ProjectRoleDto>().ReverseMap();
        CreateMap<ProjectRole.ProjectRole, CreateUpdateProjectRoleDto>().ReverseMap();
        CreateMap<ProjectGenerals.ProjectGeneral, ProjectGeneralDto>().ReverseMap(); ;
        CreateMap<CreateUpdateProjectGeneralDto, ProjectGenerals.ProjectGeneral>(MemberList.Source);
        CreateMap<ProjectGenerals.ProjectGeneral, DropdownItem>()
            .ForMember(d => d.Label, s => s.MapFrom(p => p.Name))
            .ForMember(d => d.Value, s => s.MapFrom(p => p.Id));

        //CreateMap<DepartmentTeam, DepartmentTeamDto>()
        //    .ForMember(a => a.DepartmentName, f=>f.MapFrom(a=>a.Department != null? a.Department.DisplayName : string.Empty))
        //    .ForMember(a => a.LstMember, f => f.MapFrom(a => a.Members !=null? a.Members.Select(u=>new NameValue<Guid>(u.Member.UserName + ":" + u.Member.Name, u.UserId)).ToList(): string.Empty));
        //CreateMap<DepartmentTeam, DepartmentTeamListDto>().ForMember(a => a.DepartmentName, f => f.MapFrom(a => a.Department.DisplayName));
        //CreateMap<CreateUpdateDepartmentTeamDto, DepartmentTeam>(MemberList.Source);

        CreateMap<DepartmentTeam, DepartmentTeamDto>()
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DisplayName : string.Empty))
           .ForMember(dest => dest.LstMember, opt => opt.MapFrom(src => src.Members != null ? src.Members.Select(u => new NameValue<Guid>(u.Member.UserName + ":" + u.Member.Name, u.UserId)).ToList() : new List<NameValue<Guid>>()));

        CreateMap<DepartmentTeam, DepartmentTeamListDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DisplayName));

        CreateMap<CreateUpdateDepartmentTeamDto, DepartmentTeam>(MemberList.Source);
        CreateMap<ProjectState.ProjectState, ProjectStateDto>();
        CreateMap<ProjectState.ProjectState, _ProjectStateDto>();
        CreateMap<OrganizationUnit, _OrganizationUnit>();
        CreateMap<ProjectState.ProjectState, DropdownItemWork>();
        CreateMap<CreateUpdateProjectStateDto, ProjectState.ProjectState>(MemberList.Source);
        CreateMap<CspWork, CspWorkDto>();
        CreateMap<CreateUpdateCspWorkDto, CspWork>(MemberList.Source);
        CreateMap<CspWorkCheckList, CspWorkCheckListDto>().ReverseMap();
        CreateMap<CspWorkNote, CspWorkNoteDto>().ReverseMap();
        CreateMap<CspWork, DropdownItemWork>();

        CreateMap<ConfigWorkAccept, ConfigWorkAcceptDto>();
        CreateMap<ConfigWorkAccept, ConfigWorkAcceptListDto>();
        CreateMap<CreateUpdateConfigWorkAcceptDto, ConfigWorkAccept>(MemberList.Source);

        CreateMap<LogWork, LogWorkDto>();
        CreateMap<LogWork, LogWorkListDto>();
        CreateMap<CreateUpdateLogWorkDto, LogWork>(MemberList.Source);


        CreateMap<Documents, DocumentDto>().ReverseMap();
        
    }
}
    