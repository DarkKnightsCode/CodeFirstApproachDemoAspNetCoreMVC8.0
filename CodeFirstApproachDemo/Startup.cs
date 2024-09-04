using CodeFirstApproachDemo.Models;
using CodeFirstApproachDemo.Repository;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    { 
        services.AddControllersWithViews();
        services.AddSession();
        var provider = services.BuildServiceProvider();
        var config = provider.GetService<IConfiguration>();
        services.AddDbContext<CompanyDbContext>(x => x.UseSqlServer(config.GetConnectionString("DatabaseConnectionString")));
        services.AddScoped(typeof(IGenericRepository< >), typeof(GenericRepository<>));
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddControllers();
    }

    public void Configure(WebApplication app, IHostEnvironment env) 
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseSession();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Employee}/{action=Index}/{id?}");

        app.Run();
    }
}
