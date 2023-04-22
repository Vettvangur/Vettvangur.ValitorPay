using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if NETCOREAPP
using Microsoft.AspNetCore.Builder;
#endif

namespace Vettvangur.ValitorPay
{
    public static class ValitorPayServiceCollectionExtensions
    {
#if NETFRAMEWORK
        public static IServiceCollection AddValitorPay(
            this IServiceCollection services,
            Uri apiUrl,
            string apiKey,
            ILoggerProvider loggerProvider = null,
            Action<ILoggingBuilder> loggingBuilder = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHttpClient<ValitorPayService>("ValitorPay", httpClient =>
            {
                httpClient.BaseAddress = apiUrl;

                httpClient.DefaultRequestHeaders.Add("valitorpay-api-version", "2.0");
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("APIKey", apiKey);

#if NET5_0_OR_GREATER
// Support Http 2.0
                httpClient.DefaultRequestVersion = new Version(2, 0);
#endif
            });
            if (loggerProvider != null)
            {
                services.AddLogging(b =>
                {
                    // Affects IHttpClientFactory created clients
                    b.AddProvider(loggerProvider);
                });
            }
            else if (loggingBuilder != null)
            {
                services.AddLogging(loggingBuilder);
            }
            services.AddSingleton<IValitorRetryPolicy, ValitorRetryPolicy>();
            services.AddTransient<ValitorPayService>();
            return services;
        }

        public static IServiceProvider BuildValitorProvider(
            this IServiceCollection services,
            Uri apiUrl,
            string apiKey,
            ILoggerProvider loggerProvider = null,
            Action<ILoggingBuilder> loggingBuilder = null)
        {
            var collection = AddValitorPay(services, apiUrl, apiKey, loggerProvider, loggingBuilder);
            return collection.BuildServiceProvider();
        }
#endif

#if NETCOREAPP
        public static IServiceCollection AddValitorPay(
            this IServiceCollection services,
            Uri apiUrl,
            string apiKey
        )
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHttpClient<ValitorPayService>("ValitorPay", (sp, httpClient) =>
            {
                httpClient.BaseAddress = apiUrl;

                httpClient.DefaultRequestHeaders.Add("valitorpay-api-version", "2.0");
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("APIKey", apiKey);

#if NET5_0_OR_GREATER
                // Support Http 2.0
                httpClient.DefaultRequestVersion = new Version(2, 0);
#endif
            });

            services.AddSingleton<IValitorRetryPolicy, ValitorRetryPolicy>();
            services.AddTransient<ValitorPayService>();

            return services;
        }
#endif
    }
}
