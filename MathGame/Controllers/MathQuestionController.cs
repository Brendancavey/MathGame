using Microsoft.AspNetCore.Mvc;
using MathGame.Models;
using System.Security.Cryptography.X509Certificates;

namespace MathGame.Controllers
{
    public class MathQuestionController : Controller
    {
        public IActionResult Random()
        {
            var math_question = new MathQuestion();
            var user = new User();

            var viewModel = new QuestionModel()
            {
                question = math_question,
                user = user
            };
            
            return View(viewModel);
        }

        [HttpPost]

        public ActionResult Random(QuestionModel question)
        {
            question.user = new User();
            question.question = new MathQuestion();
            question.user.score = 1;
            return View(question);
        }

    }
}
