using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SuitcaseEventTrigger : EventTrigger
{
    public HighlightColors highlightColors;
    private SuitcaseDisplay suitcaseDisplay;
    private Color originalColor;
    private Image image;

    // External Dependencies
    private ChosenSuitcaseDisplay chosenSuitcase;
    private Banker banker;
    private GameState gameState;
    private GameObject bankerOfferPanel;
    private GameObject instructionPanel;

    void Awake()
    {
        suitcaseDisplay = GetComponent<SuitcaseDisplay>();
        image = transform.Find("Closed")?.GetComponent<Image>();
        highlightColors = Resources.Load<HighlightColors>("SuitcaseData/Colors");
        if (image != null)
        {
            originalColor = image.color;
        }

        // external dependencies
        chosenSuitcase = FindObjectOfType<ChosenSuitcaseDisplay>();
        banker = FindObjectOfType<Banker>();
        bankerOfferPanel = GameObject.Find("BankerOfferPanel");
        instructionPanel = GameObject.Find("InstructionPanel");
        gameState = Resources.Load<GameState>("GameStateData");
        gameState.SetInitialState();
    }
    void Start()
    {
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        Debug.Log($"OnPointerEnter called: {gameObject.name}");

        if (image != null && !gameState.isBankerPresentingAnOffer)
        {
            image.color = highlightColors.hoverElement;
        }
    }

    public override void OnPointerExit(PointerEventData data)
    {
        Debug.Log($"OnPointerExit called: {gameObject.name}");
        if(image != null && !gameState.isBankerPresentingAnOffer)
        {
            image.color = originalColor;
        }
    }

    public override void OnPointerDown(PointerEventData data)
    {
        Debug.Log($"OnPointerDown called: {gameObject.name}");
        if(image != null && !gameState.isBankerPresentingAnOffer)
        {
            image.color = highlightColors.selectedElement;
        }
    }

    public override void OnPointerUp(PointerEventData data)
    {
        Debug.Log($"OnPointerUp called: {gameObject.name}");
        if(image != null && !gameState.isBankerPresentingAnOffer)
        {
            image.color = originalColor;
        }
    }

    public override void OnPointerClick(PointerEventData data)
    {
        Debug.Log($"OnPointerClick called: {gameObject.name}");
        if (gameState.isPickingFirstSuitcase)
        {
            // Select Initial Suitcase Event
            // initialize chosen suitcase with valid number and money amount
            chosenSuitcase.SuitcaseData = suitcaseDisplay.suitcase;
            suitcaseDisplay.gameObject.SetActive(false);
            gameState.isPickingFirstSuitcase = false;
            instructionPanel.GetComponent<InstructionalTextDisplay>().UpdateText();
        }
        else
        {
            // Open Suitcase Event
            if (!gameState.isBankerPresentingAnOffer && suitcaseDisplay.transform.Find("Closed").gameObject.activeInHierarchy)
            {
                banker.DecrementSuitcaseCount();
                banker.ReduceTotalMoneyAmount(suitcaseDisplay.suitcase.moneyAmount);
                // reveal money amount suitcase contained
                suitcaseDisplay.transform.Find("Closed").gameObject.SetActive(false);
                ++gameState.currentSuitcasesOpenedCount;
                ++gameState.totalSuitcasesOpened;
            }

            if (!gameState.isBankerPresentingAnOffer && GameConstants.casesToOpenPerRound[gameState.currentRoundNumber] <= gameState.currentSuitcasesOpenedCount)
            {
                // Present Banker Offer Event
                gameState.currentBankerOffer = banker.CalculateOffer(GameConstants.bankerOfferPercentagesPerRound[gameState.currentRoundNumber]);
                gameState.isBankerPresentingAnOffer = true;
                bankerOfferPanel.SetActive(true);
                bankerOfferPanel.GetComponent<BankerOffer>().SetOfferValueText();
                instructionPanel.SetActive(false);
            }
        }
    }
}
