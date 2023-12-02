using System.Text;

namespace AdventOfCode.Year2023.Day02.Entities;

public class GameDetails
{
    public int Id { get; set; }
    public Dictionary<int, Dictionary<string, int>> ColorCubesCountPerRound { get; set; } = new();

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