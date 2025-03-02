namespace TestTask.Api.Contracts
{
    public class RegistrationRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required bool Agree { get; set; }
    }
}
