using RTK.Results.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Tests.ErrorCases
{
    public class ErrorCommon
    {
        //private static new Dictionary<string, object> GetMetadata() => new Dictionary<string, object>
        //{
        //    ["test1"] = new DateTime(2000, 1, 1),
        //    ["test2"] = "Test metadata 2"
        //};

        [Fact]
        public async Task Error_Create()
        {
            var code = "000";
            var test = "Test";
            var testDetail = "Test detail";
            var testInstance = "Test instance";
            var type = nameof(ErrorCommon);
            var statusCode = 999;
            var testMessage = "Test Message";
            var metadata = new Dictionary<string, object>
            {
                ["test1"] = new DateTime(2000, 1, 1),
                ["test2"] = "Test metadata 2"
            };


            var error = Error.Create(code, test, testDetail, testInstance, type, statusCode)
                .WithMessage("Test Message")
                .WithMetadata(metadata)
                .WithMetadata("test3", 1)
                .WithStatusCode(statusCode)
                .WithType(type);
            //Result<int> result = Result.Success(2);

            Assert.Equal(error.Message, error.Title);
            Assert.Equal(testMessage, error.Message);
            Assert.Equal(type, error.Type);
            Assert.Equal(statusCode, error.StatusCode);
            
        }

        [Fact]
        public async Task Error_Equals()
        {
            var code = "000";
            var test = "Test";
            var testDetail = "Test detail";
            var testInstance = "Test instance";
            var type = nameof(ErrorCommon);
            var statusCode = 999;
            var testMessage = "Test Message";
            var metadata = new Dictionary<string, object>
            {
                ["test1"] = new DateTime(2000, 1, 1),
                ["test2"] = "Test metadata 2"
            };


            var errorOne = Error.Create(code, test, testDetail, testInstance, type, statusCode)
                .WithMessage("Test Message")
                .WithMetadata(metadata)
                .WithMetadata("test3", 1)
                .WithStatusCode(statusCode)
                .WithType(type);

            var errorTwo = Error.Create(code, test, testDetail, testInstance, type, statusCode)
                .WithMessage("Test Message")
                .WithMetadata(metadata)
                .WithMetadata("test3", 1)
                .WithMetadata("test3", 1)
                .WithStatusCode(statusCode)
                .WithType(type);
            //Result<int> result = Result.Success(2);
            var isEquals = errorOne.Equals(errorTwo);


            Assert.Equal(errorOne.Message, errorOne.Title);
            Assert.Equal(testMessage, errorOne.Message);
            Assert.Equal(type, errorOne.Type);
            Assert.Equal(statusCode, errorOne.StatusCode);
            Assert.Equal(errorOne.GetHashCode(), errorTwo.GetHashCode());
            Assert.True(isEquals);
        }

        [Fact]
        public async Task Error_Not_Equals()
        {
            var code = "000";
            var test = "Test";
            var testDetail = "Test detail";
            var testInstance = "Test instance";
            var type = nameof(ErrorCommon);
            var statusCode = 999;
            var testMessage = "Test Message";
            var metadata = new Dictionary<string, object>
            {
                ["test1"] = new DateTime(2000, 1, 1),
                ["test2"] = "Test metadata 2"
            };


            var errorOne = Error.Create(code, test, testDetail, testInstance, type, statusCode)
                .WithMessage("Test Message")
                .WithMetadata(metadata)
                .WithMetadata("test3", 1)
                .WithStatusCode(statusCode)
                .WithType(type);

            var errorTwo = Error.Create(code, test, testDetail, testInstance, type, statusCode)
                .WithMessage("Test Message")
                .WithMetadata(metadata)
                .WithMetadata("test3", 2)
                .WithStatusCode(statusCode)
                .WithType(type);
            //Result<int> result = Result.Success(2);
            var isEquals = errorOne.Equals(errorTwo);
            var isNotEqual = errorOne != errorTwo;

            Assert.Equal(errorOne.Message, errorOne.Title);
            Assert.Equal(testMessage, errorOne.Message);
            Assert.Equal(type, errorOne.Type);
            Assert.Equal(statusCode, errorOne.StatusCode);
            Assert.False(isEquals);
            Assert.True(isNotEqual);
        }
    }
}
