namespace Kwikemark
{
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Text;
    using Kwikemark.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
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

            // ===== Add our DbContext ========
            services.AddDbContext<DbKwikemartContext>();

            // ===== Add Identity ========
            services.AddIdentity<Entities.User, Entities.Role>()
                .AddEntityFrameworkStores<DbKwikemartContext>()
                .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            // services
            //     .AddAuthentication(options =>
            //     {
            //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //     })
            //     .AddJwtBearer(cfg =>
            //     {
            //         cfg.RequireHttpsMetadata = false;
            //         cfg.SaveToken = true;
            //         cfg.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
            //             ValidAudience = Configuration["JwtSecurityToken:Audience"],
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityToken:Key"])),
            //             ValidateIssuerSigningKey = true,
            //             ValidateLifetime = true
            //             //ClockSkew = TimeSpan.Zero //remove delay of token when expire
            //         };
            //     });

            // ===== Add MVC ========
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            DbKwikemartContext dbContext
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            // ===== Use Authentication ======
            app.UseAuthentication();
            app.UseMvc();

            app.Use(async (context, next) => {
            await next();
            if (context.Response.StatusCode == 404 &&
                !Path.HasExtension(context.Request.Path.Value) &&
                !context.Request.Path.Value.StartsWith("/api/")) {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // ===== Create tables ======
            dbContext.Database.EnsureCreated();
        }
    }
}   
