using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandLauncher.Models;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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

        public IActionResult Launcher()
        {
            var commandList = GetCommandList();

            List<SelectListItem> Commands = commandList
                .Select(command => new SelectListItem
                {
                    Value = commandList.IndexOf(command).ToString() ,
                    Text = command.CommandName
                }).ToList();

            return View(Commands);
        }

        public /*async Task<*/IActionResult/*>*/ Launch(int Command)
        {
            List<Command> CommandList = GetCommandList();

            Process proc = new Process(); //Declare le nouveau process
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = CommandList[Command].CommandFile;
            proc.StartInfo.Arguments = CommandList[Command].CommandArgs;
            //proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();

            string output = proc.StandardOutput.ReadToEnd();

            return Json(new { output });
        }

        private static List<Command> GetCommandList()
        {
            var fileContent = "wwwroot/commands/list.json";

            var jsonContent = System.IO.File.ReadAllText(fileContent);

            List<Command> CommandList = JsonConvert.DeserializeObject<List<Command>>(jsonContent);
            return CommandList;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Command
    {
        public string CommandFile { get; set; }
        public string CommandArgs { get; set; }
        public string CommandName { get; set; }
    }
}
