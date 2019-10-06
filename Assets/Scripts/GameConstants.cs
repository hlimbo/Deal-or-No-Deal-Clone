using System.Collections.Generic;
public static class GameConstants
{
    public const int SUITCASE_COUNT = 26;
    public static readonly float[] moneyAmounts = new float[SUITCASE_COUNT]
    {
        0.01f,
        1f,
        5f,
        10f,
        25f,
        50f,
        75f,
        100f,
        200f,
        300f,
        400f,
        500f,
        750f,
        1000f,
        5000f,
        10000f,
        25000f,
        50000f,
        75000f,
        100000f,
        200000f,
        300000f,
        400000f,
        500000f,
        750000f,
        1000000f
    };

    public const int MAX_ROUND_COUNT = 10;
    // key = round number | value = number of cases to open
    public static readonly Dictionary<int, int> casesToOpenPerRound = new Dictionary<int, int>()
    {
        { 1, 6 },
        { 2, 5 },
        { 3, 4 },
        { 4, 3 },
        { 5, 2 },
        { 6, 1 },
        { 7, 1 },
        { 8, 1 },
        { 9, 1 },
        { 10, 1 }
    };

    // key = round number | value = percentage banker is willing to offer to player from average money amount left in play
    public static readonly Dictionary<int, float> bankerOfferPercentagesPerRound = new Dictionary<int, float>()
    {
        { 1, 0.15f },
        { 2, 0.15f },
        { 3, 0.3f },
        { 4, 0.4f },
        { 5, 0.5f },
        { 6, 0.6f },
        { 7, 0.65f },
        { 8, 0.75f },
        { 9, 0.85f },
        { 10, 0.9f }
    };
}