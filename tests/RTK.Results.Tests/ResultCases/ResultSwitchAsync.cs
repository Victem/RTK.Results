using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases
{
    public class ResultSwitchAsync
    {
        [Fact]
        public async Task Create_Result_Switch_Success()
        {
            var message = "test";
            var failMessage = "fail";
            var succesMessage = "success";
            //var error = Error.Create("fail", $"Message {message} is not equal {failMessage}");
            var failValue = 10;
            var expectedValue = 4;

            var result = string.Empty;
            await message.ToResult()
                .AsTask()
                .SwitchAsync(
                    r => Task.Run(() => { result = succesMessage; }),
                    e => Task.Run(() => { result = failMessage; })
                 );
            ;

            Assert.Equal(succesMessage, result);
        }

        [Fact]
        public async Task Create_Result_Switch_Error()
        {
            var error = Error.Create("fail", $"Error");
            var failMessage = "fail";
            var succesMessage = "success";

            var result = string.Empty;
            await error.ToResult<string>()
                .AsTask()
                .SwitchAsync(
                    r => Task.Run(() => { result = succesMessage; }),
                    e => Task.Run(() => { result = failMessage; })
                 )
            ;

            Assert.Equal(failMessage, result);
        }
    }
}
