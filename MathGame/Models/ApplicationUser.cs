using Microsoft.AspNetCore.Identity;

namespace MathGame.Models
{
    public class ApplicationUser:IdentityUser
    {
        //public string Id { get; set; }
        public string name { get; set; }
        public int score { get; set; }

        public ApplicationUser()
        {
            
        }
    }
}
