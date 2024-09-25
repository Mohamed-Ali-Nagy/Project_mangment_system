﻿using AutoMapper;
using Project_management_system.CQRS.Projects.Queries;
using Project_management_system.CQRS.Projects.Commands;
using Project_management_system.CQRS.Projects.Orchestrators;
using Project_management_system.Models;
using Project_management_system.ViewModels.ProjectVMs;

namespace Project_management_system.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectVM, CreateProjectOrchestrator>();
            CreateMap<CreateProjectOrchestrator, CreateProjectCommand>();
            CreateMap<CreateProjectCommand, Project>().ReverseMap();
            CreateMap<Project, ProjectListDTO>().ReverseMap();

            CreateMap<UpdateProjectVM, UpdateProjectDTO>();
            CreateMap<UpdateProjectDTO, Project>();
        }
    }
}
