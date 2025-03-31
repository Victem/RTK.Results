using RTK.Results.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RTK.Results.Tests
{
    public class ResultElseAsync
    {
        [Fact]
        public async Task Create_Resut_Then_Next_ElseAsync_Value()
        {
            var message = "test";
            //var failMessage = "TEST";
            var error = Error.Create("fail", $"Error");
            var nextError = Error.Create("next_fail", $"Next Error");
            var expectedValue = 4;
            
            var result = await error.ToResult<string>()
                .Then(r => r.Length)
                .ElseAsync(Task.FromResult(expectedValue))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Next_ElseAsync_Value_Builder()
        {
            var message = "test";
            //var failMessage = "TEST";
            var error = Error.Create("fail", $"Error");
            var nextError = Error.Create("next_fail", $"Next Error");
            var expectedValue = 4;

            var result = await error.ToResult<string>()
                .Then(r => r.Length)
                .ElseAsync(e => Task.FromResult(expectedValue))
                ;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(expectedValue, result);
            Assert.Equal(Error.None, result.Error);
            //Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Next_ElseAsync_On_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");

            var result = await error.ToResult<string>()
                .Then(r => r.Length)
                .ElseAsync(Task.FromResult(nextError))
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(nextError, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }

        [Fact]
        public async Task Create_Resut_Then_Next_ElseAsync_On_Error_Winth_New_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var nextError = Error.Create("next_fail", $"Next Error");

            var result = await error.ToResult<string>()
                .Then(r => r.Length)
                .ElseAsync(e => Task.FromResult(nextError))
                ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(nextError, result);
            Assert.Throws(typeof(InvalidOperationException), () => result.Value);
        }
    }
}
