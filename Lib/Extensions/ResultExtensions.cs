using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.DataTypes;

namespace Lib.Extensions
{
    public static class ResultExtensions
    {
        public static Option<T> ToOption<T>(this Result<T> result)
        {
            return result.HasException ? Option<T>.None : Option<T>.Some(result.ValueOrThrow);
        }
    }
}
