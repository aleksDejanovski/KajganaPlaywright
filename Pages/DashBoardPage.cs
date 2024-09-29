using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTest.Pages
{
    public class DashBoardPage
    {
        private readonly IPage _page;

        public DashBoardPage(IPage Page)
        {
             _page = Page   ;
        }

        private ILocator UserIcon => _page.GetByRole(AriaRole.Button, new() { Name = "User Icon" });
        private ILocator LoginButton => _page.GetByTitle("Најавете се на вашата сметка");
        private ILocator LoginText => _page.GetByRole(AriaRole.Heading, new() { Name = "Најавете се на вашата сметка" });





        public async Task ClickLoginButton()
        {
            await UserIcon.HoverAsync();
            await LoginButton.ClickAsync();
        }

        public async Task<bool> AreWeOnLoginPage() => await LoginText.IsVisibleAsync();


    }
}
