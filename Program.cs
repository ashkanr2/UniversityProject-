using Microsoft.EntityFrameworkCore;
using UniversityProject.Interfaces;
using UniversityProject.Services;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using UniversityProject.Infrastructures;
using Microsoft.AspNetCore.Identity;
using UniversityProject.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddAuthorization();

// Register UniversityDBContext with the DI container
builder.Services.AddDbContext<UniversityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthorization();
// Register your services
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddIdentity<User, IdentityRole<Guid>>(
    Options =>
    {
        Options.SignIn.RequireConfirmedAccount = false;
        Options.SignIn.RequireConfirmedEmail = false;
        Options.SignIn.RequireConfirmedPhoneNumber = false;
     
        Options.Password.RequireDigit = false;
        Options.Password.RequireLowercase = false;
        Options.Password.RequireNonAlphanumeric = false;
        Options.Password.RequireUppercase = false;
        Options.Password.RequiredLength = 5;
        Options.Password.RequiredUniqueChars = 0;


     

    })
    .AddEntityFrameworkStores<UniversityDBContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
