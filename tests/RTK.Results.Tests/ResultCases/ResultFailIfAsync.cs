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
        public async Task Create_Async_Error_When_Initial_Is_Error_And_Condition_Fail()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await error.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, errorNext);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }


        [Fact]
        public async Task Create_Async_Error_When_Initial_Is_Error_And_Condition_Success()
        {
            var message = "test";
            var failMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await error.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, errorNext);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }


        [Fact]
        public async Task Create_Async_Error_When_Initial_Is_Value_And_Condition_Success()
        {
            var message = "test";
            var failMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            //var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await message.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Async_Success_When_Initial_Is_Value_And_Condition_Fail()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            //var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await message.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, error);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }



        ///------------------------------

        [Fact]
        public async Task Create_Async_Error_When_Initial_Is_Error_And_Condition_Fail_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await error.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, async (v) => errorNext);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }


        [Fact]
        public async Task Create_Async_Error_When_Initial_Is_Error_And_Condition_Success_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await error.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, async (v) => errorNext);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }


        [Fact]
        public async Task Create_Async_Error_When_Initial_Is_Value_And_Condition_Success_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            //var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await message.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, async (v) => error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Async_Success_When_Initial_Is_Value_And_Condition_Fail_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            //var errorNext = Error.Create("failNext", $"Message {message} is not equal {failMessage}");

            Result<string> result = await message.ToResult<string>()
                .AsTask()
                .FailIfAsync(async r => r == failMessage, async (v) => error);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }
    }
}
