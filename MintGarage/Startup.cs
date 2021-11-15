using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MintGarage.Database;
using MintGarage.Models.Categories;
using MintGarage.Models.Products;
using MintGarage.Models.ConsultationForms;
using MintGarage.Models;
using MintGarage.Models.Accounts;
using MintGarage.Models.HomeTab.Contacts;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.SocialMedias;
using MintGarage.Models.HomeTab.Suppliers;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.Partners;
using MintGarage.Models.AboutUsTab.Teams;
using MintGarage.Models.AboutUsTab.Values;
using MintGarage.Models.GalleryTab;

namespace MintGarage
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
            // Adding session services to Razor Pages
            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();

            services.AddControllersWithViews();
            services.AddDbContext<MintGarageContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:MintGarageConnStr"]);
            });

            // Products
            //services.AddScoped<IMintGarageDBInitializer, MintGarageDBInitializer>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            // Customer Form
            services.AddScoped<IConsultationFormRepository, ConsultationFormRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //New
            services.AddScoped<IContactRepository, ContactsRepository>();
            services.AddScoped<IHomeContentRepository, HomeContentRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IFooterContactInfoRepository, FooterContactInfoRepository>();
            services.AddScoped<IFooterSocialMediaRepository, FooterSocialMediaRepository>();

            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IValueRepository, ValueRepository>();
            services.AddScoped<IRepository<Gallery>, GalleryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            /* if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }
             else
             {
                 app.UseExceptionHandler("/Home/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }*/
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<MintGarageContext>())
                context.Database.Migrate();
            //  mintGarageDBInitializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            InitialData.EnsurePopulated(app);
        }
    }
}
