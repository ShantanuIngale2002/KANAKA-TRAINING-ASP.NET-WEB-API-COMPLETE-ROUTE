using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using PracticeForTelerickUI01.Repository;
using PracticeForTelerickUI01.Repository.EFCore;
using PracticeForTelerickUI01.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);



// Add Kendo UI services to the services container.
builder.Services.AddKendo();

// added this all as well
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout to 30 minutes
});
// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();
builder.Services.AddDbContext<BookDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnectionString") ?? throw new InvalidOperationException("Connection String not Found!!")
        )
    );

builder.Services.AddScoped<IBookRepository, BookService>();


//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

var app = builder.Build();

app.UseSession();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
