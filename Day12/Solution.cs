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
        var originalResult = ProcessJsonObject(root, false);
        // var resultText = RemoveReds(input)
            // .Replace(",,", ",")
            // .Replace("[,", "[")
            // .Replace("{,", "{")
            // .Replace(":,", ":{},");
        // var manualResult = ProcessJsonObject(JsonNode.Parse(resultText)!.AsObject(), false);

        File.WriteAllText("original.json", originalResult.ToJsonString());
        File.WriteAllText("resultJson.json", jsonResult.ToJsonString());
        // File.WriteAllText("resultText.json", manualResult.ToJsonString());

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

            if (haveAtIt && jsonNode.AsValue().ToString() == "red")
            {
                return new JsonObject();
            }
            result.Add(i.ToString(), jsonNode.AsValue().ToString());
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

    public static string RemoveReds(string s)
    {
        s = s.Replace(",\"red\"", ",\"xxx\"")
            .Replace("\"red\"]", ",\"xxx\"]")
            .Replace("[\"red\"", "[\"xxx\"");
        int index;
        while (true)
        {
            index = s.IndexOf(":\"red\"", StringComparison.Ordinal);
            if (index == -1) break;
            var prefix = s.Substring(0, index);
            var suffix = s.Substring(index);

            var lastOpenCurly = FindLastUnclosed('{', '}', prefix);
            var closure = FindFirstClosed('{', '}', suffix);

            var count = closure + (index - lastOpenCurly) + 1;
            s = s.Remove(lastOpenCurly, count);
        }

        return s;
    }

    public static int FindLastUnclosed(char open, char closure, string s)
    {
        var lastOpen = s.LastIndexOf(open);
        var lastCloseCurly = s.LastIndexOf(closure);
        return lastOpen > lastCloseCurly || (lastOpen == -1)
            ? lastOpen
            : FindLastUnclosed(open, closure, s[..lastOpen]);
    }

    private static int FindFirstClosed(char open, char closure, string s)
    {
        var firstClose = s.IndexOf(closure);
        var firstOpen = s.IndexOf(open);
        return firstClose < firstOpen || (firstClose == -1) || (firstOpen == -1)
            ? firstClose
            : (firstClose + 1) + FindFirstClosed(open, closure, s[(firstClose + 1)..]);
    }
}