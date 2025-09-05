using System.Diagnostics;
using System.Threading.Tasks;
using htmlToPdf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Net.Http.Headers;
using PuppeteerSharp;



namespace htmlToPdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRazorViewToStringRenderer _viewRenderer;

        public HomeController(ILogger<HomeController> logger, IRazorViewToStringRenderer viewRenderer)
        {
            _logger = logger;
            _viewRenderer= viewRenderer;
        }

        public IActionResult Index()
        {
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


        public IActionResult ViewElectronicCheck()
        {
            var obj = new ElectronicCheckViewModel(DateTime.UtcNow.Ticks.ToString(), DateTime.UtcNow.ToString("dd-MMM-yyyy"), "John Doe", "2436 Main Street",
                 "", "Springfield", "IL", "62704", "Two Thousand Three Hundred Dollors And 00 cents", "2300.00", "Floor repair Young");
            return View("~/Views/PdfTemplate/ElectronicCheck.cshtml", obj);
        }


        [HttpGet]
        public async Task<ActionResult> OpenElectronicCheckAsPDF()
        {
            var obj = new ElectronicCheckViewModel(DateTime.UtcNow.Ticks.ToString(), DateTime.UtcNow.ToString("dd-MMM-yyyy"),
                "John Doe", "2436 Main Street","", "Springfield", "IL", "62704", "Two Thousand Three Hundred Dollors And 00 cents",
                "2300.00", "Floor repair Young");

            // Render Razor View To String HTML 
            string html = await _viewRenderer.RenderViewToStringAsync("~/Views/PdfTemplate/ElectronicCheck.cshtml", obj);

            //Download chromium executable
            await new BrowserFetcher().DownloadAsync();
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdfStream = await page.PdfStreamAsync(new PdfOptions{
                Format = PuppeteerSharp.Media.PaperFormat.Letter,
                PrintBackground = false              
            });

            MemoryStream ms = new MemoryStream();
            pdfStream.CopyTo(ms);
          
            ms.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            var contentDisposition = new ContentDispositionHeaderValue("inline");
            contentDisposition.SetHttpFileName($"DummyPDF_{Guid.NewGuid()}.pdf");
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            return fileStreamResult;
        }
    }
}
