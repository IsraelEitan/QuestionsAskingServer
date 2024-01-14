using System.ComponentModel.DataAnnotations;

namespace QuestionsAskingServer.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
        public int? CorrectAnswerId { get; set; } 
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
