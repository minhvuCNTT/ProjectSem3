using Microsoft.EntityFrameworkCore;
using Pro3MVC.Middlewares;
using Pro3MVC.Models;
using Pro3MVC.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//connect db
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"].ToString();
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//add service
builder.Services.AddScoped<IAccountService, AccountServiceImplement>();
builder.Services.AddScoped<ICourseService, CourseServiceImplement>();
builder.Services.AddScoped<IBatchService, BatchServiceImplement>();
builder.Services.AddScoped<ISubjectService, SubjectServiceImplement>();
builder.Services.AddScoped<IEnquiryService, EnquiryServiceImplement>();
builder.Services.AddScoped<IAccSubService, AccSubServiceImplement>();
builder.Services.AddScoped<IInvoiceService, InvoiceServiceImplement>();
builder.Services.AddScoped<IFacultyService, FacultyServiceImplement>();
builder.Services.AddScoped<IStudentService, StudentServiceImplement>();


var app = builder.Build();

app.UseSession();

app.UseMiddleware<SecurityMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
    );

app.Run();
