using System.Collections.Generic;
using Lib.Extensions;
using NUnit.Framework;

namespace LibTests.Extensions
{
    [TestFixture]
    class EnumerableExtensions
    {
        [Test]
        public void FirstOrNone_should_get_match()
        {
            var listA = new List<int> { 1, 2, 3, 4 };

            var result = listA.FirstOrNone(x => x == 2);

            Assert.That(result.ValueOrThrow, Is.EqualTo(2));
        }

        [Test]
        public void FirstOrNone_should_get_none_if_no_match()
        {
            var listA = new List<int> { 1, 2, 3, 4 };

            var result = listA.FirstOrNone(x => x == 5);

            Assert.That(result.IsNone, Is.True);
        }

        [Test]
        public void Diff_should_get_same_elements()
        {
            var listA = new List<int> { 1, 2, 3, 4 };
            var listB = new List<int> { 1, 2, 3, 4 };

            var result = listA.Diff(listB);

            Assert.AreEqual(result.Same.Count, 4);
            Assert.AreEqual(result.Same[0], listA[0]);
            Assert.AreEqual(result.Same[1], listA[1]);
            Assert.AreEqual(result.Same[2], listA[2]);
            Assert.AreEqual(result.Same[3], listA[3]);

            Assert.AreEqual(result.Removed.Count, 0);
            Assert.AreEqual(result.Added.Count, 0);
        }

        [Test]
        public void Diff_should_get_removed_elements()
        {
            var listA = new List<int> { 1, 2, 3, 4, 5, 6 };
            var listB = new List<int> { 1, 2, 3, 4 };

            var result = listA.Diff(listB);

            Assert.AreEqual(result.Same.Count, 4);
            Assert.AreEqual(result.Same[0], listA[0]);
            Assert.AreEqual(result.Same[1], listA[1]);
            Assert.AreEqual(result.Same[2], listA[2]);
            Assert.AreEqual(result.Same[3], listA[3]);

            Assert.AreEqual(result.Removed.Count, 2);
            Assert.AreEqual(result.Removed[0], listA[4]);
            Assert.AreEqual(result.Removed[1], listA[5]);

            Assert.AreEqual(result.Added.Count, 0);
        }

        [Test]
        public void Diff_should_get_added_elements()
        {
            var listA = new List<int> { 1, 2, 3, 4, 5, 6 };
            var listB = new List<int> { 1, 2, 3, 4, 7, 8 };

            var result = listA.Diff(listB);

            Assert.AreEqual(result.Same.Count, 4);
            Assert.AreEqual(result.Same[0], listA[0]);
            Assert.AreEqual(result.Same[1], listA[1]);
            Assert.AreEqual(result.Same[2], listA[2]);
            Assert.AreEqual(result.Same[3], listA[3]);

            Assert.AreEqual(result.Removed.Count, 2);
            Assert.AreEqual(result.Removed[0], listA[4]);
            Assert.AreEqual(result.Removed[1], listA[5]);

            Assert.AreEqual(result.Added.Count, 2);
            Assert.AreEqual(result.Added[0], listB[4]);
            Assert.AreEqual(result.Added[1], listB[5]);
        }
    }
}
