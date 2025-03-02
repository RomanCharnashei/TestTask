using System.Text.Json.Serialization;

namespace TestTask.Api.СachedСountries
{
    public class FileProvince
    {
        [JsonConstructor]
        public FileProvince(string code, string name)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Code { get; }
        public string Name { get; }
    }
}
