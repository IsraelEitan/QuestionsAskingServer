using Newtonsoft.Json;

namespace QuestionsAskingServer.Dtos
{
    public sealed record ErrorDetailsDto
    {
        public int StatusCode { get; init; }
        public string? Message { get; init; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
