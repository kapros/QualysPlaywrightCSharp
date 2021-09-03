using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QualysPlaywright
{
    public class DriverFactory
    {
        public async static Task<IBrowser> CreatePlaywrightBrowser(BrowserTypeLaunchOptions options = null)
        {
            var pw = await Playwright.CreateAsync();
            var launchOptions = new BrowserTypeLaunchOptions(options);
            ((List<string>)(launchOptions.Args ?? new List<string>())).Add("--load-extension=qualys.crx");
            var chrome = await pw.Chromium.LaunchAsync(launchOptions);
            return chrome;
        }
    }
}