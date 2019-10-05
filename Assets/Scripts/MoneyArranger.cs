using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyArranger : MonoBehaviour
{
    public const int SUITCASE_COUNT = 26;
    public Suitcase[] suitcases = new Suitcase[SUITCASE_COUNT];
    private static readonly float[] moneyAmounts = new float[SUITCASE_COUNT]
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

    void Start()
    {
        LoadSuitcaseData();
        ShuffleMoneyAmountsInSuitcases();
    }

    public void LoadSuitcaseData()
    {
        for (int i = 0; i < SUITCASE_COUNT; ++i)
        {
            suitcases[i] = Resources.Load<Suitcase>($"SuitcaseData/Suitcase{i + 1}");
        }
    }
    public void ShuffleMoneyAmountsInSuitcases()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < SUITCASE_COUNT; ++i)
            availableIndices.Add(i);

        // assign a random monetary amount per suitcase ensuring that the same monetary value doesn't get assigned multiple times for different suitcase so's
        for(int i = 0;i < SUITCASE_COUNT;++i)
        {
            int randomMoneyIndex = availableIndices[Random.Range(0, availableIndices.Count)];
            if(availableIndices.Remove(randomMoneyIndex))
            {
                suitcases[i].moneyAmount = moneyAmounts[randomMoneyIndex];
            }
        }
    }

}
