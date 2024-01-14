namespace QuestionsAskingServer.Data
{
    using Microsoft.EntityFrameworkCore;
    using QuestionsAskingServer.Models;

    public class QASDBContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public QASDBContext(DbContextOptions<QASDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Question>()
                .ToTable("Questions")
                .HasKey(q => q.Id);

            modelBuilder.Entity<Answer>()
                .ToTable("Answers")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Question>()
            .Property(q => q.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Answer>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }

}
