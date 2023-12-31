using AdventOfCode.Year2023.Day04.Entities;

namespace AdventOfCode.Year2023.Day04;

public static class Solution
{
    public static void Solve()
    {
        var cards = GetCards();

        var totalScore = cards.Select(GetCardScore).Sum();
        Console.WriteLine($"The cards are worth {totalScore} points.");

        var newCards = GetCardsAccordingToNewRules(cards).ToList();
        Console.WriteLine($"The new cards are worth {newCards.Count} points.");
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

            cards.Add(new Card
            {
                WinningNumbers = cardInfoTokens[0].Trim().Split().Where(x => x != string.Empty).Select(int.Parse).ToHashSet(),
                PlayedNumbers = cardInfoTokens[1].Trim().Split().Where(x => x != string.Empty).Select(int.Parse).ToHashSet(),
            });
        }

        return cards;
    }

    private static List<Card> GetCardsAccordingToNewRules(List<Card> originalCards)
    {
        var newCards = new List<Card>();

        var cardsByIndex = new Dictionary<int, List<Card>>();

        for (int index = 0; index < originalCards.Count; index++)
        {
            cardsByIndex.Add(index, new List<Card> { originalCards[index] });
        }

        foreach (var a in cardsByIndex)
        {
            Help(cardsByIndex, originalCards, a.Value, a.Key);
        }

        foreach (var a in cardsByIndex)
        {
            newCards.AddRange(a.Value);
        }

        return newCards;
    }

    private static void Help(Dictionary<int, List<Card>> cardsByIndex, List<Card> originalCards, List<Card> currentCards, int id)
    {
        var card = currentCards[0];
        var cardScore = GetCardScore(card);

        if (cardScore == 0)
        {
            return;
        }

        var extraCardsToPick = (int)Math.Log2(cardScore) + 1;
        var extraCards = originalCards.Skip(id + 1).Take(extraCardsToPick).ToList();

        for (var i = 0; i < extraCards.Count; i++)
        {
            var extraCard = extraCards[i];
            var extraCardIndex = id + i + 1;

            for (var index = 0; index < currentCards.Count; index++)
            {
                cardsByIndex[extraCardIndex].Add(extraCard);
            }
        }
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