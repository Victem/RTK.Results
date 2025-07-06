using RTK.Results.Core;

namespace RTK.Results.SampleApi;

public record TestServiceResult(int Message);
public class TestService
{
    public Result<TestServiceResult> HandleNumger(int number) => number.ToResult()
        .FailIf(n => n < 0,  Errors.Error_0001)
        .FailIf(n => n > 10, Errors.Error_0002)
        .Then(n => n - 3)
        .FailIf(n => n < 0, Errors.Error_0003)
        .Then(n => new TestServiceResult(n));
        
}
