using Finbuckle.MultiTenant.Stores;
using HelpDesk.MultiTenant.Data;
using HelpDesk.MultiTenant.MultiTenant;
using HelpDesk.MultiTenant.Web.MultiTenant;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MultiTenant
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
            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<QuodyMutiTenantDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging(true);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddUserManager<AspNetUserManager<IdentityUser>>() 
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();
            
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
            services.AddAuthorization();

            services.AddMultiTenant()
                .WithHostStrategy("*.__tenant__.?")
                .WithFallbackStrategy(Configuration.GetValue<string>("MultiTenent:FullBackStrategry"))
                .WithStore<DummyMultiTenantStore>(ServiceLifetime.Transient)
                .WithPerTenantOptions<CookieAuthenticationOptions>((options, tenantInfo) =>
                {
                    options.Cookie.Name = $"{tenantInfo.Identifier}.auth.cookie";
                    options.Cookie.Domain = $"{tenantInfo.Identifier}.{Configuration.GetValue<string>("WebsiteMainDomain")}";

                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                });

            services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddScoped<CustomCookieAuthenticationEvents>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseMultiTenant();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            return base.ValidatePrincipal(context);
        }

        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToAccessDenied(context);
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToLogin(context);
        }

        public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToLogout(context);
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToReturnUrl(context);
        }

        public override Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);
        }

        public override Task SigningIn(CookieSigningInContext context)
        {
            return base.SigningIn(context);
        }

        public override Task SigningOut(CookieSigningOutContext context)
        {
            foreach (var cookieKey in _httpContextAccessor.HttpContext.Request.Cookies.Keys.Where(w => w.EndsWith(".auth.cookie")))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieKey);
            }
            
            return base.SigningOut(context);
        }
    }
}
