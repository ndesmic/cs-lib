using System;
using Lib.DataTypes;

namespace Lib.Extensions
{
    public static class OptionExtensions
    {
        public static Result<T> ToResult<T>(this Option<T> option, Func<Exception> mapNone)
        {
            return option.IsNone ? Result<T>.Error(mapNone()) : Result<T>.Ok(option.ValueOrThrow);
        }
    }
}
