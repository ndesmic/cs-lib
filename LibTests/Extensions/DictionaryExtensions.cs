using System.Collections.Generic;
using Lib.Extensions;
using NUnit.Framework;

namespace LibTests.Extensions
{
    [TestFixture]
    public class DictionaryExtensions
    {
        [Test]
        public void Pick_should_create_a_subdictionary()
        {
            var dict = new Dictionary<string, string>
            {
                { "foo", "foo_value" },
                { "bar", "bar_value" },
                { "baz", "baz_value" }
            };
            var subDict = dict.Pick("foo", "baz");
            Assert.That(subDict.Count, Is.EqualTo(2));
            Assert.That(subDict["foo"], Is.EqualTo("foo_value"));
            Assert.That(subDict["baz"], Is.EqualTo("baz_value"));
        }
    }
}
