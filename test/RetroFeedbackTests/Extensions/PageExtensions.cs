using System;
using Microsoft.Playwright;

namespace RetroFeedbackTests.Extensions;

public static class PageExtensions
{
    public static async Task ClickButton(this IPage page, string selector)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = selector }).CheckAsync();
    }
}
