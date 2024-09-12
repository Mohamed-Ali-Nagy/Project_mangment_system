﻿using Autofac;
using Project_management_system.Services;

namespace Project_management_system
{
    public class AutoFacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IEmailService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
