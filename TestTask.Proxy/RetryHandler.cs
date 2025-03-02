using Polly;
using System.Diagnostics;

namespace TestTask.Proxy
{
    public class RetryHandler : DelegatingHandler
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        public RetryHandler()
        {
            _retryPolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(10, retryAttempt =>
                {
                    Debug.WriteLine($"Retry Attempt: {retryAttempt}");
                    return TimeSpan.FromSeconds(1);
                });
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            return await _retryPolicy.ExecuteAsync(() =>
            {
                Debug.WriteLine($"Retry Policy Execute: {DateTime.Now}");
                return base.SendAsync(request, cancellationToken);
            });
        }
    }
}
