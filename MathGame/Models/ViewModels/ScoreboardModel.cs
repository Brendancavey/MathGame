namespace MathGame.Models.ViewModels
{
    public class ScoreboardModel
    {
        public List<ApplicationUser> allUsers { get; set; }
        public ApplicationUser? user { get; set; }
    }
}
