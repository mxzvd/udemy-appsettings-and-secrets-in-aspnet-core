using AppSettingsManager;
using AppSettingsManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddConfiguration<TwilioOptions>(builder.Configuration, "Twilio");
builder.Services.Configure<SocialLoginOptions>(builder.Configuration.GetSection("SocialLoginSettings"));
builder.Services.Configure<TwilioOptions>(builder.Configuration.GetSection("Twilio"));

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
