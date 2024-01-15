using QuestionsAskingServer.Enums;
using QuestionsAskingServer.Models;

namespace QuestionsAskingServer.Data.Seeder
{
    internal static class DbInitializer
    {
        public static void Initialize(QASDBContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Questions.Any())
            {
                //QuestionTypeEntity
                var questionTypes = new List<QuestionTypeEntity>
                {
                    new QuestionTypeEntity { Type = QuestionType.Trivia, Name = "Trivia" },
                    new QuestionTypeEntity { Type = QuestionType.Poll, Name = "Poll" }
                };

                context.QuestionTypes.AddRange(questionTypes);

                // Trivia Questions
                var triviaQuestions = new List<Question>
                {
                    new Question
                    {
                        Text = "What is the largest planet in our solar system?",
                        QuestionTypeId = QuestionType.Trivia,
                        CorrectAnswerId = 3,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Mars" },
                            new Answer { Text = "Earth" },
                            new Answer { Text = "Jupiter" },
                            new Answer { Text = "Venus" }
                        }
                    },
                    new Question
                    {
                        Text = "Who is the author of 'To Kill a Mockingbird'?",
                        QuestionTypeId = QuestionType.Trivia,
                        CorrectAnswerId = 1,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Harper Lee" },
                            new Answer { Text = "Mark Twain" },
                            new Answer { Text = "J.K. Rowling" },
                            new Answer { Text = "George Orwell" }
                        }
                    },
                    new Question
                    {
                        Text = "What is the capital of France?",
                        QuestionTypeId = QuestionType.Trivia,
                        CorrectAnswerId = 2,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Berlin" },
                            new Answer { Text = "Paris" },
                            new Answer { Text = "Rome" },
                            new Answer { Text = "Madrid" }
                        }
                    },
                    new Question
                    {
                        Text = "Which planet is known as the Red Planet?",
                        QuestionTypeId = QuestionType.Trivia,
                        CorrectAnswerId = 4,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Venus" },
                            new Answer { Text = "Jupiter" },
                            new Answer { Text = "Mars" },
                            new Answer { Text = "Mercury" }
                        }
                    },
                };

                // Poll Questions
                var pollQuestions = new List<Question>
                {
                    new Question
                    {
                        Text = "Do you enjoy reading books?",
                        QuestionTypeId = QuestionType.Poll,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Yes" },
                            new Answer { Text = "No" }
                        }
                    },
                    new Question
                    {
                        Text = "Which programming language do you prefer?",
                        QuestionTypeId = QuestionType.Poll,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Python" },
                            new Answer { Text = "JavaScript" },
                            new Answer { Text = "C#" }
                        }
                    },
                    new Question
                    {
                        Text = "Are you excited about the upcoming holidays?",
                        QuestionTypeId = QuestionType.Poll,
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Yes" },
                            new Answer { Text = "No" }
                        }
                    },
                };

                    context.Questions.AddRange(triviaQuestions);
                    context.Questions.AddRange(pollQuestions);

                    context.SaveChanges();
                }
        }
    }

}
