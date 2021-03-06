﻿using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Web3.Core.Addresses.Models;
using Web3.Infra.Repositories;
using Web3.Infura;
using Web3.Infura.Addresses;

namespace Web3.Api.Addresses.Composition
{
    internal class AddressesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Settings>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, cc) => pi.ParameterType == typeof(string),
                        (pi, cc) => cc.Resolve<IConfiguration>()["Infura:Url"]))
                .AsSelf();
            builder
                .RegisterType<AddressesRepository>()
                .As<IRepository<AddressInfo, string>>()
                .As<IQueryableRepository<AddressInfo, string>>();

            base.Load(builder);
        }
    }
}