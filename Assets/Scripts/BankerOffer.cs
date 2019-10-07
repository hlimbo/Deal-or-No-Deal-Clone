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
    void Awake()
    {
        gameState = Resources.Load<GameState>("GameStateData");
        offerValue = transform.Find("OfferValue")?.GetComponent<Text>();
        gameOverPanel = GameObject.Find("GameOverPanel");
        instructionPanel = GameObject.Find("InstructionPanel");
        chosenSuitcase = FindObjectOfType<ChosenSuitcaseDisplay>();
    }
    void Start()
    {
        gameOverPanel.SetActive(false);
        gameObject.SetActive(false);
    }
    public void Deal()
    {
        chosenSuitcase.transform.Find("Closed").gameObject.SetActive(false);
        gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        gameState.totalEarnings = gameState.currentBankerOffer;
        gameOverPanel.transform.Find("EarningsText").GetComponent<Text>().text = $"You Won {gameState.totalEarnings.ToString("C")}";
        gameState.isBankerPresentingAnOffer = false;
        gameState.isGameOver = true;
    }

    public void NoDeal()
    {
        gameState.isBankerPresentingAnOffer = false;
        if (gameState.totalSuitcasesOpened < GameConstants.SUITCASE_COUNT - 1)
        {
            gameState.GotoNextRound();
            gameObject.SetActive(false);
            instructionPanel.SetActive(true);
            instructionPanel.GetComponent<InstructionalTextDisplay>().UpdateText();
        }
        else
        {
            gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
            chosenSuitcase.transform.Find("Closed").gameObject.SetActive(false);
            gameState.totalEarnings = chosenSuitcase.SuitcaseData.moneyAmount;
            gameOverPanel.transform.Find("EarningsText").GetComponent<Text>().text = $"You Won {gameState.totalEarnings.ToString("C")}";
            gameState.isGameOver = true;
        }
    }

    public void SetOfferValueText()
    {
        if (offerValue != null)
            offerValue.text = $"{gameState.currentBankerOffer.ToString("C")}";
    }

}
