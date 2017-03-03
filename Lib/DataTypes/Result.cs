using System;
using System.Threading.Tasks;

namespace Lib.DataTypes
{
    //Represents a calculation, if successful has value, if unsuccessful has exception, never both
    public class Result<T>
    {
        public Result(T value)
        {
            HasValue = true;
            Value = value;
        }
        public Result(Exception exception)
        {
            HasValue = false;
            Exception = exception;
        }
        public Exception Exception { get; }
        public T Value { get; }
        public bool HasValue { get; } = false;
        public bool HasException => !HasValue;

        public T ValueOrDefault(T defaultValue)
        {
            return HasValue ? Value : defaultValue;
        }

        public void TryThrow()
        {
            if (HasException)
            {
                throw Exception;
            }
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
    }
}
