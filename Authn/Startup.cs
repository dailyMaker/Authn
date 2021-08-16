using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authn
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
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GoogleOpenID";
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";
                })
                .AddOpenIdConnect("GoogleOpenID", options =>
                {
                    options.Authority = "https://accounts.google.com";
                    options.ClientId = "gcp���� ������ Ŭ���̾�Ʈ ID�� �Է�";
                    options.ClientSecret = "Ŭ���̾�Ʈ ID�� ���ȹ�ȣ";
                    options.CallbackPath = "/auth";
                    options.SaveTokens = true;
                    options.Events = new OpenIdConnectEvents()
                    {
                        OnTokenValidated = async context =>
                        {
                            //var myclaim = context.Principal.Claims; // ���� �� ������ ��
                            if (context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "����ڽĺ���ȣ")
                            {
                                var claim = new Claim(ClaimTypes.Role, "Admin");
                                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                                claimsIdentity.AddClaim(claim);
                            }
                        }
                    };
                });

                //.AddGoogle(options =>
                //{
                //    options.ClientId = "37474395668-96i7406is845gr8ht4ffk42mkilrecpe.apps.googleusercontent.com";
                //    options.ClientSecret = "W6tNNmfQutuzLU6L1WFGaRCL";
                //    options.CallbackPath = "/auth";
                //    options.AuthorizationEndpoint += "?prompt=consent";
                //});

                //    option.Events = new CookieAuthenticationEvents()
                //    {
                //        OnSigningIn = async context =>
                //        {
                //            var principal = context.Principal;
                //            if (principal.HasClaim(c=>c.Type == ClaimTypes.NameIdentifier))
                //            {
                //                if(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "jbs")
                //                {
                //                    var claimsIdentity = principal.Identity as ClaimsIdentity;
                //                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                //                }
                //            }
                //            await Task.CompletedTask;
                //        },
                //        OnSignedIn = async context =>
                //        {
                //            await Task.CompletedTask;
                //        },
                //        OnValidatePrincipal = async context =>
                //        {
                //            await Task.CompletedTask;
                //        }
                //    };
                //});
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
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
