using ChildBehaviour.BLL.Abstracts.DecisionTable;
using ChildBehaviour.BLL.Abstracts.Repositories;
using ChildBehaviour.BLL.DecisionTables;
using ChildBehaviour.DAL.Context;
using ChildBehaviour.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildBehaviour.RESTAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDbContexts(services);
            RegisterRepositories(services);
            RegisterServices(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChildBehaviour.RESTAPI", Version = "v1" });
            });
        }
        private void RegisterDbContexts(IServiceCollection services)
        {
            services.AddDbContext<ChildBehaviourContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ChildBehaviour")));
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IBehaviourRespository, BehaviourRepository>();
            services.AddScoped<IPupilRespository, PupilRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISymptomsRepository, SymptomsRepository>();
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IDecisionTable, DecisionTable>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChildBehaviour.RESTAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
