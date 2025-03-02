namespace TestTask.Api.Contracts
{
    public class UserInfoResponse
    {
        public UserInfoResponse(string? accessLevel)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(accessLevel);
            AccessLevel = accessLevel;
        }

        public string AccessLevel { get; }
    }
}
