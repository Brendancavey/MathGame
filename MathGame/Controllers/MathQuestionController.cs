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
            int scoreGenerator = 0;
            ///////////SCORE GENERATOR//////////////////
            if (user_answer == model.question.answer)
            {
                if (model.question.timeLeft >= 8)
                {
                    scoreGenerator = 3;
                }
                else if (model.question.timeLeft >= 5)
                {
                    scoreGenerator = 2;
                }
                else
                {
                    scoreGenerator = 1;
                }
                if (model.user.scoreStreak > 1)
                {
                    scoreGenerator = scoreGenerator * 2;
                }
                model.user.correctQuestionsAnswered += 1;
                model.user.scoreStreak += 1;
                model.user.score += scoreGenerator;
                //Checking for new personal high streak score
                if (model.user.scoreStreak > model.user.highestSessionStreak)
                {
                    model.user.highestSessionStreak = model.user.scoreStreak;
                }
            } 
            else if (model.user.score > 0)
            {
                model.user.score -= 1;
                model.user.scoreStreak = 0;
            }
            model.user.totalQuestionsAnswered += 1;
            ///////////DIFFICULTY MODIFIER///////////////
            if (model.user.score % 5 == 0 && model.user.score > 0)
            {
                model.question.difficulty += model.question.difficulty;
            }
            //////////INSTANTIATING NEW MODELS/////////////
            var question = new MathQuestion()
            {
                num1 = (rand.Next(1, 11 * model.question.difficulty)) * (model.question.difficulty),
                num2 = (rand.Next(1, 11 * model.question.difficulty)) * (model.question.difficulty),
                difficulty = model.question.difficulty,
            };
            question.getNewAnswer(); //calculates new answer from newly generated numbers

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
                player.sessionScore = model.user.score;
                player.highestSessionStreak = model.user.highestSessionStreak;
                //Setting new high score
                if (model.user.score > player.score)
                {
                    player.name = model.user.name;
                    player.score = model.user.score;
                }
                if (model.user.highestSessionStreak > player.highestStreak)
                {
                    player.highestStreak = model.user.highestSessionStreak;
                }
                await mathGameDbContext.SaveChangesAsync();
            }
            
            var viewModel = new ScoreboardModel()
            {
                user = player,
                allUsers = await mathGameDbContext.users.OrderByDescending(u => u.score).ToListAsync()
            };
            return View(viewModel);
        }
    }
}
