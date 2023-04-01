using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Vettvangur.ValitorPay.Models;

namespace Vettvangur.ValitorPay
{
    public class ValitorPayService
    {
        static readonly JsonSerializerOptions _jsonSerializerOptions
            = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

        readonly ILogger _logger;
        readonly HttpClient _httpClient;
        readonly IValitorRetryPolicy _retryPolicy;
        string _agreementNumber;
        string _terminalId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="retryPolicy"></param>
        /// <param name="httpClientFac">Can't get typed HttpClient to work in AspNet Framework</param>
        public ValitorPayService(
            ILogger<ValitorPayService> logger,
            IValitorRetryPolicy retryPolicy,
            IHttpClientFactory httpClientFac)
            : this(retryPolicy, httpClientFac)
        {
            _logger = logger;
        }
        public ValitorPayService(
            IValitorRetryPolicy retryPolicy,
            IHttpClientFactory httpClientFac)
        {
            _retryPolicy = retryPolicy;
            _httpClient = httpClientFac.CreateClient("ValitorPay");
        }

        public virtual Task<CardVerificationResponse> CardVerificationAsync(CardVerificationRequest req)
        {
            PopulateTerminalAgreement(req);

            return _retryPolicy.DefaultPolicy<CardVerificationResponse>(
                () => _httpClient.PostAsJsonAsync(
                    "cardVerification",
                    req,
                    _jsonSerializerOptions),
                _logger);
        }

        public virtual Task<CreateVirtualCardResponse> CreateVirtualCardAsync(CreateVirtualCardRequest req)
        {
            PopulateTerminalAgreement(req);
            return _retryPolicy.DefaultPolicy<CreateVirtualCardResponse>(
                () => _httpClient.PostAsJsonAsync(
                    "virtualCard/createVirtualCard",
                    req,
                    _jsonSerializerOptions),
                _logger);
        }

        public virtual Task<CardPaymentResponse> CardPaymentAsync(CardPaymentRequest req)
        {
            PopulateTerminalAgreement(req);
            return _retryPolicy.PurchaseTransactionPolicy<CardPaymentResponse>(
                () => _httpClient.PostAsJsonAsync(
                    "payment/cardPayment",
                    req,
                    _jsonSerializerOptions),
                _logger);
        }

        public virtual Task<VirtualCardPaymentResponse> VirtualCardPaymentAsync(VirtualCardPaymentRequest req)
        {
            PopulateTerminalAgreement(req);
            return _retryPolicy.PurchaseTransactionPolicy<VirtualCardPaymentResponse>(
                () => _httpClient.PostAsJsonAsync(
                    "payment/virtualCardPayment",
                    req,
                   _jsonSerializerOptions),
                _logger);
        }
        public virtual Task<GetVirtualCardDataResponse> GetVirtualCardData(GetVirtualCardDataRequest req)
        {
            PopulateTerminalAgreement(req);
            return _retryPolicy.PurchaseTransactionPolicy<GetVirtualCardDataResponse>(
                () => _httpClient.PostAsJsonAsync(
                    "VirtualCard/GetVirtualCardData",
                    req,
                   _jsonSerializerOptions),
                _logger);
        }

        public void ConfigureAgreement(string agreementNumber, string terminalId)
        {
            _agreementNumber = agreementNumber;
            _terminalId = terminalId;
        }
        private void PopulateTerminalAgreement(ValitorRequestBase request)
        {
            if (!string.IsNullOrEmpty(_agreementNumber) && !string.IsNullOrEmpty(_terminalId))
            {
                request.AgreementNumber = _agreementNumber;
                request.TerminalId = _terminalId;
            }
        }
    }
}
