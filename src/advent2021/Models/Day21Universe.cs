namespace advent2021.Models;
public class Day21Universe
{
    // Dice rolls three times per turn, equally likely to return 1, 2, or 3. After 3 rolls, the observation count for each possible sum:
    public static Dictionary<int, int> diceProbabilities { get; } = new Dictionary<int, int>() {
                {3 , 1},
                {4 , 3},
                {5 , 6},
                {6 , 7},
                {7 , 6},
                {8 , 3},
                {9 , 1},
        };

    public static bool GameOver(int p1Score, int p2Score) => p1Score >= 21 || p2Score >= 21;
    public static bool PlayerOneWon(int p1Score) => p1Score >= 21;

    public Day21Universe(int playerOnePosition, int playerTwoPosition, int playerOneScore, int playerTwoScore, bool playerOneIsNext, long startingPrevalence)
    {
        int playerScore, playerPosition;

        if (playerOneIsNext)
        {
            playerScore = playerOneScore;
            playerPosition = playerOnePosition;
        }
        else
        {
            playerScore = playerTwoScore;
            playerPosition = playerTwoPosition;
        }

        foreach (var key in diceProbabilities.Keys)
        {
            var newPosition = (key + playerPosition) % 10;
            var newScore = playerScore + newPosition + 1;
            var newPrevalence = startingPrevalence * diceProbabilities[key];

            if (GameOver(playerOneIsNext ? newScore : playerOneScore, !playerOneIsNext ? newScore : playerTwoScore))
            {
                if (PlayerOneWon(playerOneIsNext ? newScore : playerOneScore))
                {
                    p1Wins += newPrevalence;
                }
                else
                {
                    p2Wins += newPrevalence;
                }
            }
            else
            {
                var nextUniverse = new Day21Universe(
                    playerOneIsNext ? newPosition : playerOnePosition,
                    !playerOneIsNext ? newPosition : playerTwoPosition,
                    playerOneIsNext ? newScore : playerOneScore,
                    !playerOneIsNext ? newScore : playerTwoScore,
                    !playerOneIsNext,
                    newPrevalence);

                p1Wins += nextUniverse.p1Wins;
                p2Wins += nextUniverse.p2Wins;
            }
        }
    }
    public long p1Wins { get; set; } = 0;
    public long p2Wins { get; set; } = 0;
}