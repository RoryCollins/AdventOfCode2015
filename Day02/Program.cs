using Day02;

var input = File.ReadLines("input.txt");
var solution = new Solution(input);

Console.WriteLine($"Part one: {solution.PartOne()}");
Console.WriteLine($"Part two: {solution.PartTwo()}");