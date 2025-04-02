using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit.Sdk;

namespace RTK.Results.Tests.ResultCases;

public class ResultAwait
{
    [Fact]
    public async Task Success_From_Task()
    {
        var expectedresult = Result.Ok;
        //var result = Result.Success(new Ok());
        var ok = new Ok();

        var result = await Task.FromResult(new Ok()).AwaitResult();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(result.Error, Error.None);
        Assert.Equal(expectedresult, result.Value);
    }


    [Fact]
    public async Task Fail_From_Task()
    {
        var expectedresult = Result.Ok;
        var errorMessage = "Test exception";
        var exception = new Exception(errorMessage);
        //var result = Result.Success(new Ok());
        var expectedResult = exception.ToResult<Ok>();
        var ok = new Ok();

        var result = await Task.FromException<Ok>(new Exception(errorMessage))
            .AwaitResult()
            ;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(errorMessage, result.Error.Message);
        Assert.Throws(typeof(InvalidOperationException), () => result.Value);
    }

    [Fact]
    public async Task Fail_From_Task_WithErrorBuilder()
    {
        var expectedresult = Result.Ok;
        var errorMessage = "Test exception";
        var exception = new Exception(errorMessage);
        //var result = Result.Success(new Ok());
        var expectedResult = exception.ToResult<Ok>();
        var expectedError = Error.Create("fail", "Fail with custom message");
        var ok = new Ok();

        var result = await Task.FromException<Ok>(new Exception(errorMessage))
            .AwaitResult(e => Error.Create("fail", "Fail with custom message"))
            ;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(expectedError, result.Error);
        Assert.Equal(expectedError.Message, result.Error.Message);
        Assert.Throws(typeof(InvalidOperationException), () => result.Value);
    }

    [Fact]
    public async Task Success_From_ValueTask()
    {
        var expectedresult = Result.Ok;
        //var result = Result.Success(new Ok());
        var ok = new Ok();

        var result = await ValueTask.FromResult(new Ok()).AwaitResult();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(result.Error, Error.None);
        Assert.Equal(expectedresult, result.Value);
    }


    [Fact]
    public async Task Fail_From_ValueTask()
    {
        var expectedresult = Result.Ok;
        var errorMessage = "Test exception";
        var exception = new Exception(errorMessage);
        //var result = Result.Success(new Ok());
        var expectedResult = exception.ToResult<Ok>();
        var ok = new Ok();

        var result = await ValueTask.FromException<Ok>(new Exception(errorMessage))
            .AwaitResult()
            ;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        //Assert.Equal(result.Error, expectedResult.Error);
        //Assert.Equal(expectedResult, result);
        Assert.Equal(errorMessage, result.Error.Message);
        Assert.Throws(typeof(InvalidOperationException), () => result.Value);

        //Assert.False(result.IsSuccess);
        //Assert.True(result.IsFailure);
        //Assert.Equal(error, result);
    }

    [Fact]
    public async Task Fail_From_ValueTask_WithErrorBuilder()
    {
        var expectedresult = Result.Ok;
        var errorMessage = "Test exception";
        var exception = new Exception(errorMessage);
        //var result = Result.Success(new Ok());
        var expectedResult = exception.ToResult<Ok>();
        var expectedError = Error.Create("fail", "Fail with custom message");
        var ok = new Ok();

        var result = await ValueTask.FromException<Ok>(new Exception(errorMessage))
            .AwaitResult(e => Error.Create("fail", "Fail with custom message"))
            ;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(expectedError, result.Error);
        Assert.Equal(expectedError.Message, result.Error.Message);
        Assert.Throws(typeof(InvalidOperationException), () => result.Value);
    }
}
