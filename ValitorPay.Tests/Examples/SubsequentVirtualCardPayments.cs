using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vettvangur.ValitorPay;
using Vettvangur.ValitorPay.Models;
using Vettvangur.ValitorPay.Models.Enums;

namespace ValitorPay.Tests.Examples
{
    [TestClass]
    public class SubsequentVirtualCardPayments
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

        public async Task Step1()
        {
            var secret = Config.apiKey
                .Split('.')
                .Last();

            var vcard = Guid.NewGuid(); // Get customer virtual card number
            var merchantData = new MerchantDataVirtualCard
            {
                OrderId = Guid.NewGuid().ToString(),

                VirtualCard = vcard.ToString(),
            };

            var svc = _services.GetRequiredService<ValitorPayService>();

            var amount = 200000;
            var maxScaAmount = 100000;

            if (amount < maxScaAmount)
            {
                var resp = await svc.VirtualCardPaymentAsync(new VirtualCardPaymentRequest
                {
                    Operation = Operation.Sale,
                    VirtualCardNumber = merchantData.VirtualCard,
                    Amount = 200000,
                    ScaExemption = ScaExemption.Automatic,
                });

                if (resp.IsSuccess)
                {
                    CompleteOrder();
                    return;
                }
            }

            var req = new CardVerificationRequest
            {
                VirtualCard = vcard.ToString(),
                Amount = 200000,
                AuthenticationUrl = new Uri("https://webhook.site/aec97fc3-44e4-48c7-9648-81442d8a7d13"),
            };
            req.SetMerchantData(merchantData, secret);

            var verificationResp = await svc.CardVerificationAsync(req);
            if (verificationResp.IsSuccess)
            {
                var rawHtml = verificationResp.CardVerificationRawResponse;
            }
        }

        public async Task Step2()
        {
            var secret = Config.apiKey
                .Split('.')
                .Last();

            var svc = _services.GetRequiredService<ValitorPayService>();

            // Receive callback as CardVerificationCallback after customer completes 3ds
            var callbackData = new CardVerificationCallback
            {
                MD = "",
                TDS2 = new TDS2
                {
                    DsTransID = "cd89beff-e04b-4e31-bea7-73a091c5636b"
                },

                Cavv = "kA1Hja90rhaDCRECEKc0AEUAAAAA",
                Xid = "wCpUsc4I+34I+zXpEgQbee2hKrM=",
                MdStatus = 1,
            };

            var merchantData = callbackData.GetMerchantData<MerchantDataVirtualCard>(secret);

            // var order = GetOrderData(merchantData.OrderId)
            // if (!order.Completed && IsSuccessMdStatus(callbackData.MdStatus))
            // var amount = order.Amount

            var resp = await svc.VirtualCardPaymentAsync(new VirtualCardPaymentRequest
            {
                Operation = Operation.Sale,
                VirtualCardNumber = merchantData.VirtualCard,
                CardVerificationData = new CardVerificationData
                {
                    DsTransId = callbackData.TDS2.DsTransID,
                    Cavv = callbackData.Cavv,
                    Xid = callbackData.Xid,
                    MdStatus = callbackData.MdStatus,
                },
                Amount = 200000,
            });

            if (resp.IsSuccess)
            {
                CompleteOrder();
            }
        }

        private void CompleteOrder()
        {
            // Complete order
            // order.Completed = true;
        }
    }
}
