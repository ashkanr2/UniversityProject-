using Microsoft.EntityFrameworkCore;
using UniversityProject.Interfaces;
using UniversityProject.Services;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using UniversityProject.Infrastructures;
using Microsoft.AspNetCore.Identity;
using UniversityProject.Entities;
using UniversityProject.Models.EmailSetting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAuthorization();

// Register UniversityDBContext with the DI container
builder.Services.AddDbContext<UniversityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthorization();
// Register your services
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<IUserCourseService , UserCourseService>();
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
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials
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
