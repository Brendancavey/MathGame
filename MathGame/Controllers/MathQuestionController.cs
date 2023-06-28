using Microsoft.AspNetCore.Mvc;
using MathGame.Models;
using System.Security.Cryptography.X509Certificates;
using MathGame.Data;
using MathGame.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using MathGame.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MathGame.Controllers
{
    public class MathQuestionController : Controller
    {
        private readonly MathGameDbContext mathGameDbContext;
        public MathQuestionController(MathGameDbContext mathGameDbContext)
        {
            this.mathGameDbContext = mathGameDbContext;
        }
        //[Area("Admin")]
        //[Authorize(Roles = StaticDetails.Role_Admin)]
        [HttpGet]
        public IActionResult Random()
        {
            var math_question = new MathQuestion();
            
            var user = new ApplicationUser()
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
                num1 = (rand.Next(1, 11 * model.question.difficulty)) * (model.question.difficulty),
                num2 = (rand.Next(1, 11 * model.question.difficulty)) * (model.question.difficulty),
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
        [HttpGet]
        public async Task<IActionResult> Scoreboard()
        {
            var viewModel = new ScoreboardModel()
            {
                user = null,
                allUsers = await mathGameDbContext.users.OrderByDescending(u => u.score).ToListAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Scoreboard(QuestionModel model)
        {
            var player = await mathGameDbContext.users.FindAsync(model.user.Id);
            if (player != null)
            {
                if (model.user.score > player.score)
                {
                    player.name = model.user.name;
                    player.score = model.user.score;
                    await mathGameDbContext.SaveChangesAsync();
                }
            }
            
            var viewModel = new ScoreboardModel()
            {
                user = model.user,
                allUsers = await mathGameDbContext.users.OrderByDescending(u => u.score).ToListAsync()
            };
            return View(viewModel);
        }
    }
}
