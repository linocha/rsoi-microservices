using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests.Utils.AssertHelpers
{
    public class AssertHelperBase
    {
        public static void AssertEqualLists<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> assertFunction)
        {
            Assert.Equal(collection1.Count(), collection2.Count());
            for (int i = 0; i < collection1.Count(); i++)
            {
                assertFunction(collection1.ElementAt(i), collection2.ElementAt(i));
            }
        }
    }
}