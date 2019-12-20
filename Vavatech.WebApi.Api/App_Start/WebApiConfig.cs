using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using Unity;
using Unity.Lifetime;
using Vavatech.WebApi.Api.Contstraints;
using Vavatech.WebApi.Api.Filters;
using Vavatech.WebApi.Api.Handlers;
using Vavatech.WebApi.Api.Resolvers;
using Vavatech.WebApi.DbServices;
using Vavatech.WebApi.FakeServices;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            IUnityContainer container = new UnityContainer();

            container.RegisterType<ICustomerService, DbCustomerService>();
            container.RegisterType<Faker<Customer>, CustomerFaker>();
            container.RegisterType<Faker<Address>, AddressFaker>();

            //container.RegisterType<IProductService, FakeProductService>();
            //container.RegisterType<Faker<Product>, ProductFaker>(new ContainerControlledLifetimeManager());

            container.RegisterType<IProductService, DbProductService>();
            container.RegisterType<WarehouseContext>();

            container.RegisterType<IOrderService, DbOrderService>();


            container.RegisterType<IAuthenticationFilter, BasicAuthenticationFilter>();

            config.DependencyResolver = new UnityDependencyResolver(container);
          
            config.MessageHandlers.Add(new LoggerMessageHandler());
            //config.MessageHandlers.Add(new SecretKeyMessageHandler());

           // config.Filters.Add(new BasicAuthenticationFilter());
            config.Filters.Add(container.Resolve<IAuthenticationFilter>());


            var constraintResolver = new DefaultInlineConstraintResolver();

            constraintResolver.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint));

            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
