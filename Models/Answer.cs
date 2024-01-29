using System.ComponentModel.DataAnnotations;

namespace QuestionsAskingServer.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int Votes { get; set; } = 0;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
