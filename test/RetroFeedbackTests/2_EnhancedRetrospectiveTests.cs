using System;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using RetroFeedbackTests.Extensions;
using RetroFeedbackTests.Services;

namespace RetroFeedbackTests;

public class EnhancedRetrospectiveTests : PageTest
{
    [Fact]
    public async Task Retrospective_PlethoraOfFeedback_ExpectCountOfActions()
    {
        // Arrange
        string actionText = "Keep doing it";
        var apiService = new ApiService();
        int numberOfFeedback = 10;
        // Act & Assert
        await Page.GotoAsync("https://www.retrotool.app");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Start a new retro" }).ClickAsync();
        await Task.Delay(TimeSpan.FromSeconds(1));
        var retroId = Page.Url.Split("/").Last();
        File.WriteAllText("./retroId.txt", retroId); // 🪲 Useful to debug the failed runs
        await apiService.AddWorkedFeedback(retroId, numberOfFeedback);
        await Page.ClickButton("Group & vote comments");
        await Page.Locator("reach-portal").GetByText("Group & vote comments").ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Group)).ToContainTextAsync("#00Test Automation");
        await Page.ClickButton("Discuss and add action items");
        await Page.ClickButton("Finish retro");
        await Page.EnterTextBoxText("We need to do...", actionText);
        await Task.Delay(1000);
        await Page.ClickButton("Export");
        //await Page.PauseAsync(); // 🪲 useful for debugging with the UI open
        //await Task.Delay(1000); // Sometimes the export button takes a subsecond to render the final screen and the counts are 0
        var listOfWorks = Page.Locator("ul").Nth(0);
        var li = await listOfWorks.Locator("li").CountAsync();
        Assert.Equal(5, li);
    }
}
