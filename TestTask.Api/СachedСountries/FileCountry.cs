using System.Text.Json.Serialization;

namespace TestTask.Api.СachedСountries
{
    public class FileCountry
    {
        [JsonConstructor]
        public FileCountry(string code2, string name, IEnumerable<FileProvince> states)
        {
            Code2 = code2 ?? throw new ArgumentNullException(nameof(code2));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            States = states ?? throw new ArgumentNullException(nameof(states));
        }

        public string Code2 { get; }
        public string Name { get; }
        public IEnumerable<FileProvince> States { get; }
    }
}
