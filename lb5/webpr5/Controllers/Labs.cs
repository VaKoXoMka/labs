using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleMvcApp.Controllers
{
    public class Lab : Controller
    {

        [Authorize]
        public IActionResult Lb1()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void Lb1(string input_file, string output_file)
            => webLib.Lb1.Pr1(input_file, output_file);

        [Authorize]
        public IActionResult Lb2()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void Lb2(string input_file, string output_file)
            => webLib.Lb2.Pr2(input_file, output_file);

        [Authorize]
        public IActionResult Lb3()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void Pr3(string input_file, string output_file)
             => webLib.Lb3.Pr3(input_file, output_file);
    }
}
