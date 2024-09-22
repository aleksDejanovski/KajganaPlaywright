using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestingKajgana : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        
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


}