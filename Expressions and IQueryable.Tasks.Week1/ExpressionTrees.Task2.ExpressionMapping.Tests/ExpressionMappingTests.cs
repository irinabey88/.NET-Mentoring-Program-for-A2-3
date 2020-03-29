using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        [DataTestMethod]
        [DataRow("TestCountry", "TestName", 145)]
        [DataRow("Country", "Name", 76)]
        [DataRow("Test", "Test", 89)]
        public void PropertiesValues_Mapped_Correct(string country, string name, int age)
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var sourceObj = new Foo();
            sourceObj.Country = country;
            sourceObj.Age = age;
            sourceObj.Name = name;

            Bar res = mapper.Map(sourceObj);

            Assert.AreEqual(sourceObj.Name, res.Name);
            Assert.AreEqual(sourceObj.Country, res.Country);
            Assert.AreEqual(sourceObj.Age, res.Age);
        }

        [TestMethod]
        public void PropertiesTypes_Mapped_Correct()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var sourceObj = new Foo();
            sourceObj.Country = "TestCountry";
            sourceObj.Age = 135;
            sourceObj.Name = "TestName";

            Bar res = mapper.Map(sourceObj);

            Assert.AreEqual(sourceObj.Name.GetType(), res.Name.GetType());
            Assert.AreEqual(sourceObj.Country, res.Country);
            Assert.AreEqual(sourceObj.Age.GetType(), res.Age.GetType());
        }

        [TestMethod]
        public void Properties_MappedNotToDefaultTypeValues_Correct()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var sourceObj = new Foo();
            sourceObj.Country = "TestCountry";
            sourceObj.Age = 135;
            sourceObj.Name = "TestName";

            Bar res = mapper.Map(sourceObj);

            Assert.AreNotEqual(res.Name, default);
            Assert.AreNotEqual(res.Country, default);
            Assert.AreNotEqual(res.Age, default);
        }
    }
}
