using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizForms.Data;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Data.Repositories.Implementations;
using System;

namespace QuizForms.Web
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
            //
            services.AddSingleton<IAccountsRepository, AccountsRepository>();
            services.AddSingleton<IContactMessagesRepository, ContactMessagesRepository>();
            services.AddSingleton<ILivestreamRepository, LivestreamRepository>();
            services.AddSingleton<IQuizFormAnswersRepository, QuizFormAnswersRepository>();
            services.AddSingleton<IQuizFormsRepository, QuizFormsRepository>();
            services.AddSingleton<IQuizFormsScoresRepostiory, QuizFormsScoresRepostiory>();
            services.AddSingleton<IScoreboardRepository, ScoreboardRepository>();            

            //
            services.Configure<QuizFormsSettings>(Configuration.GetSection("Settings"));

            //
            services.AddControllersWithViews();

            //
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties�.
                options.FormFieldName = "AntiforgeryToken";
                options.HeaderName = "X-CSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });

            //
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(o => {
                  o.LoginPath = new PathString("/admin/sign-in");
                  o.LogoutPath = new PathString("/admin/sign-out");
                  o.ExpireTimeSpan = new TimeSpan(90, 0, 0, 0);
                  o.ReturnUrlParameter = "redirect";
                  o.SlidingExpiration = true;
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
