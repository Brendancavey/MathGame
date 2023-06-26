using Microsoft.AspNetCore.Mvc;
using MathGame.Models;
using System.Security.Cryptography.X509Certificates;
using MathGame.Data;
using MathGame.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MathGame.Controllers
{
    public class MathQuestionController : Controller
    {
        private readonly MathGameDbContext mathGameDbContext;
        public MathQuestionController(MathGameDbContext mathGameDbContext)
        {
            this.mathGameDbContext = mathGameDbContext;
        }

        [HttpGet]
        public IActionResult Random()
        {
            var math_question = new MathQuestion();
            
            var user = new User()
            {
                name = "Bob",
                score = 0
            };
            var viewModel = new QuestionModel()
            {
                question = math_question,
                user = user,
            };
            //mathGameDbContext.users.Add(user);
            //mathGameDbContext.SaveChanges();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Random(QuestionModel model)
        {
            var user_answer = model.question.user_answer;
            if (user_answer == model.question.answer)
            {
                model.user.score += 1;
            }
            var question = new MathQuestion();

            var user = model.user;

            var viewModel = new QuestionModel()
            {
                question = question,
                user = user
            };

            mathGameDbContext.mathQuestions.Add(model.question);
            mathGameDbContext.SaveChanges();

            ModelState.Clear();
            return View(viewModel);
        }

    }
}
