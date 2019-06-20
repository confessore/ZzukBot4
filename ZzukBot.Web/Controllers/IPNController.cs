using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZzukBot.Web.Services.Interfaces;

namespace ZzukBot.Web.Controllers
{
    public class IPNController : Controller
    {
        readonly IMySqlQuerier _mySqlQuerier;

        public IPNController(IMySqlQuerier mySqlQuerier)
        {
            _mySqlQuerier = mySqlQuerier;
        }

        [HttpPost]
        public async Task<IActionResult> IPN()
        {
            return await ReceiveAsync();
        }

        async Task<IActionResult> ReceiveAsync()
        {
            IPNContext ipnContext = new IPNContext()
            {
                IPNRequest = Request
            };

            using (StreamReader reader = new StreamReader(ipnContext.IPNRequest.Body, Encoding.ASCII))
            {
                ipnContext.RequestBody = reader.ReadToEnd();
            }

            //Fire and forget verification task
            await Task.Run(() => VerifyTask(ipnContext, false));

            //Reply back a 200 code
            return Ok();
        }

        async Task VerifyTask(IPNContext ipnContext, bool useSandbox)
        {
            try
            {
                var url = useSandbox ? "https://ipnpb.sandbox.paypal.com/cgi-bin/webscr" : "https://ipnpb.paypal.com/cgi-bin/webscr";
                var verificationRequest = WebRequest.Create(url);

                //Set values for the verification request
                verificationRequest.Method = "POST";
                verificationRequest.ContentType = "application/x-www-form-urlencoded";

                //Add cmd=_notify-validate to the payload
                string strRequest = "cmd=_notify-validate&" + ipnContext.RequestBody;
                verificationRequest.ContentLength = strRequest.Length;

                //Attach payload to the verification request
                using (StreamWriter writer = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII))
                {
                    writer.Write(strRequest);
                }

                //Send the request to PayPal and get the response
                using (StreamReader reader = new StreamReader(verificationRequest.GetResponse().GetResponseStream()))
                {
                    ipnContext.Verification = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await ProcessVerificationResponse(ipnContext);
        }

        async Task ProcessVerificationResponse(IPNContext ipnContext)
        {
            if (ipnContext.Verification.Equals("VERIFIED"))
            {
                var ipnModel = new IPNModel();
                var pairs = ParseIPN(ipnContext.RequestBody);
                ipnModel.Custom = pairs["custom"];
                ipnModel.ItemName = pairs["item_name"];
                ipnModel.PaymentStatus = pairs["payment_status"];

                if (ipnModel.PaymentStatus.Equals("Completed"))
                {
                    if (ipnModel.ItemName.Equals("1Unlimited") || ipnModel.ItemName.Equals("3Unlimited") ||
                        ipnModel.ItemName.Equals("6Unlimited") || ipnModel.ItemName.Equals("12Unlimited"))
                    {
                        if (ipnModel.ItemName.Equals("1Unlimited"))
                            await AddTimeToAccount(ipnModel.Custom, 1);
                        if (ipnModel.ItemName.Equals("3Unlimited"))
                            await AddTimeToAccount(ipnModel.Custom, 3);
                        if (ipnModel.ItemName.Equals("6Unlimited"))
                            await AddTimeToAccount(ipnModel.Custom, 6);
                        if (ipnModel.ItemName.Equals("12Unlimited"))
                            await AddTimeToAccount(ipnModel.Custom, 12);

                        await AddRewardTimeToAccount(ipnModel.Custom, 1);
                    }
                }
            }
        }

        async Task AddTimeToAccount(string userId, int months)
        {
            var subscriptionExpiration = await _mySqlQuerier.FindSubscriptionExpirationByIdAsync(userId);
            if  (subscriptionExpiration > DateTime.Now)
                await _mySqlQuerier.SetSubscriptionExpirationByIdAsync(userId, subscriptionExpiration.AddMonths(months));
            else
                await _mySqlQuerier.SetSubscriptionExpirationByIdAsync(userId, DateTime.Now.AddMonths(months));
        }

        async Task AddRewardTimeToAccount(string userId, int months)
        {
            var referral = await _mySqlQuerier.FindReferralByIdAsync(userId);
            var redeemed = await _mySqlQuerier.FindReferralRedeemedByIdAsync(userId);
            if (!string.IsNullOrEmpty(referral) && !redeemed)
            {
                var id = await _mySqlQuerier.FindIdByUsernameAsync(referral);
                await _mySqlQuerier.SetReferralRedeemedByIdAsync(userId);
                await AddTimeToAccount(id, months);
            }
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

        class IPNContext
        {
            public HttpRequest IPNRequest { get; set; }
            public string RequestBody { get; set; }
            public string Verification { get; set; } = string.Empty;
        }

        class IPNModel
        {
            public string Custom { get; set; }
            public string ItemName { get; set; }
            public string PaymentStatus { get; set; }
        }
    }
}
