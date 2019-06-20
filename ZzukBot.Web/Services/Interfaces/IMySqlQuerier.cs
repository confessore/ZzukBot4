using System;
using System.Threading.Tasks;

namespace ZzukBot.Web.Services.Interfaces
{
    public interface IMySqlQuerier
    {
        bool FindEmailConfirmedByEmail(string email);
        Task<bool> FindEmailConfirmedByEmailAsync(string email);
        string FindIdByEmail(string email);
        Task<string> FindIdByEmailAsync(string email);
        string FindIdByUsername(string username);
        Task<string> FindIdByUsernameAsync(string username);
        string FindPasswordHashByEmail(string email);
        Task<string> FindPasswordHashByEmailAsync(string email);
        string FindReferralById(string id);
        Task<string> FindReferralByIdAsync(string id);
        bool FindReferralRedeemedById(string id);
        Task<bool> FindReferralRedeemedByIdAsync(string id);
        DateTime FindSubscriptionExpirationByEmail(string email);
        Task<DateTime> FindSubscriptionExpirationByEmailAsync(string email);
        DateTime FindSubscriptionExpirationById(string id);
        Task<DateTime> FindSubscriptionExpirationByIdAsync(string id);
        string FindUsernameByEmail(string email);
        Task<string> FindUsernameByEmailAsync(string email);
        void SetReferralById(string id, string username);
        Task SetReferralByIdAsync(string id, string username);
        void SetReferralRedeemedById(string id);
        Task SetReferralRedeemedByIdAsync(string id);
        void SetSubscriptionExpirationById(string id, DateTime dateTime);
        Task SetSubscriptionExpirationByIdAsync(string id, DateTime dateTime);
    }
}
