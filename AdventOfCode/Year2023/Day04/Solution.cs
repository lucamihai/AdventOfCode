using AdventOfCode.Year2023.Day04.Entities;

namespace AdventOfCode.Year2023.Day04;

public static class Solution
{
    public static void Solve()
    {
        var cards = GetCards();

        var totalScore = cards.Select(GetCardScore).Sum();
        Console.WriteLine($"The cards are worth {totalScore} points.");
    }

    private static List<Card> GetCards()
    {
        var path = Path.Combine("Year2023", "Day04", "Input.txt");
        var inputLines = File.ReadAllLines(path);

        var cards = new List<Card>();

        foreach (var inputLine in inputLines)
        {
            var cardInfo = inputLine.Split(':')[1];
            var cardInfoTokens = cardInfo.Split("|");

            var a1 = cardInfoTokens[0].Trim();
            var b1 = cardInfoTokens[0].Trim().Split().Where(x => x != string.Empty).ToList();

            var a2 = cardInfoTokens[1].Trim();
            var b2 = cardInfoTokens[1].Trim().Split().Where(x => x != string.Empty).ToList();

            cards.Add(new Card
            {
                WinningNumbers = cardInfoTokens[0].Trim().Split().Where(x => x != string.Empty).Select(int.Parse).ToHashSet(),
                PlayedNumbers = cardInfoTokens[1].Trim().Split().Where(x => x != string.Empty).Select(int.Parse).ToHashSet(),
            });
        }

        return cards;
    }

    private static int GetCardScore(Card card)
    {
        var chosenWinningNumbersCount = card.PlayedNumbers.Count(card.WinningNumbers.Contains);

        if (chosenWinningNumbersCount == 0)
        {
            return 0;
        }

        return (int)Math.Pow(2, chosenWinningNumbersCount - 1);
    }
}