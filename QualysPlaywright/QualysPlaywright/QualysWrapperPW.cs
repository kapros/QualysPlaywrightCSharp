using Microsoft.Playwright;
using System.Threading.Tasks;

namespace QualysPlaywright
{
    public class QualysWrapperPW
    {
        private readonly IPage _page;

        public QualysWrapperPW(IBrowser browser)
        {
            _page = browser.NewPageAsync().Result;
        }

        public async Task OpenExtension()
        {
            await _page.GotoAsync("chrome-extension://abnnemjpaacaimkkepphpkaiomnafldi/panel/index.html");
        }

        public async Task StartCapture(string testCaseName)
        {
            _page.Dialog += async (object sender, IDialog dialog) => await dialog.AcceptAsync(testCaseName);
            var untitledTestCaseElement = await _page.QuerySelectorAsync("//*[@id='testCase-grid']//*[text()='Untitled Test Case']");
            await untitledTestCaseElement.ClickAsync(new ElementHandleClickOptions { Button = Microsoft.Playwright.MouseButton.Right });
            await _page.ClickAsync("//a[text()='Rename Test Case']");
            await ClickRecord();
        }

        public async Task AddWait()
        {
            // you could provide some logic to insert waiting between steps statically,
            // or alternatively use driver events to insert these commands or overwrite the edited ones instead to make the script less brittle
            // obviously if the tests have to take into account that the driver could have qualys attached or not is not a good idea
            // so it would be up to you to figure it out,
            // if you have many loaders you could just use a post click event to insert waiting for the loader to appear and disappear, same with navigation
        }

        public async Task StopCapture(string fileName)
        {
            await ClickRecord();
            await _page.ClickAsync("#download");
            _page.Dialog += async (object sender, IDialog dialog) => await dialog.AcceptAsync(fileName);
        }

        private async Task ClickRecord() =>
            await _page.ClickAsync("#record");
    }
}
