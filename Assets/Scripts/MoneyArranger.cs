using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyArranger : MonoBehaviour
{
    public Suitcase[] suitcases = new Suitcase[GameConstants.SUITCASE_COUNT];

    void Awake()
    {
        LoadSuitcaseData();
        ShuffleMoneyAmountsInSuitcases();
    }

    public void LoadSuitcaseData()
    {
        for (int i = 0; i < GameConstants.SUITCASE_COUNT; ++i)
        {
            suitcases[i] = Resources.Load<Suitcase>($"SuitcaseData/Suitcase{i + 1}");
        }
    }
    public void ShuffleMoneyAmountsInSuitcases()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < GameConstants.SUITCASE_COUNT; ++i)
            availableIndices.Add(i);

        // assign a random monetary amount per suitcase ensuring that the same monetary value doesn't get assigned multiple times for different suitcase so's
        foreach(var suitcase in suitcases)
        {
            int randomMoneyIndex = availableIndices[Random.Range(0, availableIndices.Count)];
            availableIndices.Remove(randomMoneyIndex);
            suitcase.moneyAmount = GameConstants.moneyAmounts[randomMoneyIndex];
        }
    }

}
