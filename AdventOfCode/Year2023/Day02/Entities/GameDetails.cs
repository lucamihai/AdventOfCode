using System.Text;

namespace AdventOfCode.Year2023.Day02.Entities;

public class GameDetails
{
    public int Id { get; set; }
    public Dictionary<int, Dictionary<string, int>> ColorCubesCountPerRound { get; set; } = new();
    public Dictionary<string, int> MinimumCubesRequiredPerColor { get; set; } = new();

    public void ComputeMinimumCubesRequiredPerColor(ICollection<string> colors)
    {
        MinimumCubesRequiredPerColor.Clear();

        foreach (var color in colors)
        {
            MinimumCubesRequiredPerColor.Add(color, 0);
        }

        foreach (var roundDetails in ColorCubesCountPerRound.Values)
        {
            foreach (var color in colors)
            {
                if (!roundDetails.ContainsKey(color))
                {
                    continue;
                }

                var currentCount = roundDetails[color];

                if (currentCount > MinimumCubesRequiredPerColor[color])
                {
                    MinimumCubesRequiredPerColor[color] = currentCount;
                }
            }
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Game {Id}: ");

        foreach (var round in ColorCubesCountPerRound.Keys)
        {
            var roundDetails = ColorCubesCountPerRound[round];

            stringBuilder.Append(string.Join(", ", roundDetails.Select(x => $"{x.Value} {x.Key}").ToList()));
            stringBuilder.Append("; ");
        }

        return stringBuilder.ToString();
    }
}