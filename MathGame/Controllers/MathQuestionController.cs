using Microsoft.AspNetCore.Mvc;
using MathGame.Models;

namespace MathGame.Controllers
{
    public class MathQuestionController : Controller
    {
        public IActionResult Random()
        {
            var math_question = new MathQuestion();
            return View(math_question);
        }
    }
}
