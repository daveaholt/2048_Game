using _2048;
using NUnit.Framework;
using Philotechnia.interfaces;

namespace Tests
{
    public class Game_2048Tests
    {
        private IGame unit;

        [SetUp]
        public void Setup()
        {
            unit = new Game_2048();
        }

        [Test]
        public void ShouldConstruct ()
        {
            Assert.That(unit.CurrentScore, Is.EqualTo(0));
            Assert.That(unit.CurrentScore, Is.EqualTo(0));
        }
    }
}