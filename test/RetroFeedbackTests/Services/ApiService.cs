using System;
using System.Text.Json;
using Microsoft.Playwright;

namespace RetroFeedbackTests.Services;

public class ApiService
{
    private const string _apiUrl = "https://www.retrotool.app/api/graph";
    public async Task AddWorkedFeedback(string id, int numberOfFeedback)
    {
        for (int i = 0; i < numberOfFeedback; i++)
        {
            var payload = GenerateWorksJson(id, $"Test Automation #{i}");
            //TODO: Sent the payload to the API
            await Task.Delay(1);
        }
    }


    private static object? GenerateWorksJson(string id, string title)
    {
        var payload = new
        {
            operationName = "createWorksItem",
            variables = new
            {
                slug = id,
                title = title
            },
            query = "mutation createWorksItem($slug: String!, $title: String!) {\n  createWorksItem(retroSlug: $slug, title: $title) {\n    id\n    hidden\n    title\n    userUuid\n    votes\n    __typename\n  }\n}\n"
        };

        return payload;
    }
}
