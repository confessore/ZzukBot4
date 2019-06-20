using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZzukBot.Web.Services.Interfaces;

namespace ZzukBot.Web.Controllers
{
    public class BPController : Controller
    {
        readonly IEmailSender emailSender;
        readonly IMySqlQuerier mySqlQuerier;

        public BPController(
            IEmailSender emailSender,
            IMySqlQuerier mySqlQuerier)
        {
            this.emailSender = emailSender;
            this.mySqlQuerier = mySqlQuerier;
        }

        [HttpPost]
        public async Task<IActionResult> BP()
        {
            return await ReceiveAsync();
        }

        async Task<IActionResult> ReceiveAsync()
        {
            BPContext context = new BPContext()
            {
                HttpRequest = Request
            };

            using (StreamReader reader = new StreamReader(context.HttpRequest.Body, Encoding.ASCII))
            {
                context.RequestBody = reader.ReadToEnd();
            }

            //Fire and forget verification task
            await Task.Run(() => VerifyTask(context, false));

            return Ok();
        }

        async Task VerifyTask(BPContext context, bool sandbox)
        {
            var pairs = ParseIPN(context.RequestBody);
            var url = sandbox ? $"https://test.bitpay.com/invoices/{pairs["invoice_id"]}" : $"https://bitpay.com/invoices/{pairs["invoice_id"]}";

            await ProcessVerificationResponse(JsonConvert.DeserializeObject<BPModel>(GetUrl(url)));
        }

        async Task ProcessVerificationResponse(BPModel model)
        {
            if (model.Data.Status == "confirmed")
            {
                if (model.Data.ItemDesc.Equals("1Unlimited") || model.Data.ItemDesc.Equals("3Unlimited") ||
                        model.Data.ItemDesc.Equals("6Unlimited") || model.Data.ItemDesc.Equals("12Unlimited"))
                {
                    if (model.Data.ItemDesc.Equals("1Unlimited"))
                        await AddTimeToAccount(model.Data.POSData, 1);
                    if (model.Data.ItemDesc.Equals("3Unlimited"))
                        await AddTimeToAccount(model.Data.POSData, 3);
                    if (model.Data.ItemDesc.Equals("6Unlimited"))
                        await AddTimeToAccount(model.Data.POSData, 6);
                    if (model.Data.ItemDesc.Equals("12Unlimited"))
                        await AddTimeToAccount(model.Data.POSData, 12);

                    await AddRewardTimeToAccount(model.Data.POSData, 1);
                }
            }
        }

        async Task AddTimeToAccount(string userId, int months)
        {
            var subscriptionExpiration = await mySqlQuerier.FindSubscriptionExpirationByIdAsync(userId);
            if (subscriptionExpiration > DateTime.Now)
                await mySqlQuerier.SetSubscriptionExpirationByIdAsync(userId, subscriptionExpiration.AddMonths(months));
            else
                await mySqlQuerier.SetSubscriptionExpirationByIdAsync(userId, DateTime.Now.AddMonths(months));
        }

        async Task AddRewardTimeToAccount(string userId, int months)
        {
            var referral = await mySqlQuerier.FindReferralByIdAsync(userId);
            var redeemed = await mySqlQuerier.FindReferralRedeemedByIdAsync(userId);
            if (!string.IsNullOrEmpty(referral) && !redeemed)
            {
                var id = await mySqlQuerier.FindIdByUsernameAsync(referral);
                await mySqlQuerier.SetReferralRedeemedByIdAsync(userId);
                await AddTimeToAccount(id, months);
            }
        }

        string GetUrl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        Dictionary<string, string> ParseIPN(string ipn)
        {
            var result = new Dictionary<string, string>();
            var pairs = ipn.Split('&');
            foreach (var pair in pairs)
            {
                var split = pair.Split('=');
                var key = split[0];
                var value = split[1];
                result.Add(key, value);
            }
            return result;
        }

        class BPContext
        {
            public HttpRequest HttpRequest { get; set; }
            public string RequestBody { get; set; }
            public string Verification { get; set; } = string.Empty;
        }

        class BPModel
        {
            public Data Data { get; set; }
        }
        
        class Data
        {
            public string ID { get; set; }
            public string Status { get; set; }
            public string ItemDesc { get; set; }
            public string POSData { get; set; }
        }
    }
}
