using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class RandomExtensionsTest {
        
        [Test]
        public void Test_Range_WhenNoArgumentsArePassed() {
            System.Random rand = new System.Random();

            float randomNumber = rand.Range();

            Assert.LessOrEqual(randomNumber, 1f);
            Assert.GreaterOrEqual(randomNumber, 0f);
        }

        [Test]
        public void Test_Range_WhenALowerBoundIsSet() {
            System.Random rand = new System.Random();

            float randomNumber = rand.Range(0.5f);

            Assert.LessOrEqual(randomNumber, 1f);
            Assert.GreaterOrEqual(randomNumber, 0.5f);
        }

        [Test]
        public void Test_Range_WhenAnUpperBoundIsSet() {
            System.Random rand = new System.Random();

            float randomNumber = rand.Range(max: 10f);

            Assert.LessOrEqual(randomNumber, 10f);
            Assert.GreaterOrEqual(randomNumber, 0f);
        }

        [Test]
        public void Test_Range_WhenALowerAndAnUpperBoundAreSet() {
            System.Random rand = new System.Random();

            float randomNumber = rand.Range(5f, 10f);

            Assert.LessOrEqual(randomNumber, 10f);
            Assert.GreaterOrEqual(randomNumber, 5f);
        }
    }
}
