using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class ValitorRetryPolicy : IValitorRetryPolicy
    {
        /// <summary>
        /// It can offen be preferrable to fail faster so the user facing UI doesn't hang.
        /// This way the user is informed quickly of an issue and can make his own decision to retry / wait.
        /// </summary>
        public const int RetryCount = 1;
        /// <summary>
        /// Backoff seconds is created by multiplying this and the retry attempt number
        /// </summary>
        public const int BackoffTimeMultiplier = 1;

        // Handle both exceptions and return values in one policy
        static readonly HttpStatusCode[] retryCodes = {
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            (HttpStatusCode)429, // 429
        };

        // Handle both exceptions and return values in one policy
        static readonly HttpStatusCode[] otherCodes = {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.GatewayTimeout, // 504
        };

        static readonly HttpStatusCode[] allCodes = retryCodes.Concat(otherCodes).ToArray();

        public virtual Task<T> DefaultPolicy<T>(Func<Task<HttpResponseMessage>> action, ILogger logger)
            where T : ValitorResponseBase
            => BaseValitorPolicy<T>(action, BasePolicy, logger);

        private AsyncRetryPolicy<HttpResponseMessage> BasePolicy()
            => Polly.Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => allCodes.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(BackoffTimeMultiplier, retryAttempt))
                );

        /// <summary>
        /// Intended to be safe from double purchases, according to comments from Microsoft on
        /// <see cref="HttpRequestException"/>, it should throw as such on network errors.
        /// The exceptions to that are calls to EnsureSuccessStatusCode which does not apply here.
        /// https://github.com/dotnet/runtime/issues/911#issuecomment-454446820
        /// 
        /// We only handle HttpRequestException in inner policy which doesn't enclose our logResponse.
        /// </summary>
        /// <returns></returns>
        public virtual Task<T> PurchaseTransactionPolicy<T>(Func<Task<HttpResponseMessage>> action, ILogger logger)
            where T : ValitorResponseBase
            => BaseValitorPolicy<T>(action, BasePurchaseTransactionPolicy, logger);

        private Task<T> BaseValitorPolicy<T>(
            Func<Task<HttpResponseMessage>> action,
            Func<AsyncRetryPolicy<HttpResponseMessage>> innerPolicy,
            ILogger logger)
            where T : ValitorResponseBase
            => Policy<T>
                .HandleResult(r => StatusCodes.IsRetryableErrorCode(r))
                .WaitAndRetryAsync(RetryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(BackoffTimeMultiplier, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    var resp = await innerPolicy()
                        .ExecuteAsync(action)
                        .ConfigureAwait(false);

                    var content = await DeserializeAndLogResponseAsync<T>(resp, logger)
                        .ConfigureAwait(false);

                    return content;
                });

        private AsyncRetryPolicy<HttpResponseMessage> BasePurchaseTransactionPolicy()
            => Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => retryCodes.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(BackoffTimeMultiplier, retryAttempt))
                );

        private async Task<T> DeserializeAndLogResponseAsync<T>(HttpResponseMessage resp, ILogger logger)
            where T : ValitorResponseBase
        {
            if (!resp.IsSuccessStatusCode)
            {
                // Don't try to deserialize object in error cases
                // we do not depend on shape of error object.
                // Otherwise we might miss important response data
                var content = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                // Create HttpRequestException to log or throw
                try
                {
                    resp.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException ex)
                {
                    var respStr = JsonSerializer.Serialize(content);
                    logger?.LogError(
                        ex, 
                        string.IsNullOrEmpty(respStr) ? null : respStr);

                    throw new ValitorPayResponseException("ValitorPay response exception", ex)
                    {
                        ValitorResponseRaw = respStr,
                    };
                }

                return null; // unreachable
            }
            else
            {
                var content = await resp.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);

                if (!content.IsSuccess)
                {
                    logger?.LogError(JsonSerializer.Serialize(content));

                    // We don't want to force callers to check the Valitor response body for success
                    if (!StatusCodes.IsRetryableErrorCode(content))
                    {
                        throw new ValitorPayResponseException
                        {
                            ValitorResponse = content,
                            ValitorResponseRaw = JsonSerializer.Serialize(content)
                        };
                    }
                }

                return content;
            }
        }

        // Alternate version for when the remote api has a uniform response shape
        // for all response status codes
        //private async Task<T> DeserializeAndLogResponseAsync<T>(HttpResponseMessage resp, ILogger logger)
        //{
        //    string content = null;

        //    if (!resp.IsSuccessStatusCode)
        //    {
        //        if (logger != null)
        //        {
        //            // Log the raw response body, regardless of shape
        //            content = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

        //            // Create HttpRequestException to log or throw
        //            try
        //            {
        //                resp.EnsureSuccessStatusCode();
        //            }
        //            catch (HttpRequestException ex)
        //            {
        //                if (content != null)
        //                {
        //                    logger.LogError(ex, content);
        //                }
        //                else
        //                {
        //                    // We're done, game over
        //                    throw;
        //                }
        //            }
        //        }
        //    }

        //    // Here we want to make sure to either throw an HttpRequestException,
        //    // or return a non-null ValitorResponseBase
        //    try
        //    {
        //        // Could be an unexpected string
        //        if (content != null)
        //        {
        //            return JsonSerializer.Deserialize<T>(content);
        //        }
        //        else
        //        {
        //            // Could be empty
        //            return await resp.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        //        }
        //    }
        //    catch (Exception ex)
        //    when (ex is ArgumentNullException || ex is JsonException || ex is NotSupportedException)
        //    {
        //        // Should always throw here
        //        resp.EnsureSuccessStatusCode();

        //        return default;
        //    }
        //}

    }
}
