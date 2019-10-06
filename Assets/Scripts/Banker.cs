using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Banker responsiblities
/*
 * 1. Keep track of which suitcases were revealed
 * 2. calculate the average prize pool value
 * 3. calculate offer to make to player
 */
public class Banker : MonoBehaviour
{
    [SerializeField]
    private float totalMoneyAmount;
    [SerializeField]
    private int closedSuitcaseCount;
    [SerializeField]
    private float averageMoneyAmount;

    private ChosenSuitcaseDisplay chosenSuitcase;

    void Awake()
    {
        totalMoneyAmount = GameConstants.moneyAmounts.Sum();
        closedSuitcaseCount = GameConstants.SUITCASE_COUNT;
        chosenSuitcase = FindObjectOfType<ChosenSuitcaseDisplay>();
    }

    public void DecrementSuitcaseCount()
    {
        --closedSuitcaseCount;
    }

    public void ReduceTotalMoneyAmount(float moneyValue)
    {
        totalMoneyAmount -= moneyValue;
    }

    public float TotalMoneyAmount
    {
        get { return totalMoneyAmount; }
    }

    public float ClosedSuitcaseCount
    {
        get { return closedSuitcaseCount; }
    }

    public float AverageMoneyAmount
    {
        get
        {
            // it shouldn't be 0f .. it should be the FULL amount contained in the suitcase the player selected to keep
            averageMoneyAmount = closedSuitcaseCount > 0 ?
                totalMoneyAmount / closedSuitcaseCount :
                chosenSuitcase?.SuitcaseData?.moneyAmount ?? 0f;
            
            return averageMoneyAmount;
        }
    }

    public float CalculateOffer(float reducePercentage)
    {
        return AverageMoneyAmount * reducePercentage;
    }
}
