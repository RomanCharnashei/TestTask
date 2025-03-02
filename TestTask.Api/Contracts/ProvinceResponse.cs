using TestTask.Api.СachedСountries;

namespace TestTask.Api.Contracts
{
    public class ProvinceResponse
    {
        public ProvinceResponse(string code, string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(code);
            Code = code;
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            Name = name;
        }

        public string Code { get; }
        public string Name { get; }

        public static IEnumerable<ProvinceResponse> MapFrom(IEnumerable<FileProvince> provinces)
        {
            return provinces.Select(x => new ProvinceResponse(x.Code, x.Name));
        }
    }
}
