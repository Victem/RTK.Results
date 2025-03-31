using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Common
{
    public static class ErrorExtensions
    {
        public static async Task<Result<TValue>> FailIf<TValue>(
        this Task<Result<TValue>> errorOr,
        Func<TValue, bool> onValue,
        Error error)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.FailIf(onValue, error);
        }

        public static async Task<Result<TValue>> FailIf<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, bool> onValue,
            Func<TValue, Error> errorBuilder)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.FailIf(onValue, errorBuilder);
        }

        public static async Task<Result<TValue>> FailIfAsync<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, Task<bool>> onValue,
            Error error)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.FailIfAsync(onValue, error);
        }

        public static async Task<Result<TValue>> FailIfAsync<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, Task<bool>> onValue,
            Func<TValue, Task<Error>> errorBuilder)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.FailIfAsync(onValue, errorBuilder);
        }

        public static async Task<Result<TValue>> Else<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<Error, TValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Else(onError);
        }

        public static async Task<Result<TValue>> Else<TValue>(
            this Task<Result<TValue>> errorOr,
            TValue onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Else(onError);
        }

        public static async Task<Result<TValue>> ElseAsync<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<Error, Task<TValue>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        public static async Task<Result<TValue>> ElseAsync<TValue>(this Task<Result<TValue>> errorOr, Task<TValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        public static async Task<Result<TValue>> Else<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<Error, Error> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Else(onError);
        }

        public static async Task<Result<TValue>> Else<TValue>(
            this Task<Result<TValue>> errorOr,
            Error error)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Else(error);
        }

        public static async Task<Result<TValue>> ElseAsync<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<Error, Task<Error>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        public static async Task<Result<TValue>> ElseAsync<TValue>(
            this Task<Result<TValue>> errorOr,
            Task<Error> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        public static async Task<TNextValue> Match<TValue, TNextValue>(this Task<Result<TValue>> errorOr, Func<TValue, TNextValue> onValue, Func<Error, TNextValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Match(onValue, onError);
        }

        public static async Task<TNextValue> MatchAsync<TValue, TNextValue>(this Task<Result<TValue>> errorOr, Func<TValue, Task<TNextValue>> onValue, Func<Error, Task<TNextValue>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.MatchAsync(onValue, onError);
        }

        public static async Task Switch<TValue>(
            this Task<Result<TValue>> errorOr, Action<TValue> onValue,
            Action<Error> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            result.Switch(onValue, onError);
        }

        public static async Task SwitchAsync<TValue>(
            this Task<Result<TValue>> errorOr, Func<TValue, Task> onValue,
            Func<Error, Task> onError)
        {
            var result = await errorOr.ConfigureAwait(false);
            await result.SwitchAsync(onValue, onError);
        }

        public static async Task<Result<TNextValue>> Then<TValue, TNextValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, Result<TNextValue>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Then(onValue);
        }

        public static async Task<Result<TNextValue>> Then<TValue, TNextValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, TNextValue> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.Then(onValue);
        }

        public static async Task<Result<TValue>> ThenDo<TValue>(
            this Task<Result<TValue>> errorOr,
            Action<TValue> action)
        {
            var result = await errorOr.ConfigureAwait(false);
            return result.ThenDo(action);
        }

        public static async Task<Result<TNextValue>> ThenAsync<TValue, TNextValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, Task<Result<TNextValue>>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ThenAsync(onValue).ConfigureAwait(false);
        }

        public static async Task<Result<TNextValue>> ThenAsync<TValue, TNextValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, Task<TNextValue>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ThenAsync(onValue).ConfigureAwait(false);
        }

        public static async Task<Result<TValue>> ThenDoAsync<TValue>(
            this Task<Result<TValue>> errorOr,
            Func<TValue, Task> action)
        {
            var result = await errorOr.ConfigureAwait(false);
            return await result.ThenDoAsync(action).ConfigureAwait(false);
        }
        public static Result<TValue> ToResult<TValue>(this TValue value)
        {
            return value;
        }

        public static Result<TValue> ToResult<TValue>(this Error error)
        {
            return error;
        }
    }
}
