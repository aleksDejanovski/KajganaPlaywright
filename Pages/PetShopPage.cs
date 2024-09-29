using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTest.Pages
{
    public class PetShopPage
    {
        private IPage _page;

        public PetShopPage(IPage Page) => _page = Page;

        private ILocator IfPetShopIsOpen => _page.GetByRole(AriaRole.Link, new() { Name = "Почетна" });
        private ILocator ItemToShop => _page.GetByRole(AriaRole.Heading, new() { Name = "Iskra QR-PET2506 BLUE Машинка за шишање на домашни миленици" });

        private ILocator ItemToShopOpened => _page.Locator(".tvcms-product-brand-logo");
        private ILocator AddToCardButton => _page.GetByRole(AriaRole.Button, new() { Name = "Додај во кошничка" });


        public async Task<bool> AreWeOnPetShopPage() => await IfPetShopIsOpen.IsVisibleAsync();

        public async Task ClickPetShopItem()
        {
            await ItemToShop.ClickAsync();
        }

        public async Task <bool>IfItemIsOpened() => await ItemToShopOpened.IsEnabledAsync();

        public async Task AddItemToCard()
        {
            await AddToCardButton.ClickAsync();
        }

     
       
    }
}
