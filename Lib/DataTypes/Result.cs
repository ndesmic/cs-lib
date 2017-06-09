using System;
using System.Threading.Tasks;

namespace Lib.DataTypes
{
    //Represents a calculation, if successful has value, if unsuccessful has exception, never both
    public class Result<T>
    {
        private Result(T valueOrThrow)
        {
            HasValue = true;
            ValueOrThrow = valueOrThrow;
        }
        private Result(Exception exception)
        {
            HasValue = false;
            Exception = exception;
        }
        public Exception Exception { get; }
        public T ValueOrThrow { get; }
        public bool HasValue { get; } = false;
        public bool HasException => !HasValue;

        public T ValueOrDefault(T defaultValue)
        {
            return HasValue ? ValueOrThrow : defaultValue;
        }

        public void TryThrow()
        {
            if (HasException)
            {
                throw Exception;
            }
        }

        public Result<TU> Map<TU>(Func<T,TU> func)
        {
            return HasValue ? new Result<TU>(func(ValueOrThrow)) : new Result<TU>(Exception);
        }

        public Result<T> Filter(Func<T, bool> func, Func<T,Exception> exceptionMapFunc)
        {
            if (HasException)
            {
                return Error(Exception);
            }
            return func(ValueOrThrow) ? Ok(ValueOrThrow) : Error(exceptionMapFunc(ValueOrThrow));
        }

        public Result<T> AndThen(Action<T> func)
        {
            if (HasException)
            {
                return new Result<T>(Exception);
            }
            func(ValueOrThrow);
            return new Result<T>(ValueOrThrow);
        }

        public Result<T> OrElse(Func<T> func)
        {
            return HasException ? new Result<T>(func()) : new Result<T>(ValueOrThrow);
        }

        public static Result<T> Try(Func<T> func)
        {
            Result<T> result;
            try
            {
                result = new Result<T>(func());
            }
            catch (Exception ex)
            {
                result = new Result<T>(ex);
            }
            return result;
        }

        public static async Task<Result<T>> TryAsync(Func<Task<T>> func)
        {
            Result<T> result;
            try
            {
                result = new Result<T>(await func());
            }
            catch (Exception ex)
            {
                result = new Result<T>(ex);
            }
            return result;
        }

        public static Result<T> Ok(T value){
            return new Result<T>(value);
        }

        public static Result<T> Error(Exception exception)
        {
            return new Result<T>(exception);
        } 
    }
}
