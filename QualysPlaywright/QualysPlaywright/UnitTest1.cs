using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace QualysPlaywright
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public async Task PlaywrightWithQualys()
        {
            var launchOpts = new BrowserTypeLaunchOptions { DownloadsPath = AppDomain.CurrentDomain.BaseDirectory };
            var qualys = new QualysWrapperPW(await DriverFactory.CreatePlaywrightBrowser(launchOpts));
            await qualys.OpenExtension();
            await qualys.StartCapture("test1");
            // do something here
            await qualys.StopCapture("test1_recording");
        }
    }
}