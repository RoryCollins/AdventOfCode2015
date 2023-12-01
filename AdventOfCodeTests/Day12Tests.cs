namespace AdventOfCodeTests;

using global::Day12;

public class Day12Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", "")]
    [TestCase("{\"d\":[\"red\",5],\"e\":[1,2,3,4],\"f\":5}", "{\"d\":[,5],\"e\":[1,2,3,4],\"f\":5}")]
    [TestCase("[1,\"red\",5]", "[1,,5]")]
    [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", "[1,,3]")]
    [TestCase("[1,{\"boo\": 12,{\"a\": \"foo\"},\"c\":\"red\",\"b\":2},3]", "[1,,3]")]
    [TestCase("[1,{\"boo\": 12,{\"a\": \"foo\"},\"c\":\"red\",\"b\":2, {\"bar\": 13}},3]", "[1,,3]")]
    [TestCase("{\"a\":[1,{\"c\":\"red\",\"b\":2},3]}", "{\"a\":[1,,3]}")]
    [TestCase("{\"a\":108,\"b\":[\"red\",-48]}", "{\"a\":108,\"b\":[,-48]}")]
    public void RemoveRedsTest(string input, string expected)
    {
        Assert.That(Solution.RemoveReds(input), Is.EqualTo(expected));
    }
}