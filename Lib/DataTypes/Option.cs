using System;

namespace Lib.DataTypes
{
    //Idea stolen from function languages to represent existence, can have value or none but not both, similar to F# Option.
    //This helps us in instances like getting default(T) of a primative where the result is ambiguous without having to change type to 
    //nullable or accessing a dictionary where null and does not exist are different things.
    public class Option<T>
    {
        private readonly T _value;

        private Option() { }

        private Option(T value)
        {
            HasValue = true;
            _value = value;
        }

        public bool HasValue { get; } = false;
        public bool IsNone => !HasValue;
        public T Value
        {
            get
            {
                if (IsNone)
                {
                    throw new Exception("You attempted to get an option value of None which is not allowed, please check the value before accessing.");
                }
                return _value;
            }
        }
        public T ValueOrDefault(T defaultValue)
        {
            return IsNone ? defaultValue : Value;
        }
        public Option<TU> Map<TU>(Func<T, TU> func)
        {
            return IsNone ? Option<TU>.None : new Option<TU>(func(Value));
        }

        public Option<T> AndThen(Action<T> func)
        {
            if (IsNone)
            {
                return None;
            }
            func(Value);
            return new Option<T>(Value);
        }

        public Option<T> OrElse(Func<T> func)
        {
            return IsNone ? new Option<T>(func()) : new Option<T>(Value);
        }

        public static Option<T> None => new Option<T>();
        public static Option<T> Some(T value) => new Option<T>(value);
    }
}
