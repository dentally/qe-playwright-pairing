using System;
using Microsoft.Playwright;

namespace RetroFeedbackTests.Services;

public static class TextBoxService
{
    public static async Task EnterTextBoxText(this IPage page, string selector, string text)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = selector }).ClickAsync();
        await Task.Delay(1000); // ❄️ Need to delay so that the click event is registered
        await page.GetByRole(AriaRole.Textbox, new() { Name = selector }).FillAsync(text);
        await page.Keyboard.PressAsync("Enter");
        await Task.Delay(1000); // ❄️ Need to delay so that the keyboard event is registered
    }
}
