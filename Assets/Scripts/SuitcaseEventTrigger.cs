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
    private GameObject instructionalPanel;

    void Start()
    {
        suitcaseDisplay = GetComponent<SuitcaseDisplay>();
        image = transform.Find("Closed")?.GetComponent<Image>();
        highlightColors = Resources.Load<HighlightColors>("SuitcaseData/Colors");
        if(image != null)
        {
            originalColor = image.color;
        }

        chosenSuitcase = FindObjectOfType<ChosenSuitcaseDisplay>();
        banker = FindObjectOfType<Banker>();
        // I should move this logic somewhere else
        bankerOfferPanel = GameObject.Find("BankerOfferPanel");
        //bankerOfferPanel.SetActive(false);
        instructionalPanel = GameObject.Find("InstructionPanel");
        gameState = Resources.Load<GameState>("GameStateData");
        gameState.SetInitialState();

    }

    public override void OnPointerEnter(PointerEventData data)
    {
        Debug.Log($"OnPointerEnter called: {gameObject.name}");
        if (image != null)
        {
            image.color = highlightColors.hoverElement;
        }
    }

    public override void OnPointerExit(PointerEventData data)
    {
        Debug.Log($"OnPointerExit called: {gameObject.name}");
        if(image != null)
        {
            image.color = originalColor;
        }
    }

    public override void OnPointerDown(PointerEventData data)
    {
        Debug.Log($"OnPointerDown called: {gameObject.name}");
        if(image != null)
        {
            image.color = highlightColors.selectedElement;
        }
    }

    public override void OnPointerUp(PointerEventData data)
    {
        Debug.Log($"OnPointerUp called: {gameObject.name}");
        if(image != null)
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

            banker.DecrementSuitcaseCount();
            banker.ReduceTotalMoneyAmount(suitcaseDisplay.suitcase.moneyAmount);
        }
        else
        {
            // Open Suitcase Event
            if (suitcaseDisplay.transform.Find("Closed").gameObject.activeInHierarchy)
            {
                banker.DecrementSuitcaseCount();
                banker.ReduceTotalMoneyAmount(suitcaseDisplay.suitcase.moneyAmount);
                // reveal money amount suitcase contained
                suitcaseDisplay.transform.Find("Closed").gameObject.SetActive(false);
                ++gameState.currentSuitcasesOpenedCount;
                ++gameState.totalSuitcasesOpened;
            }

            if (GameConstants.casesToOpenPerRound[gameState.currentRoundNumber] <= gameState.currentSuitcasesOpenedCount)
            {
                // Present Banker Offer Event
                // open banker offer prompt that contains deal AND no deal buttons
                gameState.currentBankerOffer = banker.CalculateOffer(0.15f);
                bankerOfferPanel.SetActive(true);
                bankerOfferPanel.GetComponent<BankerOffer>().SetOfferValueText();
                instructionalPanel.SetActive(false);
            }
        }
    }
}
