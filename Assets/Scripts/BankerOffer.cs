using UnityEngine;
using UnityEngine.UI;
public class BankerOffer : MonoBehaviour
{
    [SerializeField]
    private GameState gameState;
    private GameObject gameOverPanel;
    private GameObject instructionPanel;
    private ChosenSuitcaseDisplay chosenSuitcase;
    private Text offerValue;
    void Start()
    {
        gameState = Resources.Load<GameState>("GameStateData");
        offerValue = transform.Find("OfferValue")?.GetComponent<Text>();
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
        instructionPanel = GameObject.Find("InstructionPanel");
        chosenSuitcase = FindObjectOfType<ChosenSuitcaseDisplay>();
    }
    public void Deal()
    {
        chosenSuitcase.transform.Find("Closed").gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void NoDeal()
    {
        if(gameState.totalSuitcasesOpened < GameConstants.SUITCASE_COUNT - 1)
        {
            gameState.GotoNextRound();
            gameObject.SetActive(false);
            instructionPanel.SetActive(true);
            instructionPanel.GetComponent<InstructionalTextDisplay>().SetCurrentText();
        }
        else
        {
            gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
            chosenSuitcase.transform.Find("Closed").gameObject.SetActive(false);
        }

    }

    public void SetOfferValueText()
    {
        if (offerValue != null)
            offerValue.text = $"${gameState.currentBankerOffer}";
    }

}
