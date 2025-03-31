using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases
{
    public class ResultMatch
    {
        [Fact]
        public async Task Create_Result_Match_Success()
        {
            var message = "test";
            //var failMessage = "TEST";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var failValue = 10;
            var expectedValue = 4;


            var result = await message.ToResult()
                .AsTask()
                .Match(r => r.Length, e => failValue);
                ;

            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public async Task Create_Result_Match_Error()
        {
            var error = Error.Create("fail", $"Error");
            var expectedValue = 4;
            var failValue = 10;

            var result = await error.ToResult<string>()
                .AsTask()
                .Match(r => r.Length, e => failValue)
            ;

            Assert.Equal(failValue, result);
        }
    }
}
