using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestingKajgana : PageTest
{
    [SetUp]
    public async Task SetUp()
    {

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


}