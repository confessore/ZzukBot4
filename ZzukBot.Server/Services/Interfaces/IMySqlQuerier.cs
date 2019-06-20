using System;
using System.Threading.Tasks;

namespace ZzukBot.Server.Services.Interfaces
{
    internal interface IMySqlQuerier
    {
        bool FindEmailConfirmedByEmail(string email);
        Task<bool> FindEmailConfirmedByEmailAsync(string email);
        string FindIdByEmail(string email);
        Task<string> FindIdByEmailAsync(string email);
        string FindIdByUsername(string username);
        Task<string> FindIdByUsernameAsync(string username);
        string FindPasswordHashByEmail(string email);
        Task<string> FindPasswordHashByEmailAsync(string email);
        DateTime FindSubscriptionExpirationByEmail(string email);
        Task<DateTime> FindSubscriptionExpirationByEmailAsync(string email);
        DateTime FindSubscriptionExpirationById(string id);
        Task<DateTime> FindSubscriptionExpirationByIdAsync(string id);
        string FindUsernameByEmail(string email);
        Task<string> FindUsernameByEmailAsync(string email);
        void SetSubscriptionExpirationById(string id, DateTime dateTime);
        Task SetSubscriptionExpirationByIdAsync(string id, DateTime dateTime);
    }
}
