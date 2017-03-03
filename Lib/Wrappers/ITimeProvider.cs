using System;

namespace Lib.Wrappers
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
