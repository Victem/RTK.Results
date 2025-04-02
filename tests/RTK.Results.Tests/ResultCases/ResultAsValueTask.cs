using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases;

public class ResultAsValueTask
{
    [Fact]
    public async Task Success_As_ValueTask_From_Value()
    {
        var expectedresult = Result.Ok;
        //var result = Result.Success(new Ok());
        var ok = new Ok();

        var result = await Result.Ok.AsValueTask();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(result.Error, Error.None);
        Assert.Equal(expectedresult, result.Value);
    }

    [Fact]
    public async Task Fail_As_ValueTask_From_Error()
    {
        var expectedresult = Result.Ok;
        var errorMessage = "Test exception";
        var exception = new Exception(errorMessage);
        var expectedError = Error.Create("fail", "Fail with custom message");
        //var result = Result.Success(new Ok());
        var expectedResult = exception.ToResult<Ok>();
        var ok = new Ok();

        var result = await Error.Create("fail", "Fail with custom message").AsValueTask<Ok>();

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(expectedError, result.Error);
        Assert.Equal(expectedError.Message, result.Error.Message);
        Assert.Throws(typeof(InvalidOperationException), () => result.Value);
    }
}
