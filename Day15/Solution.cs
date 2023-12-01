namespace Day15;

public class Solution
{
    private readonly List<Ingredient> ingredients;
    private readonly IngredientAmount[][] matrix;

    public Solution(IEnumerable<string> input)
    {
        this.ingredients = input.Select(Ingredient.From)
            .ToList();
        matrix = new IngredientAmount[ingredients.Count][];
        for(var i = 0; i < ingredients.Count; i++)
        {
            matrix[i] = new IngredientAmount[101];
            for (var j = 0; j <= 100; j++)
            {
                matrix[i][j] = ingredients[i].Times(j);
            }
        }

    }

    public object PartOne()
    {
        return GetBestRemainingIngredients(100, IngredientAmount.Empty, matrix);
    }

    public object PartTwo()
    {
        return GetBestRemainingIngredients(100, IngredientAmount.Empty, matrix, 500);
    }

    private static int GetBestRemainingIngredients(int remainder, IngredientAmount current, IngredientAmount[][] options, int calorieLimit = 0)
    {
        if (options.Length == 1)
        {
            var total = current + options[0][remainder];
            return calorieLimit == 0 || calorieLimit == total.Calories
                ? total.Score
                : 0;
        }

        var possibilities = new List<int>();
        for (var i = 0; i <= remainder; i++)
        {
            possibilities.Add(GetBestRemainingIngredients(remainder-i, current+options[0][i], options[1..], calorieLimit));
        }

        return possibilities.Max()!;
    }
}