using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// ✅ Register DbContext
builder.Services.AddDbContext<BloggieDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ✅ FIX: Register Interface → Implementation
builder.Services.AddScoped<ITagRepository, TagRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ✅ Optional: set your default page (your tags list)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminTags}/{action=List}/{id?}"
);

app.Run();