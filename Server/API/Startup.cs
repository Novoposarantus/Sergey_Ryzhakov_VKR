﻿using Domain.Context;
using Domain.Interfaces;
using Domain.Repositories;
using API.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace API
{
	public class Startup
	{
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository>(provider =>
                new UserRepository(Configuration.GetConnectionString("DefaultConnection"),
                provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<ITestRepository>(provider =>
                new TestRepository(Configuration.GetConnectionString("DefaultConnection"),
                provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<IQuestionRepository>(provider =>
                new QuestionRepository(Configuration.GetConnectionString("DefaultConnection"),
                provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<IAssignmentsRepository>(provider =>
                new AssignmentsRepository(Configuration.GetConnectionString("DefaultConnection"),
                provider.GetService<IRepositoryContextFactory>()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthenticationOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthenticationOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (path.StartsWithSegments("/chat")))
                                {
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes
                .MapRoute(
                    name: "api",
                    template: "api/{controller}/{action}/{id?}");
            });
        }
	}
}
