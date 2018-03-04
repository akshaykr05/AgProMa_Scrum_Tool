using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AgpromaWebAPI.model;

using Microsoft.EntityFrameworkCore;
using Serilog;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Service;

namespace AgpromaWebAPI
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AgpromaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IProjectMasterRepo, ProjectMasterRepo>();
            //services.AddScoped<IProjectMasterService, ProjectMasterService>();
            //services.AddScoped<IProjectmembersRepository, ProjectmembersRepository>();
            //services.AddScoped<IProjectmemberservice, Projectmemberservice>();
            //services.AddSignalR();
            //services.AddScoped<IforgetPassword, forgetPassword>();
            //services.AddScoped<IMasterRepository, MasterRepository>();
            //services.AddScoped<IMasterService, MasterService>();
            //services.AddScoped<ISignUpService, SignUpService>();
            //services.AddScoped<ISignUpRepository, SignUpRepository>();
            //services.AddScoped<IinviteMembersService, InviteMembersService>();
            //services.AddScoped<IInviteRepository, InviteRepository>();
            //services.AddScoped<IProjectmemberservice, Projectmemberservice>();
            //services.AddScoped<IProjectmembersRepository, ProjectmembersRepository>();
            //services.AddScoped<ITeamRepo, TeamRepo>();
            //services.AddScoped<ITeamService, TeamService>();
            //services.AddScoped<IEpicServices, EpicService>();
            //services.AddScoped<IEpicRepository, EpicRepository>();
            //services.AddScoped<IBacklogServices, BacklogService>();
            //services.AddScoped<IBacklogRepository, BacklogRepository>();
            //services.AddScoped<IReleasePlanRepo, ReleasePlanRepo>();
            //services.AddScoped<IReleasePlanService, ReleasePlanService>();
            //services.AddScoped<ISprintRepository, SprintRepository>();
            //services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<ICheckListRepository, ChecklistRepository>();
            services.AddScoped<ICheckListService, ChecklistService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskServices, TaskService>();
          //  services.AddScoped<ITaskBacklogReposiory, TaskBacklogRepository>();
           // services.AddScoped<ITaskBacklogService, TaskBacklogService>();
           // services.AddScoped<IEfficiencyService, EfficiencyService>();
            //services.AddScoped<IEfficiencyRepository, EfficiencyRepository>();
          //  services.AddScoped<IBurndownService, BurndownService>();
            //services.AddScoped<IBurndownRepository, BurndownRepository>();
            services.AddSingleton(Configuration);
            // Add framework services.
            ConfigureJwtAuthService(Configuration, services);
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
           // app.UseSignalR(routes =>
            //{
                //routes.MapHub<EpicHub>("epichub");
                //routes.MapHub<SprintBacklogHub>("sprint");
                //routes.MapHub<ReleasePlanHub>("releaseplan");
                //routes.MapHub<TeamHub>("teamhub");
                //routes.MapHub<TaskBacklogHub>("taskbacklog");
                //routes.MapHub<BacklogHub>("backlog");
                //routes.MapHub<TaskHub>("taskhub");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
