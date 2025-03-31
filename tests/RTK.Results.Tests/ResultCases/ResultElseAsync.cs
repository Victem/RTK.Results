using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RTK.Results.Tests.ResultCases
{
    public class ResultElseAsync
    {
        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Succes_Winth_Error_Builder()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");

            var message = "test";
            //var failMessage = "TEST";

            var result = await message.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(e => Task.FromResult(nextError))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Error_Winth_Error_Builder()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");

            var result = await error.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(e => Task.FromResult(nextError))
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(nextError, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }


        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Success_With_New_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");

            var message = "test";
            //var failMessage = "TEST";

            var result = await message.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(Task.FromResult(nextError))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Error_With_New_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");

            var result = await error.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(Task.FromResult(nextError))
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(nextError, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Success_With_New_Value_Builder()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");
            var nextValue = 10;
            var message = "test";
            //var failMessage = "TEST";

            var result = await message.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(e => Task.FromResult(nextValue))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Error_With_New_Value_Builder()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");
            var nextValue = 10;

            var result = await error.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(e => Task.FromResult(nextValue))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(nextValue, result);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Success_With_New_Value()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");
            var nextValue = 10;
            var message = "test";
            //var failMessage = "TEST";

            var result = await message.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(Task.FromResult(nextValue))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public async Task Create_Result_Then_ElseAsync_On_Error_With_New_Value()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");
            var nextValue = 10;

            var result = await error.ToResult<string>()
                .AsTask()
                .Then(r => r.Length)
                .ElseAsync(Task.FromResult(nextValue))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(nextValue, result);
            Assert.Equal(Error.None, result.Error);
        }
    }
}
