using DoAnCoSo.Models;
using DoAnCoSo.Repositories;
using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<ITruongRepository, EFTruongRepository>();
builder.Services.AddScoped<ILoaiTruongRepository, EFLoaiTruongRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IMonThiRepository, EFMonThiRepository>();
builder.Services.AddScoped<ICuocThiRepository, EFCuocThiRepository>();
builder.Services.AddScoped<IQuyDinhRepository, EFQuyDinhRepository>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();



builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=CuocThis}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=CuocThis}/{action=Index}/{id?}"
    );
});
app.Run();
