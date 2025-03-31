using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases
{
    public class ResultFailIfAsync
    {

        [Fact]
        public async Task Create_Success_With_Fail_Async_On_First_Step()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = await error.ToResult<string>()
                .AsTask()
                .FailIfAsync(r => Task.FromResult(r == failMessage), error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Fail_Async_On_Next_Step()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = await message.ToResult()
                .AsTask()
                .FailIfAsync(r => Task.FromResult(r == failMessage), error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Success_Async_On_Next_Step()
        {
            var message = "test";
            var successMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {successMessage}");
            Result<string> expectedSuccessResult = message;

            Result<string> result = await message.ToResult()
                .FailIfAsync(r => Task.FromResult(r == successMessage), error);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedSuccessResult, result);
            Assert.Equal(expectedSuccessResult.Error, Error.None);
        }


        [Fact]
        public async Task Create_Success_With_Fail_Async_On_First_Step_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = await error.ToResult<string>()
                .AsTask()
                .FailIfAsync(r => Task.FromResult(r == failMessage), r => Task.FromResult(error));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Fail_Async_On_Next_Step_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = await message.ToResult()
                .AsTask()
                .FailIfAsync(r => Task.FromResult(r == failMessage), r => Task.FromResult(error));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Success_Async_On_Next_Step_With_Error_Builder()
        {
            var message = "test";
            var successMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {successMessage}");
            Result<string> expectedSuccessResult = message;

            Result<string> result = await message.ToResult()
                .AsTask()
                .FailIfAsync(r => Task.FromResult(r == successMessage), r => Task.FromResult(error));

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedSuccessResult, result);
            Assert.Equal(expectedSuccessResult.Error, Error.None);
        }
    }
}
