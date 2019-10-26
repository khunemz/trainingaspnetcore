using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IGreeter, Greeter>();
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

            app.UseMvcWithDefaultRoute();
        }
    }
}