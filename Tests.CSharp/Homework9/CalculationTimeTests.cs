using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework9;

public class CalculationTimeTests : IClassFixture<WebApplicationFactory<Hw9.Program>>
{
    private readonly HttpClient _client;

    public CalculationTimeTests(WebApplicationFactory<Hw9.Program> fixture)
    {
        _client = fixture.CreateClient();
    }

    [HomeworkTheory(Homeworks.HomeWork9)]
    [InlineData("2 + 3 + 4 + 6")]
    [InlineData("(2 * 3 + 3 * 3) * (5 / 5 + 6 / 6)")]
    [InlineData("(2 + 3) / 12 * 7 + 8 * 9")]
    private async Task CalculatorController_ParallelTest(string expression, long minExpectedTime = 900, long maxExpectedTime = 5000)
    {
        var executionTime = await GetRequestExecutionTime(expression);

        Assert.True(executionTime >= minExpectedTime,
            UserMessagerForTest.WaitingTimeIsLess(minExpectedTime, executionTime));
        Assert.True(executionTime <= maxExpectedTime,
            UserMessagerForTest.WaitingTimeIsMore(maxExpectedTime, executionTime));
    }

    private async Task<long> GetRequestExecutionTime(string expression)
    {
        var watch = Stopwatch.StartNew();
        var response = await _client.PostCalculateExpressionAsync(expression);
        watch.Stop();
        response.EnsureSuccessStatusCode();
        return watch.ElapsedMilliseconds;
    }
}