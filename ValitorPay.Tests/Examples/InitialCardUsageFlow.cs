using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vettvangur.ValitorPay;
using Vettvangur.ValitorPay.Models;
using Vettvangur.ValitorPay.Models.Enums;

namespace ValitorPay.Tests.Examples
{
    [TestClass]
    public class InitialCardUsageFlow
    {
        ServiceProvider _services;

        [TestInitialize]
        public void Init()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddHttpClient();
            collection.AddLogging(b =>
            {
                b.AddFilter((category, level) => true); // Spam the world with logs.

                // Add console logger so we can see all the logging produced by the client by default.
                b.AddConsole(c => c.IncludeScopes = true);
            });

            collection.AddSingleton<IValitorRetryPolicy, ValitorRetryPolicy>();
            collection.BuildValitorProvider(
                Config.apiUrl,
                Config.apiKey
            ); 
            
            _services = collection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task Step1()
        {
            var merchantData = new MerchantDataCard
            {
                OrderId = Guid.NewGuid().ToString(),

                CardNumber = Config.cardNumber,

                ExpirationMonth = Config.expirationMonth,

                ExpirationYear = Config.expirationYear,

                Cvc = Config._cvc,
            };

            var svc = _services.GetRequiredService<ValitorPayService>();

            // "First transactions must provide Card Verification Data"
            //var resp = await svc.CardPaymentAsync(new CardPaymentRequest
            //{
            //    Operation = Operation.Sale,

            //    CardNumber = merchantData.CardNumber,
            //    ExpirationMonth = merchantData.ExpirationMonth,
            //    ExpirationYear = merchantData.ExpirationYear,
            //    CVC = merchantData.Cvc,

            //    Amount = 200000,

            //    ScaExemption = ScaExemption.Automatic,
            //    FirstTransactionData = new FirstTransactionData
            //    {
            //        InitiationReason = InitiationReason.CredentialOnFile,
            //    },
            //});

            //if (resp.IsSuccess) 
            //{
            //    await CompleteOrder(svc, merchantData);
            //}
            //else // if (resp.ResponseCode == ) T8 / TB
            {
                var secret = Config.apiKey
                    .Split('.')
                    .Last();

                var req = new CardVerificationRequest
                {
                    CardNumber = Config.cardNumber,
                    ExpirationMonth = Config.expirationMonth,
                    ExpirationYear = Config.expirationYear,
                    Amount = 1000000,
                    AuthenticationUrl = new Uri("https://webhook.site/a187bc5f-adb7-4bb2-9d67-e6f484808234"),

                    TerminalId = Config.terminalId,
                    AgreementNumber = Config.agreementNumber,
                };
                req.SetMerchantData(merchantData, secret);

                var verificationResp = await svc.CardVerificationAsync(req);
                if (verificationResp.IsSuccess)
                {
                    var rawHtml = verificationResp.CardVerificationRawResponse;
                }
            }
        }

        [TestMethod]
        public async Task Step2()
        {
            var secret = Config.apiKey
                .Split('.')
                .Last();

            // Receive callback as CardVerificationCallback after customer completes 3ds
            var callbackData = new CardVerificationCallback
            {
                TDS2 = new TDS2
                {
                    DsTransID = "4ee269d5-c5bb-4d02-9fc9-e0aaa0feeac6"
                },
                MD = "",

                Cavv = "kBOfzCtngu4mcCcQHXapBjUhVL5Y",
                Xid = "W+nushNuxN9hgL//gASHAmMnrTY=",
                MdStatus = 1,
            };

            var merchantData = callbackData.GetMerchantData<MerchantDataCard>(secret);

            // var order = GetOrderData(merchantData.OrderId)
            // if (!order.Completed)
            // var amount = order.Amount

            var svc = _services.GetRequiredService<ValitorPayService>();

            var resp = await svc.CardPaymentAsync(new CardPaymentRequest
            {
                Operation = Operation.Sale,

                CardNumber = merchantData.CardNumber,
                ExpirationMonth = merchantData.ExpirationMonth,
                ExpirationYear = merchantData.ExpirationYear,
                CVC = merchantData.Cvc,

                CardVerificationData = new CardVerificationData
                {
                    DsTransId = callbackData.TDS2.DsTransID,
                    Cavv = callbackData.Cavv,
                    Xid = callbackData.Xid,
                    MdStatus = callbackData.MdStatus,
                },
                Amount = 1000000,
                FirstTransactionData = new FirstTransactionData
                {
                    InitiationReason = InitiationReason.CredentialOnFile,
                },
                TerminalId = Config.terminalId,
                AgreementNumber = Config.agreementNumber,
                AdditionalData = new CardPaymentAdditionalData
                {
                    MerchantReferenceData = null,
                    //order.UserID,
                    //order.StoreID,
                    //order.WebCoupon,
                    //order.CreditUsed,
                },
            });

            if (resp.IsSuccess)
            {
                await CompleteOrder(svc, merchantData);
            }
        }

        private async Task CompleteOrder(ValitorPayService svc, MerchantDataCard merchantData)
        {
            // Complete order and create virtual card

            var resp2 = await svc.CreateVirtualCardAsync(new CreateVirtualCardRequest
            {
                CardNumber = merchantData.CardNumber,
                ExpirationMonth = merchantData.ExpirationMonth,
                ExpirationYear = merchantData.ExpirationYear,

                SubsequentTransactionType = SubsequentTransactionType.CardholderInitiatedCredentialOnFile,
            });

            // SaveVirtualCard(resp2.VirtualCard);
        }
    }
}
