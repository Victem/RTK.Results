using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RTK.Results.Common;


namespace RTK.Results.Tests
{
    public class ResultCommon
    {
        [Fact]
        public async Task Create_Empty_Result()
        {

            var expectedresult = Result.Ok;
            var result = Result.Success(new Ok());

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.Equal(expectedresult.Value, result.Value);


        }

        [Fact]
        public async Task Create_Failure_Result()
        {
            //var expectedresult = Result.Ok;
            //var error = Error.Create("Test", "Test messae");
            //var result = Result.Failure<Ok>(error);

            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(result.Error, error);
            //Assert.NotEqual(expectedresult, result);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);

        }

        [Fact]
        public async Task Create_String_Result()
        {
            //var expectedResult = "Test";
            //var result = Result.Success("Test");

            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            ////Assert.Equal(expectedresult.Error, Error.None);
            //Assert.Equal(result.Error, Error.None);
            //Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public async Task Create_Null_Result()
        {
            Result<string> result = null;
            //Result<DateTime?> result2 = null;
            Result<string> expectedError = Error.Null
                .WithMessageTransform(message => string.Format(message, typeof(string).FullName))
                ;

            //expectedError = expectedError.WithMessageTransform(message => string.Format(message, typeof(string).FullName));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            
            //Assert.Equal(expectedresult.Error, Error.None);
            Assert.Equal(expectedError, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_Implicit()
        {
            var value = "Test";
            Result<string> result = value;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public async Task Create_Error_Implicit()
        {
            var error = Error.Create("Implicit_Error", "Implicit error");
            Result<string> result = error;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result.Error, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }


        [Fact]
        public async Task Create_Success_With_Fail_On_First_Step()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = error.ToResult<string>()
                .FailIf(r => r == failMessage, error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Fail_On_Next_Step()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = message.ToResult()
                .FailIf(r => r == failMessage, error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Success_On_Next_Step()
        {
            var message = "test";
            var successMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {successMessage}");
            Result<string> expectedSuccessResult = message;

            Result<string> result = message.ToResult()
                .FailIf(r => r == successMessage, error);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedSuccessResult, result);
            Assert.Equal(expectedSuccessResult.Error, Error.None);
        }

        [Fact]
        public async Task Create_Success_With_Fail_On_First_Step_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = error.ToResult<string>()
                .FailIf(r => r == failMessage, r => error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Fail_On_Next_Step_With_Error_Builder()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = message.ToResult()
                .FailIf(r => r == failMessage, r => error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result, error);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Success_With_Success_On_Next_Step_With_Error_Builder()
        {
            var message = "test";
            var successMessage = "test";
            var error = Error.Create("fail", $"Message {message} is not equal {successMessage}");
            Result<string> expectedSuccessResult = message;

            Result<string> result = message.ToResult()
                .FailIf(r => r == successMessage, r => error);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedSuccessResult, result);
            Assert.Equal(expectedSuccessResult.Error, Error.None);
        }


        [Fact]
        public async Task Create_Success_With_Fail_Async_On_First_Step()
        {
            var message = "test";
            var failMessage = "TEST";
            var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");


            Result<string> result = await error.ToResult<string>()
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
                .FailIfAsync(r => Task.FromResult(r == successMessage), r => Task.FromResult(error));

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedSuccessResult, result);
            Assert.Equal(expectedSuccessResult.Error, Error.None);
        }

        [Fact]
        public async Task Create_Resut_Then_Next()
        {
            var message = "test";
            //var failMessage = "TEST";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var expectedValue = 4;

            var result = message.ToResult()
                .Then(r => r.Length)
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Next_On_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;

            var result = error.ToResult<string>()
                .Then(r => r.Length)
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Next_Result()
        {
            var message = "test";
            //var failMessage = "TEST";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var expectedValue = 4;

            var result = message.ToResult()
                .Then(r => r.Length.ToResult())
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Next_Result_On_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;

            var result = error.ToResult<string>()
                .Then(r => r.Length.ToResult())
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Async_Next()
        {
            var message = "test";
            //var failMessage = "TEST";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var expectedValue = 4;

            var result = await message.ToResult()
                .ThenAsync(r => Task.FromResult(r.Length))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Async_On_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;

            var result = await error.ToResult<string>()
                .ThenAsync(r => Task.FromResult(r.Length))
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }
    }
}
