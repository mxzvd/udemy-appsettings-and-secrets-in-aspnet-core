using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppSettingsManager.Models;
using Microsoft.Extensions.Options;

namespace AppSettingsManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IConfiguration configuration;
	private TwilioOptions twilioOptions;
	private readonly IOptions<TwilioOptions> twilioOptions2;
	private IOptions<SocialLoginOptions> socialLoginOptions;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, TwilioOptions twilioOptions, IOptions<TwilioOptions> twilioOptions2, IOptions<SocialLoginOptions> socialLoginOptions)
    {
        _logger = logger;
		this.configuration = configuration;
		// twilioOptions = new TwilioOptions();
		// configuration.GetSection("Twilio").Bind(twilioOptions);
		this.twilioOptions = twilioOptions;
		this.twilioOptions2 = twilioOptions2;
		this.socialLoginOptions = socialLoginOptions;
    }

    public IActionResult Index()
    {
        ViewBag.SendGridKey = configuration.GetValue<string>("SendGridKey");
		// ViewBag.TwilioAuthToken = configuration.GetSection("Twilio").GetValue<string>("AuthToken");
		// ViewBag.TwilioAccountSid = configuration.GetValue<string>("Twilio:AccountSid");
		ViewBag.TwilioAuthToken = configuration.GetSection("Twilio").GetValue<string>("AuthToken");
		ViewBag.TwilioAccountSid = configuration.GetValue<string>("Twilio:AccountSid");

		ViewBag.Assignment1Method1 = configuration.GetValue<string>("FirstLevelSetting:SecondLevelSetting:BottomLevelSetting");
		ViewBag.Assignment1Method2 = configuration.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting").GetValue<string>("BottomLevelSetting");
		ViewBag.Assignment1Method3 = configuration.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting").GetSection("BottomLevelSetting").Value;
		// ViewBag.TwilioPhoneNumber = twilioOptions.PhoneNumber;

		// IOptions
		// ViewBag.TwilioAuthToken = twilioOptions2.Value.AuthToken;
		// ViewBag.TwilioAccountSid = twilioOptions2.Value.AccountSid;
		// ViewBag.TwilioPhoneNumber = twilioOptions2.Value.PhoneNumber;

		ViewBag.TwilioAuthToken = twilioOptions.AuthToken;
		ViewBag.TwilioAccountSid = twilioOptions.AccountSid;
		ViewBag.TwilioPhoneNumber = twilioOptions.PhoneNumber;

		ViewBag.FacebookKey = socialLoginOptions.Value.FacebookSettings.Key;
		ViewBag.GoogleKey = socialLoginOptions.Value.GoogleSettings.Key;

		ViewBag.ConnectionString = configuration.GetConnectionString("AppSettingsManagerDb");

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
