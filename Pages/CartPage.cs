using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTest.Pages
{
    public class CartPage
    {
        private IPage _page;

        public CartPage(IPage Page) =>  _page = Page;

        private ILocator ItemAddedToCart => _page.GetByRole(AriaRole.Heading, new() { Name = "Кошничка" });

        public async Task ItemAddedToCartCheck()
        {
            await ItemAddedToCart.IsVisibleAsync();
        }

        //await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Кошничка" })).ToBeVisibleAsync();



    }
}
