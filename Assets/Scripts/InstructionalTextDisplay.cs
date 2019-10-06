using UnityEngine;
using UnityEngine.UI;

public class InstructionalTextDisplay : MonoBehaviour
{
    private GameState gameState;
    private Text instructionText;
    
    void Awake()
    {
        gameState = Resources.Load<GameState>("GameStateData");
        instructionText = transform.Find("Text")?.GetComponent<Text>();
    }

    // set the instructional text everytime player 
    // selects no deal
    // picks the initial suitcase to keep
    public void UpdateText()
    {
        if(instructionText != null)
        {
            instructionText.text = gameState.isPickingFirstSuitcase ?
                "Pick a suitcase" : OpenXSuitcasesText(GameConstants.casesToOpenPerRound[gameState.currentRoundNumber]);
        }
    }
    private string OpenXSuitcasesText(int suitcaseCount)
    {
        return suitcaseCount > 1 ?
            $"Open {suitcaseCount} suitcases" :
            $"Open {suitcaseCount} suitcase";
    }
}
