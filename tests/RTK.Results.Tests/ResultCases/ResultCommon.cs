using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using RTK.Results.Core;


namespace RTK.Results.Tests.ResultCases
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
            var expectedresult = Result.Ok;
            var error = Error.Create("Test", "Test messae");
            var result = await Result.Failure<Ok>(error).AsTask();

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(result.Error, error);
            Assert.NotEqual(expectedresult, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Null_Result()
        {
            Result<string> result = null;
            //Result<DateTime?> result2 = null;
            Result<string> expectedError = await Error.Null
                .WithMessageTransform(message => string.Format(message, typeof(string).FullName))
                .AsTask<string>()
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

        //[Fact]
        //public async Task Create_Resut_Then_Next_Result()
        //{
        //    var message = "test";
        //    //var failMessage = "TEST";
        //    //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
        //    var expectedValue = 4;

        //    var result = message.ToResult()
        //        .Then(r => r.Length.ToResult())
        //        ;

        //    Assert.True(result.IsSuccess);
        //    Assert.False(result.IsFailure);
        //    Assert.Equal(expectedValue, result);
        //    Assert.Equal(Error.None, result.Error);
        //    //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        //}

        //[Fact]
        //public async Task Create_Resut_Then_Next_Result_On_Error()
        //{
        //    var error = Error.Create("fail", $"Error");
        //    var expectedValue = 4;

        //    var result = error.ToResult<string>()
        //        .Then(r => r.Length.ToResult())
        //        ;

        //    Assert.False(result.IsSuccess);
        //    Assert.True(result.IsFailure);
        //    Assert.Equal(error, result);
        //    Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        //}
    }
}
