using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases
{
    public class ResultThen
    {
        [Fact]
        public async Task Create_Result_Then_Next()
        {
            var message = "test";
            //var failMessage = "TEST";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var expectedValue = 4;

            var result = await message.ToResult()
                .AsTask()
                .Then(r => r.Length)
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Result_Then_Next_On_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;

            var result = await error.ToResult<string>()
                .AsTask()
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

            var result = await message.ToResult()
                .AsTask()
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

            var result = await error.ToResult<string>()
                .AsTask()
                .Then(r => r.Length.ToResult())
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }
    }
}
