﻿using Microsoft.AspNetCore.Mvc;
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
            Random rand = new Random();
            var math_question = new MathQuestion();
            
            var user = new ApplicationUser()
            {
                name = "Bob",
                score = 0
            };
            var viewModel = new QuestionModel()
            {
                question = math_question,
                user = user
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

            // mathGameDbContext.users.Add(user);
            // mathGameDbContext.SaveChanges();

            ModelState.Clear();
            return View(viewModel);
        }

    }
}
