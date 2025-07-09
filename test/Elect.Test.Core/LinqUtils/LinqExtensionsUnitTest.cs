namespace Elect.Test.Core.LinqUtils
{
    [TestClass]
    public class LinqExtensionsUnitTest
    {
        [TestMethod]
        public void DistinctBy_ReturnsDistinctElements()
        {
            var list = new[] { "a", "bb", "cc", "ddd", "ee", "fff" };
            var result = Elect.Core.LinqUtils.LinqExtensions.DistinctBy(list, x => x.Length).ToList();
            CollectionAssert.AreEqual(new[] { "a", "bb", "ddd" }, result);
        }
        [TestMethod]
        public void RemoveWhere_RemovesMatchingElements()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            var result = list.RemoveWhere(x => x % 2 == 0).ToList();
            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, result);
        }
        [TestMethod]
        public void TakeUntil_StopsAtEndCondition()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            var result = list.TakeUntil(x => x == 4).ToList();
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
        }
        [TestMethod]
        public void WherePrevious_ReturnsElementsMatchingPredicate()
        {
            var list = new List<int> { 1, 5, 7, 3, 10, 9, 6 };
            var result = list.WherePrevious((first, second) => second > first).ToList();
            CollectionAssert.AreEqual(new[] { 5, 7, 10 }, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WherePrevious_NullCollection_Throws()
        {
            List<int> list = null;
            list.WherePrevious((a, b) => true).ToList();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WherePrevious_NullPredicate_Throws()
        {
            var list = new List<int> { 1, 2 };
            list.WherePrevious(null).ToList();
        }
        [TestMethod]
        public void Randomize_ReturnsAllElementsInRandomOrder()
        {
            var list = Enumerable.Range(1, 100).ToList();
            var randomized = list.Randomize().ToList();
            // Should contain all elements
            CollectionAssert.AreEquivalent(list, randomized);
            // Should not always be in the same order
            bool isSameOrder = list.SequenceEqual(randomized);
            // This could rarely fail if random returns the same order, but is unlikely
            Assert.IsFalse(isSameOrder, "Randomize did not change the order (very unlikely)");
        }

        [TestMethod]
        public void Randomize_EmptyAndSingleElementCollections()
        {
            var empty = new int[0];
            var single = new[] { 42 };
            CollectionAssert.AreEqual(empty, empty.Randomize().ToArray());
            CollectionAssert.AreEqual(single, single.Randomize().ToArray());
        }

        [TestMethod]
        public void ToListWithCapacity_ReturnsListWithElements()
        {
            var source = new[] { 1, 2, 3 };
            var result = source.ToList(10);
            CollectionAssert.AreEqual(source, result);
            Assert.IsTrue(result.Capacity >= 3);
        }

        [TestMethod]
        public void ToListWithCapacity_NullSource_ReturnsNull()
        {
            int[] source = null;
            var result = source.ToList(5);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToListWithCapacity_NegativeCapacity_Throws()
        {
            var source = new[] { 1, 2 };
            source.ToList(-1);
        }
    }
}
