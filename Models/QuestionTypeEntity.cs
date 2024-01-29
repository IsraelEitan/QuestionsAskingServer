using QuestionsAskingServer.Enums;

namespace QuestionsAskingServer.Models
{
    public class QuestionTypeEntity
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }
        public string Name { get; set; } = null!;
    }
}
