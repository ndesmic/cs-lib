using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Extensions;
using NUnit.Framework;

namespace LibTests.Extensions
{
    [TestFixture]
    public class TypeExtensions
    {
        [Test]
        public void IsNullableType_should_return_true_if_nullable()
        {
            var result = typeof(int?).IsNullableType();
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullableType_should_return_false_if_not_nullable()
        {
            var result = typeof(int).IsNullableType();
            Assert.That(result, Is.False);
        }
    }
}
