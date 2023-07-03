using Microsoft.AspNetCore.Identity;

namespace MathGame.Models
{
    public class ApplicationUser:IdentityUser
    {
        //public string Id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public int sessionScore { get; set; }
        public int scoreStreak { get; set; }
        public int highestStreak { get; set; }
        public int highestSessionStreak { get; set; }
        public int totalQuestionsAnswered { get; set; }
        public int correctQuestionsAnswered { get; set; }

        public ApplicationUser()
        {
            
        }
    }
}
