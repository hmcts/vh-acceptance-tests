﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using AcceptanceTests.TestAPI.Common.Security;
using AcceptanceTests.TestAPI.Contract.Requests;
using AcceptanceTests.TestAPI.DAL.Commands;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Swagger;
using AcceptanceTests.TestAPI.Telemetry;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace AcceptanceTests.TestAPI
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            var contractsXmlFile = $"{typeof(CreateNewUserRequest).Assembly.GetName().Name}.xml";
            var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                c.AddFluentValidationRules();
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(contractsXmlPath);
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
                c.OperationFilter<AuthResponsesOperationFilter>();
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        public static IServiceCollection AddCustomTypes(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddScoped<ITokenProvider, AzureTokenProvider>();
            services.AddSingleton<ITelemetryInitializer, BadRequestTelemetry>();

            services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddScoped<IQueryHandler, QueryHandler>();

            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<ICommandHandler, CommandHandler>();

            services.AddScoped<IAllocationService, AllocationService>();

            RegisterCommandHandlers(services);
            RegisterQueryHandlers(services);

            return services;
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            var commandHandlers = typeof(ICommand).Assembly.GetTypes().Where(t =>
                t.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

            foreach (var queryHandler in commandHandlers)
            {
                var serviceType = queryHandler.GetInterfaces()[0];
                serviceCollection.AddScoped(serviceType, queryHandler);
            }
        }

        private static void RegisterQueryHandlers(IServiceCollection serviceCollection)
        {
            var queryHandlers = typeof(IQuery).Assembly.GetTypes().Where(t =>
                t.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

            foreach (var queryHandler in queryHandlers)
            {
                var serviceType = queryHandler.GetInterfaces()[0];
                serviceCollection.AddScoped(serviceType, queryHandler);
            }
        }

        public static IServiceCollection AddJsonOptions(this IServiceCollection serviceCollection)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            serviceCollection.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = contractResolver;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            return serviceCollection;
        }
    }
}
