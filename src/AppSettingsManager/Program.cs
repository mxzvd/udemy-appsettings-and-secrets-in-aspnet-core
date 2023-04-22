using AppSettingsManager;
using AppSettingsManager.Models;

var builder = WebApplication.CreateBuilder(args);


// Define a custom hierarchy of configuration settings
builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("custom.json", optional: true, reloadOnChange: true);
if (builder.Environment.IsDevelopment())
{
	builder.Configuration.AddUserSecrets<Program>();
}
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);


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
