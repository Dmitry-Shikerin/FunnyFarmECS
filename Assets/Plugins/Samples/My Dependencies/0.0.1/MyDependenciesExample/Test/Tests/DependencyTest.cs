using System.Collections;
using MyDependencies.Sources.Test.Tests.Categories;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace MyDependencies.Sources.Test.Tests
{
    public class DependencyTest
    {
        [Test]
        public void DependecyTestSimplePasses()
        {
        }

        [UnityTest]
        [Category(TestCategory.InjectAttribute)]
        public IEnumerator DependecyTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
