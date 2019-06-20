using Microsoft.AspNetCore.Identity;
using System;

namespace ZzukBot.Web.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? SubscriptionExpiration { get; set; }
        public string Referral { get; set; }
        public bool ReferralRedeemed { get; set; }
    }
}
