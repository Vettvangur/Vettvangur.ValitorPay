using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vettvangur.ValitorPay;
using Vettvangur.ValitorPay.Models.Enums;
/*
Card number	Md Status	Md Error Msg
2223000010275433	0	Not Authenticated, do not continue transaction
2223000010246699	1	null
2223000010181581	2	null
2223000010275466	4	null
2223000010275474	5	U-received
2223000010275482	6	Error received
2223000010275490	7	Out Error
2223000010275508	8	Block by Fraud Score
2223000010275516	9	Pending
*/
namespace ValitorPay.Tests
{
    [TestClass]
    public class ValitorPayTests
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
        public async Task CardVerification()
        {
            var svc = _services.GetRequiredService<ValitorPayService>();

            var resp = await svc.CardVerificationAsync(new CardVerificationRequest
            {
                CardNumber = Config.cardNumber,
                ExpirationMonth = Config.expirationMonth,
                ExpirationYear = Config.expirationYear,
                Amount = 0,
                AuthenticationUrl = new Uri("https://webhook.site/2416f57a-38e9-4995-9a8f-8d3310c3c22f"),

                // This code forces 3ds, nice for testing
                ThreeDs20AdditionalParamaters = new ThreeDs20AdditionalParamaters
                {
                    ThreeDs2XGeneralExtrafields = new ThreeDs2XGeneralExtrafields
                    {
                        ThreeDsRequestorChallengeInd = ThreeDsRequestorChallenge.ChallengeRequested_Mandate
                    }
                }
            });
        }

        [TestMethod]
        public async Task CreatesVirtualCard()
        {
            var svc = _services.GetRequiredService<ValitorPayService>();

            var resp = await svc.CreateVirtualCardAsync(new CreateVirtualCardRequest
            {
                CardNumber = Config.cardNumber,
                ExpirationMonth = Config.expirationMonth,
                ExpirationYear = Config.expirationYear,
                Cvc = Config._cvc,

                //AgreementNumber = "",
                SubsequentTransactionType = SubsequentTransactionType.CardholderInitiatedCredentialOnFile,
                CardVerificationData = new CardVerificationData
                {
                    Cavv = "kBP5OpNm7/lF4QAAa9SfsjUhJ1sP",
                    Xid = "AT/BtUxNO8IdtQUPLA7DZSdPXeA=",
                    DsTransId = "f91f3b39-9a5e-4313-ac75-9aba7fa9b50f",
                    MdStatus = 1
                },
            });
        }

        [TestMethod]
        public async Task VirtualCardPayment()
        {
            var svc = _services.GetRequiredService<ValitorPayService>();

            var resp = await svc.VirtualCardPaymentAsync(new VirtualCardPaymentRequest
            {
                Operation = Operation.Sale,
                VirtualCardNumber = new Guid("c813b459-ea5a-457b-89ca-9ec717692846").ToString(),
                CardVerificationData = new CardVerificationData
                {
                    Cavv = "kA1Hja90rhaDCRECEKc0AEUAAAAA",
                    Xid = "wCpUsc4I+34I+zXpEgQbee2hKrM=",
                    MdStatus = 1,
                },
                Amount = 200000,
                ScaExemption = ScaExemption.Automatic,
            });
        }
    }
}
