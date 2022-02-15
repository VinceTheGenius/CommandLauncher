using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandLauncher.Models;

namespace CommandLauncher.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Launcher(string text)
        {
            return View();
        }

        public async Task<IActionResult> Launch(string Command)
        {
            var fileName = "C:/VincentTheGenius/afficher.txt";

            await System.IO.File.WriteAllTextAsync(fileName, Command);

            System.Diagnostics.Process proc = new System.Diagnostics.Process(); //Declare le nouveau process
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = Command;
            //proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();

            string output = proc.StandardOutput.ReadToEnd();

            return RedirectToAction("Launcher", new { text = output });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
