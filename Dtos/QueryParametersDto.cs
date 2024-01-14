namespace QuestionsAskingServer.Dtos
{
    public record QueryParametersDto(string? SearchText = null, int? PageNumber = 1, int? PageSize = 10);
}
