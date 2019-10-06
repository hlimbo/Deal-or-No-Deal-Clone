using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameState", menuName  = "ScriptableObject/GameState")]
public class GameState : ScriptableObject
{
    public bool isPickingFirstSuitcase;
    public int currentRoundNumber;
    public byte currentSuitcasesOpenedCount;
    public float totalEarnings;
    public float currentBankerOffer;
    public byte totalSuitcasesOpened;

    public void GotoNextRound()
    {
        currentSuitcasesOpenedCount = 0;
        currentRoundNumber = Mathf.Min(currentRoundNumber + 1, GameConstants.MAX_ROUND_COUNT);
    }

    public void SetInitialState()
    {
        isPickingFirstSuitcase = true;
        currentRoundNumber = 1;
        currentSuitcasesOpenedCount = 0;
        totalEarnings = 0;
        currentBankerOffer = 0;
        totalSuitcasesOpened = 0;
    }
}
