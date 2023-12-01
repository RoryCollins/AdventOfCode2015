namespace Day12;

using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

public class Solution
{
    private string input;

    public Solution(string input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        return SumNumbersInString(input);
    }

    private object SumNumbersInString(string s)
    {
        var regex = new Regex(@"(-?\d+)");
        var matches = regex.Matches(s);
        return matches.Sum(m => int.Parse(m.Value));
    }

    public object PartTwo()
    {
        var root = JsonNode.Parse(input)!.Root.AsObject();
        var jsonResult = ProcessJsonObject(root, true);

        return SumNumbersInString(jsonResult.ToString());
    }

    private static JsonObject ProcessJsonObject(JsonObject node, bool haveAtIt)
    {
        var result = new JsonObject();
        for (var i = 'a'; i <= 'z'; i++)
        {
            if (node[i.ToString()] == null)
            {
                continue;
            }

            var jsonNode = node[i.ToString()]!;

            var type = jsonNode.GetType();
            if (type == typeof(JsonObject))
            {
                result.Add(i.ToString(), ProcessJsonObject(jsonNode.AsObject(), haveAtIt));
                continue;
            }

            if (type == typeof(JsonArray))
            {
                result.Add(i.ToString(), ProcessJsonArray(jsonNode.AsArray(), haveAtIt));
                continue;
            }

            if (haveAtIt && jsonNode.AsValue()
                    .ToString() == "red")
            {
                return new JsonObject();
            }

            result.Add(i.ToString(), jsonNode.AsValue()
                .ToString());
        }

        return result;
    }

    private static JsonArray ProcessJsonArray(JsonArray node, bool haveAtIt)
    {
        var result = new JsonArray();
        foreach (var t in node)
        {
            var jsonNode = t!;

            var type = jsonNode.GetType();
            if (type == typeof(JsonObject))
            {
                result.Add(ProcessJsonObject(jsonNode.AsObject(), haveAtIt));
                continue;
            }

            if (type == typeof(JsonArray))
            {
                result.Add(ProcessJsonArray(jsonNode.AsArray(), haveAtIt));
                continue;
            }

            result.Add(jsonNode.AsValue()
                .ToString());
        }

        return result;
    }
}