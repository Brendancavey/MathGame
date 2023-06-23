using Microsoft.CodeAnalysis;

namespace MathGame.Models
{

    public class MathQuestion
    {
        public int Id { get; set; }
        public int num1 { get; set; }
        public int num2 { get; set; }
        public int score { get; set; }
        public string answer { get; set; }
        public string user_answer { get; set; }

        Random rand = new Random();

        public MathQuestion()
        {
            this.num1 = rand.Next(1, 11);
            this.num2 = rand.Next(1, 11);
            this.answer = (this.num1 + this.num2).ToString();
        }

        public string getQuestion()
        {
            return this.num1 + " + " +  this.num2;
        }
    
    }
}
