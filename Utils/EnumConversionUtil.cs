using QuestionsAskingServer.Enums;
using QuestionsAskingServer.Exceptions;

namespace QuestionsAskingServer.Utils
{
    public static class EnumConversionUtil
    {
        public static TEnum ConvertIntToEnum<TEnum>(int source) where TEnum : struct
        {
            if (Enum.IsDefined(typeof(TEnum), source))
            {
                return (TEnum)(object)source;
            }
            else
            {
                throw new InvalidInputException($"Invalid value for enum {typeof(TEnum).Name}: {source}");
            }
        }
    }

}
