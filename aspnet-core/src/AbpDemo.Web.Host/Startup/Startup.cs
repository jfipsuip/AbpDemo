using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using AbpDemo.Configuration;
using AbpDemo.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
//using AbpDemo.WebCore.Authentication.BaseManager;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;

//#if FEATURE_SIGNALR
//using Microsoft.AspNet.SignalR;
//using Microsoft.Owin.Cors;
//using Owin;
//using Abp.Owin;
//using zyGIS.Owin;
//#elif FEATURE_SIGNALR_ASPNETCORE
//using Abp.AspNetCore.SignalR.Hubs;
//#endif

namespace AbpDemo.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        private readonly string _webRootPath;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            _webRootPath = env.WebRootPath;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //服务了现客户端
            //services.AddDiscoveryClient(_appConfiguration);

            //服务发现的Handler
            //services.AddTransient<DiscoveryHttpMessageHandler>();

            //
            services.AddHttpClient();

            // MVC
            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            );

            //IdentityRegistrar.Register(services);

            #region
            //start add by jjie   LogInManager  IUserClaimsPrincipalFactory
            ////services.AddLogging();
            ////services.AddScoped<LogInManager>();

            ////AbpIdentityBuilder identityBuilder = new AbpIdentityBuilder(services.AddIdentity<UserInfo, RoleInfo>(null), typeof(Tenant));
            ////var type = typeof(LogInManager);
            ////identityBuilder.Services.AddScoped<LogInManager>();
            ////identityBuilder.Services.AddScoped(typeof(LogInManager));

            //var identityBuilder = IdentityRegistrar.AddIdentity<UserInfo, RoleInfo>(services, options =>
            //{
            //    //options.Cookies.ApplicationCookie.AuthenticationScheme = "ApplicationCookie";
            //    //options.Cookies.ApplicationCookie.CookieName = "Interop";
            //});
            //identityBuilder.Services.AddScoped<LogInManager>();
            //identityBuilder.AddDefaultTokenProviders();

            //services.AddScoped<LogInManager>();
            //end
            #endregion

            //AuthConfigurer.Configure(services, _appConfiguration);

            //#if FEATURE_SIGNALR_ASPNETCORE
            //            services.AddSignalR();
            //#endif

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        //.WithOrigins(
                        //     //App: CorsOrigins in appsettings.json can contain more than one address separated by comma.
                        //    _appConfiguration["App:CorsOrigins"]
                        //        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        //        .Select(o => o.RemovePostFix("/"))
                        //        .ToArray()
                        //)
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                )
            );

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "zyGIS API", Version = "v1" });
                options.SwaggerDoc("用户权限", new Info { Title = "用户权限 API", Version = "用户权限" });
                options.DocInclusionPredicate((docName, description) =>
                {
                    if (description.GroupName != docName)
                    {
                        // GroupName = "用户权限" docName = "v1"
                        if (!new string[] { "用户权限" }.Contains(description.GroupName) && docName == "v1")
                        {
                        }
                        else
                        {
                            return false;
                        }
                    }
                    var classInfo = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)description.ActionDescriptor);
                    var attributes = classInfo.MethodInfo.CustomAttributes;
                    if (true)
                    //if (attributes.Count(t => t.AttributeType == typeof(HiddenApiAttribute)) == 0)
                    {
                        if (new string[] { "CreateByBatch", "UpdateByBatch", "UpdateDeleteByBatch", "GetAll" }.Contains(classInfo.MethodInfo.Name))
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                //options.DocumentFilter<HiddenApiFilter>();
                //options.DocumentFilter<CustomDocumentFiliter>();
                // Assign scope requirements to operations based on AuthorizeAttribute
                //options.OperationFilter<SecurityRequirementsOperationFilter>();
                //获取项目指定路径下xml文件
                options.IncludeXmlComments(Path.Combine(_webRootPath, "swagger.xml"));
            });

            //注入AbpDemoDbContext
            //var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            services.AddDbContext<AbpDemoDbContext>(options => options.UseSqlServer(_appConfiguration["ConnectionStrings:Default"]));

            // Configure Abp and Dependency Injection
            return services.AddAbp<AbpDemoWebModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            //app.UseCors(); // Enable CORS!
            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            app.UseAuthentication();//声明支持

            //app.UseJwtTokenMiddleware(); //中间件

            app.UseAbpRequestLocalization();

            //app.UseContentRoot(Directory.GetCurrentDirectory())

            //app.UseDiscoveryClient();


#if FEATURE_SIGNALR
            // Integrate with OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#elif FEATURE_SIGNALR_ASPNETCORE
            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });
#endif

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            //Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                string url;
                if (_appConfiguration["App:ServerRootAddress"].IsNullOrEmpty())
                {
                    url = string.Empty;
                }
                else
                {
                    url = _appConfiguration["App:ServerRootAddress"] + "/swagger/";
                }
                //options.InjectOnCompleteJavaScript($"{url}ui/abp.js");
                //options.InjectOnCompleteJavaScript($"{url}ui/on-complete.js");
                //options.InjectOnCompleteJavaScript($"{url}ui/swagger.js"); // 加载中文包
                options.InjectStylesheet($"{url}ui/swagger.css"); // 加载中文包
                options.SwaggerEndpoint($"{url}v1/swagger.json", "Web API V1");
                options.SwaggerEndpoint($"{url}用户权限/swagger.json", "用户权限");
            }); // URL: /swagger
        }

        //#if FEATURE_SIGNALR
        //        private static void ConfigureOwinServices(IAppBuilder app)
        //        {
        //            app.Properties["host.AppName"] = "zyGIS";

        //            app.UseAbp();

        //            app.Map("/signalr", map =>
        //            {
        //                map.UseCors(CorsOptions.AllowAll);
        //                var hubConfiguration = new HubConfiguration
        //                {
        //                    EnableJSONP = true
        //                };
        //                map.RunSignalR(hubConfiguration);
        //            });
        //        }
        //#endif
    }
}