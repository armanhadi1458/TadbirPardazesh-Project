using System.Text.Json;

namespace Core.Utils
{
    public static class JsonSerializerSetting
    {
        public static JsonSerializerOptions JsonSerializerOptions =>
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false,
                MaxDepth = 10,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        public static JsonSerializerOptions JsonDeserializerOptions =>
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                MaxDepth = 10,
            };
    }
}    