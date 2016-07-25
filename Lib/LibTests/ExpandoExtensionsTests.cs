using NUnit.Framework;
using System.Dynamic;
using Lib.Extensions;

namespace LibTests.Extensions
{
    [TestFixture]
    public class ExpandoExtensionsTests
    {
        [Test]
        public void HasProperty_should_return_true_if_object_has_property()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = "hello";

            var result = ((ExpandoObject)foo).HasProperty("bar");
            Assert.True(result);
        }

        [Test]
        public void HasProperty_should_return_false_if_object_doesnt_have_property()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = "hello";

            var result = ((ExpandoObject)foo).HasProperty("baz");
            Assert.False(result);
        }

        [Test]
        public void HasProperty_should_return_false_if_object_is_null()
        {
            var result = ((ExpandoObject)null).HasProperty("baz");
            Assert.False(result);
        }

        [Test]
        public void GetPropertyValue_should_return_property_value()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = "hello";

            var result = ((ExpandoObject)foo).GetPropertyValue("bar");
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void GetPropertyValue_should_return_null_if_property_doesnt_exist()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = "hello";

            var result = ((ExpandoObject)foo).GetPropertyValue("baz");
            Assert.That(result, Is.Null);
        }


        [Test]
        public void GetPropertyValue_should_return_null_if_object_null()
        {
            var result = ((ExpandoObject)null).GetPropertyValue("bar");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Query_should_return_property_value()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = new ExpandoObject();
            foo.bar.baz = "hello";

            var result = ((ExpandoObject)foo).Query<string>("bar.baz");
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void Query_should_return_null_if_property_doesnt_exist()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = new ExpandoObject();
            foo.bar.baz = "hello";

            var result = ((ExpandoObject)foo).Query<string>("bar.quux");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Query_should_return_null_if_it_cannot_traverse_expandos()
        {
            dynamic foo = new ExpandoObject();
            foo.bar = new
            {
                baz = "hello"
            };

            var result = ((ExpandoObject)foo).Query<string>("bar.baz");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Query_should_return_null_if_object_null()
        {
            var result = ((ExpandoObject)null).Query<string>("bar.quux");
            Assert.That(result, Is.Null);
        }
    }
}
