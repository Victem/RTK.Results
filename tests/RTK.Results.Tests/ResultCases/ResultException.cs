using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ResultCases
{
    public class ResultException
    {
        [Fact]
        public async Task Exception_To_Result()
        {
            var errorMessage = "Test exception";
            var exception = new Exception(errorMessage);

            var result = exception.ToResult<string>();

            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error.Message);
        }
    }
}
