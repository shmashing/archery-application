using System;
using System.Threading.Tasks;
using ArcheryTracker.App.Util;
using ArcheryTracker.Logic;
using ArcheryTracker.Logic.Config;
using ArcheryTracker.Logic.Database;
using ArcheryTracker.Logic.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace ArcheryTracker.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var auth0Config = new Auth0Config()
            {
                Domain = $"https://{Configuration["Auth0:Domain"]}",
                ClientId = Configuration["Auth0:ClientId"],
                ClientSecret = Environment.GetEnvironmentVariable("Auth0ClientSecret")
            };
            
            // Register ASP.NET and Blazor Components
            services.AddRazorPages();
            services.AddServerSideBlazor();
            
            // Register Database Connection
            var connectionString = ParseConnection(Environment.GetEnvironmentVariable("DATABASE_URL"));
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ArcheryTracker.App"));
            });
            
            // Register Data Repositories
            services.AddTransient<UserRepository>();
            services.AddTransient<SessionRepository>();
            services.AddTransient<RoundRepository>();
            
            // Register Services
            services.AddTransient<SessionService>();
            services.AddTransient<UserService>();

            // Register Utilities and Helpers
            services.AddScoped<IdentityService>();
            
            // Configure Auth0
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.Secure = CookieSecurePolicy.SameAsRequest;
            });

            services.AddExceptionHandler(options =>
            {
                options.ExceptionHandlingPath = "/error";
            });
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect("Auth0", options =>
                {
                    // Set the authority to your Auth0 domain
                    options.Authority = auth0Config.Domain;

                    // Configure the Auth0 Client ID and Client Secret
                    options.ClientId = auth0Config.ClientId;
                    options.ClientSecret =  auth0Config.ClientSecret;
                    
                    // Set response type to code
                    options.ResponseType = OpenIdConnectResponseType.Code;

                    // Configure the scope
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                    options.CallbackPath = new PathString("/callback");

                    // Configure the Claims Issuer to be Auth0
                    options.ClaimsIssuer = "Auth0";

                    options.Events = new OpenIdConnectEvents
                    {
                        // handle the logout redirection
                        OnRedirectToIdentityProviderForSignOut = (context) =>
                        {
                            var logoutUri =
                                $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                            var postLogoutUri = context.Properties.RedirectUri;
                            if (!string.IsNullOrEmpty(postLogoutUri))
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    // transform to absolute
                                    var request = context.Request;
                                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase +
                                                    postLogoutUri;
                                }

                                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                            }

                            context.Response.Redirect(logoutUri);
                            context.HandleResponse();
                            
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            app.UseHttpsRedirection();
        }
        
        private string ParseConnection(string connectionUri)
        {
            // Get connection string for local development
            if (string.IsNullOrEmpty(connectionUri))
            {
                return Configuration.GetConnectionString("ArcheryDatabase");
            }
            var uri = new Uri(connectionUri);
            var database = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var password = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;
            var connectionString = $"Server={uri.Host};Database={database};User Id={user};Password={password};Port={port}";
            return connectionString;
        }
    }
}
