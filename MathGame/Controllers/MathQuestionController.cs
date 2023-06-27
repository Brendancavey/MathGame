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
            Random rand = new Random();
            var user_answer = model.question.user_answer;
            if (user_answer == model.question.answer)
            {
                model.user.score += 1;
            }
            else if (model.user.score > 0)
            {
                model.user.score -= 1;
            }
            if (model.user.score % 5 == 0 && model.user.score > 0)
            {
                model.question.difficulty += 1;
            }
            var question = new MathQuestion()
            {
                num1 = (rand.Next(1, 11)) * (model.question.difficulty),
                num2 = (rand.Next(1, 11)) * (model.question.difficulty),
                difficulty = model.question.difficulty,
            };
            question.getNewAnswer();

            var user = model.user;

            var viewModel = new QuestionModel()
            {
                question = question,
                user = user
            };

            //mathGameDbContext.mathQuestions.Add(model.question);
            //mathGameDbContext.SaveChanges();

            ModelState.Clear();
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Scoreboard(QuestionModel model)
        {
            User user = new User()
            {
                score = model.user.score
            };
            return View(user);
            
        }
    }
}
