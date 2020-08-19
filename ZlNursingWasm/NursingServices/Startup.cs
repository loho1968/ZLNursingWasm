using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NursingBLL.Cache;
using NursingModel.TaskModels;
using NursingModel;
using Quartz;
using Quartz.Impl;
using NursingCommon;
using log4net;
using log4net.Config;
using System.IO;

namespace NursingServices
{
    public class Startup
    {
        private string ApiName = "护理记录单";
        string basePath = AppContext.BaseDirectory;
        public Startup(IConfiguration configuration)
        {
            //log4net
            LoggerHelper.Repository = LogManager.CreateRepository("NursingServices");//需要获取日志的仓库名，项目名

            //指定配置文件，如果这里你遇到问题，应该是使用了InProcess模式，请查看Blog.Core.csproj,并删之 
            XmlConfigurator.Configure(LoggerHelper.Repository, new FileInfo("Log4net.config"));//配置文件
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //.AddControllersWithViews().AddRazorRuntimeCompilation()
            services.AddControllersWithViews().AddNewtonsoftJson(o =>
            {
                //按照类定义大小写输出json
                o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //json时间字段格式化
                o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddRazorRuntimeCompilation();


            //缓存
            services.AddMemoryCache();

            services.AddSession();
            //注册任务-单例
            services.TryAddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //日志及错误处理
            services.AddMvc(options =>
            {
                options.Filters.Add(new ActionFilter());
                options.Filters.Add(new ExceptionFilter());
            });

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //1允许一个或多个来源可以跨域
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    // 设定允许跨域的来源，有多个可以用','隔开
                    policy.WithOrigins()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            /*services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "ZLRPTS API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                //// Define the BearerAuth scheme that's in use
                //options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});
            });*/

            //注册swagger服务
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "NursingServices",
                    Version = "v1"
                });
            });

            //JWT验证（token）
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,//是否验证Issuer
                        ValidateAudience = false,//是否验证Audience
                        ValidateLifetime = false,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = "zlapex",//Audience
                        ValidIssuer = "zlsoft",//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//拿到SecurityKey
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(context.HttpContext.Response.Body))
                            {
                                sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = 401,
                                    message = "Rejects The Request!",//中文会乱码
                                }));
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //注入全局对象
            DIServicesCollection.Instance = app.ApplicationServices;


            var obj = Configuration.GetSection("DataConnect").Get<List<DataConnect>>();
            SiteConfig.SetConnectObj(obj);
            SiteConfig.StandardAPI = bool.Parse(Configuration["StandardAPI"]);

            //加载缓存
            InitCache.Init();

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

            app.UseRouting();

            app.UseAuthorization();

            //启用缓存
            app.UseCookiePolicy();
            app.UseSession();

            //跨域
            app.UseCors("CustomCorsPolicy");
            //
            //app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)

            //引用swagger服务
            app.UseSwagger();
            app.UseSwaggerUI(
                m =>
                {
                    m.SwaggerEndpoint("/swagger/v1/swagger.json", "NursingServices");
                }
             );

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("localhost:5001/" + "swagger/v1/swagger.json", "ZLRPTS API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("NursingService.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger

            //注入
            DIServicesCollection.Instance = app.ApplicationServices;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller==Home}/{action=Index}/{id?}");
            });
        }
    }
}
