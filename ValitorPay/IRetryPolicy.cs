using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    interface IRetryPolicy
    {
        Task<T> DefaultPolicy<T>(Func<Task<HttpResponseMessage>> action, ILogger logger) where T : ValitorResponseBase;
        Task<T> PurchaseTransactionPolicy<T>(Func<Task<HttpResponseMessage>> action, ILogger logger) where T : ValitorResponseBase;
    }
}