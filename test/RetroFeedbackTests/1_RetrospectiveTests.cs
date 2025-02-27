using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using RetroFeedbackTests.Extensions;
using RetroFeedbackTests.Services;

namespace RetroFeedbackTests;

public class RetrospectiveTests : PageTest
{
    [Fact]
    public async Task FullRetrospective_SingleWorksSingleAction_AllInformationExported()
    {
        // Arrange
        var loginService = new LoginService();
        loginService.Login();
        var expected = "WorksTest AutomationAction ItemsKeep doing things";
        var actionText = "Keep doing it";

        // Act & Assert
        await Page.GotoAsync("https://www.retrotool.app/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Start a new retro" }).ClickAsync();
        await Page.EnterTextBoxText("It worked well that...", "Test Automation");
        await Page.ClickButton("Group & vote comments");
        await Page.Locator("reach-portal").GetByText("Group & vote comments").ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Group)).ToContainTextAsync("#00Test Automation");
        await Page.ClickButton("Discuss and add action items");
        await Page.ClickButton("Finish retro");
        await Page.EnterTextBoxText("We need to do.....", actionText);
        await Page.ClickButton("Export");
        await Expect(Page.Locator("div").Filter(new() { HasText = "WorksTest AutomationAction" }).Nth(2)).ToContainTextAsync(expected);
    } 
}


// await Page.GotoAsync("https://www.retrotool.app/");
// await Page.GetByRole(AriaRole.Link, new() { Name = "Start a new retro" }).ClickAsync();
// await Page.GetByRole(AriaRole.Textbox, new() { Name = "It worked well that..." }).ClickAsync();
// await Page.GetByRole(AriaRole.Textbox, new() { Name = "It worked well that..." }).FillAsync("Test Automation");
// await Page.Keyboard.PressAsync("Enter");
// await Page.GetByRole(AriaRole.Button, new() { Name = "Group & vote comments" }).ClickAsync();
// await Page.Locator("reach-portal").GetByText("Group & vote comments").ClickAsync();
// await Expect(Page.GetByRole(AriaRole.Group)).ToContainTextAsync("#00Test Automation");
// await Page.GetByRole(AriaRole.Button, new() { Name = "Discuss and add action items" }).ClickAsync();
// await Page.GetByRole(AriaRole.Button, new() { Name = "Finish retro" }).ClickAsync();
// await Page.GetByRole(AriaRole.Textbox, new() { Name = "We need to do..." }).ClickAsync();
// await Page.GetByRole(AriaRole.Textbox, new() { Name = "We need to do..." }).FillAsync("Keep doing it");
// await Page.Keyboard.PressAsync("Enter");
// await Page.GetByRole(AriaRole.Button, new() { Name = "Export" }).ClickAsync();
// await Page.Locator("div").Filter(new() { HasText = "WorksTest AutomationAction" }).Nth(2).ClickAsync();
// await Expect(Page.GetByRole(AriaRole.Group)).ToContainTextAsync("WorksTest AutomationAction ItemsKeep doing it");