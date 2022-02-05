using ASPSampleApplication.Core.Services;
using ASPSampleApplication.Data.Extensions;
using ASPSampleApplication.Data.Mapping;
using ASPSampleApplication.Data.Repositories;
using ASPSampleApplication.Data.Services;
using ASPSampleApplication.Web.Auth;
using ASPSampleApplication.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ASPSampleApplication.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EntryDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EntryConnectionString"));
            });

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            }).AddXmlSerializerFormatters();

            services
                .AddAuthentication(options => { options.DefaultChallengeScheme = SampleAuthOptions.DefaultSchemeName; })
                .AddScheme<SampleAuthOptions, SampleTokenAuthHandler>(SampleAuthOptions.DefaultSchemeName, options => { });

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder(SampleAuthOptions.DefaultSchemeName)
                    .RequireAssertion(c =>
                    {
                        if (c.Resource is HttpContext httpContext && httpContext.Request.Headers.ContainsKey(authOptions.HeaderName))
                        {
                            var authHeaderValue = httpContext.Request.Headers[authOptions.HeaderName];

                            return authOptions.Token == authHeaderValue;
                        }

                        return false;
                    })
                    .Build();

                options.DefaultPolicy = policy;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample API", Version = "v1" });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddRazorPages();
            services.AddAutoMapper(typeof(ArticleMappingProfile));

            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleSearchService, ArticleSearchService>();
            services.AddTransient<IEntryRepository, EntryRepository>();
            services.AddTransient<Func<IEntryRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetService<IEntryRepository>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EntryDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();

            app.UseDbTriggers();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Sample API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapSwagger();
            });
        }
    }
}
