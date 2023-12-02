using System.Text;

namespace AdventOfCode.Year2023.Day02.Entities;

public class GameDetails
{
    public int Id { get; set; }
    public List<RoundDetails> Rounds { get; set; } = new();
    public Dictionary<string, int> MinimumCubesRequiredPerColor { get; set; } = new();

    public void ComputeMinimumCubesRequiredPerColor(ICollection<string> colors)
    {
        MinimumCubesRequiredPerColor.Clear();

        foreach (var color in colors)
        {
            MinimumCubesRequiredPerColor.Add(color, 0);
        }

        foreach (var round in Rounds)
        {
            foreach (var color in colors)
            {
                if (!round.ColorCubesCount.TryGetValue(color, out var currentCount))
                {
                    continue;
                }

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

        foreach (var round in Rounds)
        {
            stringBuilder.Append(string.Join(", ", round.ColorCubesCount.Select(x => $"{x.Value} {x.Key}").ToList()));
            stringBuilder.Append("; ");
        }

        return stringBuilder.ToString();
    }
}