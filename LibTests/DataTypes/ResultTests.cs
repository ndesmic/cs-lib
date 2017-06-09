using System;
using Lib.DataTypes;
using NUnit.Framework;

namespace LibTests.DataTypes
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void Result_has_value_should_be_true_if_has_value()
        {
            var result = Result<int>.Ok(1);
            Assert.That(result.HasValue, Is.True);
        }
        [Test]
        public void Result_has_value_should_be_false_if_no_value()
        {
            var result = Result<int>.Error(new Exception());
            Assert.That(result.HasValue, Is.False);
        }
        [Test]
        public void Result_has_exception_should_be_true_if_has_exception()
        {
            var result = Result<int>.Error(new Exception());
            Assert.That(result.HasException, Is.True);
        }
        [Test]
        public void Result_has_exception_should_be_false_if_no_exception()
        {
            var result = Result<int>.Ok(1);
            Assert.That(result.HasException, Is.False);
        }

        [Test]
        public void ResultTry_should_return_valued_result_when_success()
        {
            var result = Result<int>.Try(() => 2);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.HasException, Is.False);
            Assert.That(result.ValueOrThrow, Is.EqualTo(2));
        }

        [Test]
        public void ResultTry_should_return_exception_result_when_failed()
        {
            var x = 1;
            var result = Result<int>.Try(() => 100 / (1 - x));
            Assert.That(result.HasValue, Is.False);
            Assert.That(result.HasException, Is.True);
            Assert.That(result.Exception, Is.TypeOf<DivideByZeroException>());
        }

        [Test]
        public void ResultTryThrow_should_throw_if_exception()
        {
            var result = Result<int>.Error(new Exception("the error"));
            Assert.Throws<Exception>(() => result.TryThrow());
        }

        [Test]
        public void ResultTryThrow_should_not_throw_if_not_exception()
        {
            var result = Result<int>.Ok(5);
            Assert.DoesNotThrow(() => result.TryThrow());
        }
    }
}
