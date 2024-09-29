using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTest.Pages
{
    public class LoginPage
    {
        private IPage _page;

        public LoginPage(IPage Page) {
            _page = Page;
        }

        private ILocator Email => _page.Locator("#email");
        private ILocator PassWord => _page.Locator("#field-password");
        private ILocator Submit => _page.Locator("#submit-login");

        private ILocator SignedIn => _page.GetByText(" ace aceski");

        public async Task SubmitLogin(string username, string password)
        {
            await Email.FillAsync(username);
            await PassWord.FillAsync(password);
            await Submit.ClickAsync();
        }


        public async Task <bool> IfUserIsLogedIn() => await SignedIn.IsVisibleAsync();




    }
}
