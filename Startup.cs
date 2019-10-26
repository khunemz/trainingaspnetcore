using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Entities;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json") // todo: add appsetting.json for production
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("OdeToFood")
                ));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env
            , ILoggerFactory loggerFactory, IGreeter greeter)
        {
            loggerFactory.AddConsole();
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Opps !")
                });

            //app.UseDefaultFiles(); // response index.html
            //app.UseStaticFiles();  // response index.html

            app.UseFileServer(); // response index.html

            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/welcome"
            //});

            //app.Run(async context =>
            //{
            //    //throw  new Exception("Something went wrong");
            //    //string message = Configuration["Greeting"];
            //    var message = greeter.GetGreeting();
            //    await context.Response.WriteAsync(string.Format("Hello {0}", message));
            //});

            app.UseMvc(configureRoutes);
            app.Run(ctx => ctx.Response.WriteAsync("Not found!!!"));
        }

        private void configureRoutes(IRouteBuilder routeBuilder)
        {
            // FORMAT : /Home/Index
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}