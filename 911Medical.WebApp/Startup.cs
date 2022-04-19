using _911Medical.Application.Common;
using _911Medical.Persistance.Common;
using _911Medical.WebApp.Data;
using AspNet.Security.OpenIdConnect.Primitives;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace _911Medical.WebApp
{
    public class Startup
    {

        private readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(RandomNumberGenerator.GetBytes(16));
        private readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        //.SetIsOriginAllowed(origin => true);
                        ;
                });
            });


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict();
            });

            services.AddSignalR(options=> {
                options.EnableDetailedErrors = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SignalHub", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //options.Events = new JwtBearerEvents()
                    //{
                    //    OnMessageReceived = ctx => { return Task.CompletedTask; },
                    //    OnTokenValidated = ctx => { return Task.CompletedTask; },
                    //    OnAuthenticationFailed = ctx => { return Task.CompletedTask; },
                    //    OnChallenge = ctx => { return Task.CompletedTask; },
                    //    OnForbidden = ctx => { return Task.CompletedTask; },
                    //};
                    options.RequireHttpsMetadata = false;
                    options.IncludeErrorDetails = true;
                    options.Authority = "https://localhost:44387";
                    options.Audience = "911Medical_Api";
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        NameClaimType = OpenIddictConstants.Claims.Name,
                        RoleClaimType = OpenIddictConstants.Claims.Role,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        RequireAudience = false,
                        RequireSignedTokens = false,
                        ValidateTokenReplay = false
                    };
                });



            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddOpenIddict()
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and models.
                    // Note: call ReplaceDefaultEntities() to replace the default entities.
                    options
                        .UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>();
                })
                .AddServer(options =>
                {
                    // Enable the token endpoint.
                    options
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetTokenEndpointUris("/connect/token");

                    // Enable the client credentials flow.
                    options.AllowClientCredentialsFlow();

                    // Enable the password flow.
                    options.AllowPasswordFlow();
                    options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();

                    options.RegisterScopes(new[] { "api" });

                    // Register the signing and encryption credentials.
                    options
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate()
                        .DisableAccessTokenEncryption();

                    // Register the ASP.NET Core host and configure the ASP.NET Core options.
                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough()
                           .EnableAuthorizationEndpointPassthrough();


                })
                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();
                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

            // ASP.NET Core Identity should use the same claim names as OpenIddict
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });



            // Register persistance and application services
            services.AddPersistance(Configuration.GetConnectionString("DefaultConnection"));
            services.AddApplication();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddFluentValidation();

            //services.AddRazorPages();
            services.AddMvc().AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
            }).AddFluentValidation();

            //services.AddSignalR();

            services.AddHostedService<AppCreator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors("AnyOrigin");


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Microsoft.AspNetCore.Authentication.AuthenticationTokenExtensions.
            //app.UseJwtBearerAuthentication()
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(x =>
            {
                var provider = app.ApplicationServices.GetService<IAuthenticationSchemeProvider>();

                var schemes = provider.GetAllSchemesAsync().Result;

                foreach (var scheme in schemes)
                {
                    System.Diagnostics.Debug.WriteLine($"Scheme {scheme.Name}");
                }

                return x;
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<Hubs.VehicleHub>("/vehiclehub");//.RequireAuthorization();
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            
        }
    }
}
