using QuestionsAskingServer.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuestionsAskingServer.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionTypeId { get; set; } 
        public int? CorrectAnswerId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
