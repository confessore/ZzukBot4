using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZzukBot.Web.Identity;

namespace ZzukBot.Web.Areas.Identity.Pages.Account.Manage
{
    public class SubscribeModel : PageModel
    {
        readonly UserManager<ApplicationUser> _userManager;

        public SubscribeModel(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public DateTime? SubscriptionExpiration { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            ApplicationUser = user;

            return Page();
            //return NotFound($"Purchasing time is not currently available. This is temporary. Please check back at a later date.");
        }
    }
}