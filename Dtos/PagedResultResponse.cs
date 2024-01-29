using System.Reflection.Emit;

namespace QuestionsAskingServer.Dtos
{
    public record PagedResultResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
