using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using PlaywrightTest.Pages;

namespace PlaywrightTest;



[TestFixture]
public class TestingKajgana : PageTest
{
    private DashBoardPage DashBoardPage;
    private LoginPage LoginPage;
    private PetShopPage PetShopPage;
    private CartPage CartPage;

    [SetUp]
    public async Task SetUp()
    {
        DashBoardPage = new DashBoardPage(Page);
        LoginPage = new LoginPage(Page);
        PetShopPage = new PetShopPage(Page);
        CartPage = new CartPage(Page);
        await Page.GotoAsync("https://shop.kajgana.com/");
    }
    [Test]
    public async Task HasTitle()
    {
       
        await Expect(Page).ToHaveTitleAsync(new Regex("Kajgana Shop"));
    }

 
    
    [Test]
    public async Task ClickFitness()
    {
        await Page.GetByRole(AriaRole.Link, new() { Name = "Фитнес" }).ClickAsync();
        await Expect(Page.Locator("text=Фитнес")).ToBeVisibleAsync();
    }

    [Test]
    public async Task ClickDom()
    {
        await Page.ClickAsync("text='Дом'");
        await Expect(Page.Locator("#footer_sub_menu_tvfooter_category").GetByRole(AriaRole.Link, new() { Name = "Мебел" })).ToBeVisibleAsync();
        
    }

    [Test]
    public async Task SearchKaciga()
    {
        await Page.GetByPlaceholder("Внеси поим за пребарување...").FillAsync("kaciga");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Детска заштитна кацига MAX H-104 M" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task LogIn()
    {
        await Page.GetByRole(AriaRole.Button, new() { Name= "User Icon" } ).HoverAsync();
        await Page.ClickAsync(".tvhedaer-sign-span");
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Најавете се на вашата сметка" })).ToBeVisibleAsync();
        await Page.FillAsync("#email", "dejanovski_a@yahoo.com");
        await Page.FillAsync("#field-password", "aA123456789");
        await Page.ClickAsync("#submit-login");
        await Expect(Page.GetByText(" ace aceski")).ToBeVisibleAsync();
    }

    [Test]
    public async Task LoginAndAddToCart()
    {
       

        await Page.GetByRole(AriaRole.Button, new() { Name = "User Icon" }).HoverAsync();
        await Page.ClickAsync(".tvhedaer-sign-span");
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Најавете се на вашата сметка" })).ToBeVisibleAsync();
        await Page.FillAsync("#email", "dejanovski_a@yahoo.com"); 
        await Page.FillAsync("#field-password", "aA123456789");
        await Page.ClickAsync("#submit-login");
        await Expect(Page.GetByText(" ace aceski")).ToBeVisibleAsync();
        await Page.ClickAsync("text=Пет шоп");
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Почетна" })).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Heading, new() { Name = "Iskra QR-PET2506 BLUE Машинка за шишање на домашни миленици" }).ClickAsync();
        await Expect(Page.Locator(".tvcms-product-brand-logo")).ToBeEnabledAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Додај во кошничка" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Кошничка" })).ToBeVisibleAsync();

    }
    [Test]
    public async Task LoginWithInvalidCredentials()
    {

        await Page.GetByRole(AriaRole.Button, new() { Name = "User Icon" }).HoverAsync();
        await Page.ClickAsync(".tvhedaer-sign-span");
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Најавете се на вашата сметка" })).ToBeVisibleAsync();
        await Page.FillAsync("#email", "test@yahoo.com");
        await Page.FillAsync("#field-password", "Atest12est");
        await Page.ClickAsync("#submit-login");
        await Expect(Page.GetByText("Неуспешна најава.")).ToBeVisibleAsync();

    }
    [Test]
    public async Task LoginWithValidCredentialsPOM()
    {
        
        await DashBoardPage.ClickLoginButton();
        Assert.True(await DashBoardPage.AreWeOnLoginPage());
        await LoginPage.SubmitLogin("dejanovski_a@yahoo.com", "aA123456789");
        Assert.True(await LoginPage.IfUserIsLogedIn());

    }

    [Test]
    public async Task LoginWithInvalidCredentialsPOM()
    {

        await DashBoardPage.ClickLoginButton();
        Assert.True(await DashBoardPage.AreWeOnLoginPage());
        await LoginPage.SubmitLogin("test13ee3@yahoo.com", "Atest12ssa");
        Assert.True(await LoginPage.IfUserIsNotLogedIn());

    }
    [Test]
    public async Task LoginAndAddToCartItemPOM()
    {
        await DashBoardPage.ClickLoginButton();
        await LoginPage.SubmitLogin("dejanovski_a@yahoo.com", "aA123456789");
        Assert.True(await LoginPage.IfUserIsLogedIn());
        await DashBoardPage.ClickPetShopButton();
        await PetShopPage.AreWeOnPetShopPage();
        await PetShopPage.ClickPetShopItem();
        Assert.True(await PetShopPage.IfItemIsOpened());
        await PetShopPage.AddItemToCard();
        await CartPage.ItemAddedToCartCheck();

    }

}