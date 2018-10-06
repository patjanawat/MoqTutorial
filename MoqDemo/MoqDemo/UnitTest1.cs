using Moq;
using NUnit.Framework;

namespace MoqDemo {

    public interface IFoo {
        Bar Bar { get; set; }
        string Name { get; set; }
        int Value { get; set; }
        bool DoSomething(string value);
        bool DoSomething(int number,string value);
        string DoSomethingStringy(string value);
        bool TryParse(string value,out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int value);
    }

    public class Bar {
        public virtual Baz Baz { get; set; }
        public virtual bool Submit() { return false; }
    }

    public class Baz {
        public virtual string Name { get; set; }
    }

    [TestFixture]
    public class UnitTest1 {
        Mock<IFoo> mock = new Mock<IFoo>();

        [SetUp]
        public void Setup() {

        }
        [Test]
        public void TestMethod1() {            
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            bool actual = mock.Object.DoSomething("ping");

            Assert.AreEqual(true,actual);

        }

        [Test]
        public void OutTest() {
            var outString = "ack";
            mock.Setup(f => f.TryParse("ping",out outString)).Returns(true);

            bool actual = mock.Object.TryParse("ping",out outString);

            Assert.Multiple(()=> {
                Assert.IsTrue(actual,"return");
                Assert.AreEqual(outString,"ping","compare");
            });
        }
    }
}
