using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lottery_Bets.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lottery_Bets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //初始化容器
        public static IContainer AutofacContainer;

        //运行时调用此方法，使用此方法向容器添加服务。
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //调用全局过滤器
            services.AddMvc(a =>
            {
                //全局异常
                a.Filters.Add<GlobalExceptionsFilter>();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            //创建注册组件的builder
            //第三方 Autofac 依赖注入
            var builder = new ContainerBuilder();

            //Assembly
            var serveiceDllService = Path.Combine(basePath, "Service.dll");
            var assemblysService = Assembly.LoadFile(serveiceDllService);
            builder.RegisterAssemblyTypes(assemblysService).AsImplementedInterfaces().InstancePerLifetimeScope();

            var serviceDllRepositroy = Path.Combine(basePath, "Repository.dll");
            var assmblysRepositroy = Assembly.LoadFile(serviceDllRepositroy);
            builder.RegisterAssemblyTypes(assmblysRepositroy).AsImplementedInterfaces().InstancePerLifetimeScope();

            //注入，填充过滤器
            builder.Populate(services);

            AutofacContainer = builder.Build();

            return new AutofacServiceProvider(AutofacContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //程序停止调用函数（释放）
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}
