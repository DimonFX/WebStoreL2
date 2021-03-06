﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Values;
using WebStore.Domain.Entities.Identity;
using WebStore.Hubs;
using WebStore.Infrastruction.Middleware;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.Services;
using WebStore.Services.Products.InCookies;
using WebStoreLogger;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreDB>(opt =>
            //    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<WebStoreDBInitializer>();

            services.AddSignalR();

            services.AddIdentity<User, Role>()
               //.AddEntityFrameworkStores<WebStoreDB>()
               .AddDefaultTokenProviders();

            #region WebAPI Identity clients Service
            services
                .AddTransient<IUserStore<User>, UsersClient>()
                .AddTransient<IUserPasswordStore<User>, UsersClient>()
                .AddTransient<IUserEmailStore<User>, UsersClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTransient<IUserLockoutStore<User>, UsersClient>()
                .AddTransient<IUserClaimStore<User>, UsersClient>()
                .AddTransient<IUserLoginStore<User>, UsersClient>();
            services
               .AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCD...123457890";
                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";
                //Необходимо менять идентификатор сеанса, когда пользователь был не залогинин и пора залогиниться, это повышает безовасность
                opt.SlidingExpiration = true;
            });
            /*
             * Добавляем все контроллеры
             * Это требование ASP NET CORE 3.1
             * + в версии 3.1 убрали автоматическую компиляцию представлений, поэтому мы ее доавбляем - 
             *  - нужно что бы можно  было править представление и после этого сразу могли видеть изменения!!!
             */
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddRazorPages();

            //Регистрируем наши собственные сервисы
            /*
             * AddTransient - каждый раз будет создаваться экземпляр сервиса. Предпочтителен для многопоточного режима
             * AddScoped - один экземпляр на область видимости
             * AddSingleton - один объект на все время жизни проекта, не рекомендуется для многопоточной рабюоты - возникнут проблемы
             */

            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<ICartStore, CookiesCartStore>();
            services.AddScoped<ICartService, WebStore.Services.Products.CartService>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddScoped<IValueServices, ValuesClient>();
            services.AddScoped<IMyTestService, MyTestClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            //db.Initialize();

            log.AddLog4Net();

            if (env.IsDevelopment())
            {
                //Отражает подробности об ошибках
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                //добавляет связь с браузером, что бы была возможность автоматом обновлять во всех браузерах (даже различных) наши веб страницы
                app.UseBrowserLink();
            }

            app.UseBlazorFrameworkFiles();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<InformationHub>("/info");

                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("blazor.html");

                //endpoints.MapGet("/greetings", async context =>
                //{
                //    await context.Response.WriteAsync(Configuration["CustomGreetings"]);
                //});

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
