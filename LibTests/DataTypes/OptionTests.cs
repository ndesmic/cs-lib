using System;
using Lib.DataTypes;
using NUnit.Framework;

namespace LibTests.DataTypes
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void Option_should_have_value_if_value()
        {
            var option = Option<int>.Some(1);
            Assert.True(option.HasValue);
            Assert.False(option.IsNone);
            Assert.That(option.ValueOrThrow, Is.EqualTo(1));
        }

        [Test]
        public void Option_should_have_none_if_none()
        {
            var option = Option<int>.None;
            Assert.False(option.HasValue);
            Assert.True(option.IsNone);
        }

        [Test]
        public void Option_should_throw_if_none_and_accessed_value()
        {
            var option = Option<int>.None;
            Assert.Throws<Exception>(() => { var x = option.ValueOrThrow; });
        }

        [Test]
        public void Option_GetValueOrDefault_should_get_value_if_value()
        {
            var option = Option<int>.Some(1);
            Assert.That(option.ValueOrDefault(0), Is.EqualTo(1));
        }

        [Test]
        public void Option_GetValueOrDefault_should_get_default_if_none()
        {
            var option = Option<int>.None;
            Assert.That(option.ValueOrDefault(0), Is.EqualTo(0));
        }

        [Test]
        public void Option_Map_should_map_to_new_option_type()
        {
            var option = Option<int>.Some(5);
            Assert.That(option.Map(x => x.ToString()).ValueOrThrow, Is.EqualTo("5"));
        }

        [Test]
        public void Option_Filter_should_set_rejected_value_to_none()
        {
            var option = Option<int>.Some(7);
            Assert.That(option.Filter(x => x > 10).HasValue, Is.False);
        }

        [Test]
        public void Option_Filter_should_set_accepted_value_to_value()
        {
            var option = Option<int>.Some(11);
            Assert.That(option.Filter(x => x > 10).HasValue, Is.True);
            Assert.That(option.Filter(x => x > 10).ValueOrThrow, Is.EqualTo(11));
        }

        [Test]
        public void Option_AndThen_should_run_delegate_if_value()
        {
            var option = Option<int>.Some(5);
            var value = 0;
            option.AndThen(x => value = x);
            Assert.That(value, Is.EqualTo(5));
        }

        [Test]
        public void Option_AndThen_should_not_run_delegate_if_none()
        {
            var option = Option<int>.None;
            var value = 0;
            option.AndThen(x => value = x);
            Assert.That(value, Is.EqualTo(0));
        }

        [Test]
        public void Option_OrElse_should_run_delegate_if_none()
        {
            var option = Option<int>.None;
            var value = 0;
            option.OrElse(() => value = 5);
            Assert.That(value, Is.EqualTo(5));
        }

        [Test]
        public void Option_OrElse_should_not_run_delegate_if_value()
        {
            var option = Option<int>.Some(7);
            var value = 0;
            option.OrElse(() => value = 5);
            Assert.That(value, Is.EqualTo(0));
        }
    }
}
