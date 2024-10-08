﻿using Autofac;
using Microsoft.AspNetCore.Identity;
using Project_management_system.Controllers;
using Project_management_system.CQRS;
using Project_management_system.DTO.UserDTOs;
using Project_management_system.Repositories;

namespace Project_management_system
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<Context>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            // builder.RegisterAssemblyTypes(typeof(ITokenGenerator).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(PasswordHasher<>)).As(typeof(IPasswordHasher<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<UserState>().InstancePerLifetimeScope();
            builder.RegisterType<ControllerParameters>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RequestParameters<>)).InstancePerLifetimeScope();
        }
    }
}
