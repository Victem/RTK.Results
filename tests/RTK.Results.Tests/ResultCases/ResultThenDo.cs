using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases
{
    public class ResultThenDo
    {
        [Fact]
        public async Task Create_Result_ThenDo_Success()
        {
            var message = "test";
            var failMessage = "fail";
            var succesMessage = "success";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var failValue = 10;
            var expectedValue = 4;

            var resultMessage = string.Empty;
            var result = await message.ToResult()
                .AsTask()
                .ThenDo(r => { resultMessage = succesMessage; });
            
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(message, result);
            Assert.Equal(Error.None, result.Error);
            Assert.Equal(succesMessage, resultMessage);
        }

        [Fact]
        public async Task Create_Result_ThenDo_Error()
        {
            var error = Error.Create("fail", $"Error");
            var failMessage = "fail";
            var succesMessage = "success";

            var resultMessage = string.Empty;
            var result = await error.ToResult<string>()
                .AsTask()
                .ThenDo(
                    r => { resultMessage = failMessage; }
                   
                 )
            ;

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result.Error);
            Assert.NotEqual(failMessage, resultMessage);
        }
    }
}
