

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Core
{
    public struct Ok { }

    // docs
    public static class Result
    {
        public static Result<Ok> Ok => Result<Ok>.Success(new Ok() /*default(Ok)*/);
        public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);
        public static Result<TValue> Failure<TValue>(Error value) => Result<TValue>.Failure(value);

        //public static MyResult<TValue> Ok2 => MyResult<TValue>.Success(default(Ok));
    }

    // docs 1111
    public struct Result<TValue>
    {
        private readonly TValue _value;
        private readonly Error _error;

        public bool IsSuccess => _error == Error.None;

        public bool IsFailure => !IsSuccess;

        public Error Error => _error;

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The Value property cannot be accessed when errors have been recorded. Check IsError before accessing Value.");



        private Result(TValue value)
        {
            _value = value;
            _error = Error.None;
        }

        private Result(Error Error)
        {
            _value = default;
            _error = Error;
        }

        public static Result<TValue> Success(TValue value)
        {
            return value != null
                ? new Result<TValue>(value)
                : new Result<TValue>(Error.Null.WithMessageTransform(message => string.Format(message, typeof(TValue).FullName)));
        }

        public static Result<TValue> Failure(Error error)
        {
            return new Result<TValue>(error);
        }


        public static implicit operator Result<TValue>(TValue value)
        {
            return Result.Success<TValue>(value);
        }

        public static implicit operator Result<TValue>(Error error)
        {
            return Result.Failure<TValue>(error);
        }

        public Result<TValue> FailIf(Func<TValue, bool> onValue, Error error)
        {
            if (IsFailure) { return this; }
            return onValue(Value) ? error : this;
        }

        public Result<TValue> FailIf(Func<TValue, bool> onValue, Func<TValue, Error> errorBuilder)
        {
            if (IsFailure) { return this; }
            return onValue(Value) ? errorBuilder(Value) : this;
        }

        public async Task<Result<TValue>> FailIfAsync(Func<TValue, Task<bool>> onValue, Error error)
        {
            if (IsFailure) { return this; }
            return await onValue(Value).ConfigureAwait(false) ? error : this;
        }

        public async Task<Result<TValue>> FailIfAsync(Func<TValue, Task<bool>> onValue, Func<TValue, Task<Error>> errorBuilder)
        {
            if (IsFailure) { return this; }
            return await onValue(Value).ConfigureAwait(false)
                ? await errorBuilder(Value).ConfigureAwait(false)
                : this;
        }


        public Result<TValue> Else(Func<Error, Error> onError)
        {
            return IsSuccess
                ? Value
                : onError(Error).ToResult<TValue>();
        }

        public Result<TValue> Else(Error error)
        {
            return IsSuccess
                ? Value
                : error.ToResult<TValue>();
        }

        public Result<TValue> Else(Func<Error, TValue> onError)
        {
            return IsSuccess
                ? Value
                : onError(Error);
        }

        public Result<TValue> Else(TValue onError)
        {
            return IsSuccess
                ? Value
                : onError;
        }

        public async Task<Result<TValue>> ElseAsync(Func<Error, Task<TValue>> onError)
        {
            return IsSuccess
                ? Value
                : await onError(Error).ConfigureAwait(false);

        }

        public async Task<Result<TValue>> ElseAsync(Func<Error, Task<Error>> onError)
        {
            return IsSuccess
                ? Value
                : (await onError(Error).ConfigureAwait(false)).ToResult<TValue>();
        }



        public async Task<Result<TValue>> ElseAsync(Task<Error> error)
        {
            return IsSuccess
                ? Value
                : (await error.ConfigureAwait(false)).ToResult<TValue>();
        }


        public async Task<Result<TValue>> ElseAsync(Task<TValue> onError)
        {
            return IsSuccess
                ? Value
                : await onError.ConfigureAwait(false);
        }


        public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<Error, TNextValue> onError)
        {
            return IsSuccess
                ? onValue(Value)
                : onError(Error);
        }

        public async Task<TNextValue> MatchAsync<TNextValue>(Func<TValue, Task<TNextValue>> onValue, Func<Error, Task<TNextValue>> onError)
        {
            return IsSuccess
                ? await onValue(Value).ConfigureAwait(false)
                : await onError(Error).ConfigureAwait(false);
        }


        public void Switch(Action<TValue> onValue, Action<Error> onError)
        {
            if (IsFailure) { onError(Error); }
            if (IsSuccess) { onValue(Value); }

        }

        public async Task SwitchAsync(Func<TValue, Task> onValue, Func<Error, Task> onError)
        {
            if (IsFailure) { await onError(Error).ConfigureAwait(false); }
            if (IsSuccess) { await onValue(Value).ConfigureAwait(false); }
        }


        public Result<TValue> ThenDo(Action<TValue> action)
        {
            if (IsFailure)
            {
                return Error;
            }

            action(Value);

            return this;
        }

        public async Task<Result<TValue>> ThenDoAsync(Func<TValue, Task> action)
        {
            if (IsFailure)
            {
                return Error;
            }

            await action(Value).ConfigureAwait(false);

            return this;
        }

        public Result<TNextValue> Then<TNextValue>(Func<TValue, Result<TNextValue>> onValue)
        {
            return IsSuccess
                ? onValue(Value)
                : Error;
        }

        public Result<TNextValue> Then<TNextValue>(Func<TValue, TNextValue> onValue)
        {
            return IsSuccess
                ? onValue(Value)
                : Error.ToResult<TNextValue>();
        }

        public async Task<Result<TNextValue>> ThenAsync<TNextValue>(Func<TValue, Task<Result<TNextValue>>> onValue)
        {
            return IsSuccess
                ? await onValue(Value).ConfigureAwait(false)
                : Error;
        }

        

        public async Task<Result<TNextValue>> ThenAsync<TNextValue>(Func<TValue, Task<TNextValue>> onValue)
        {
            return IsSuccess
                ? await onValue(Value).ConfigureAwait(false)
                : Error.ToResult<TNextValue>();
        }
    }
}
