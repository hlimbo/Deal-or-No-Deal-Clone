using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SuitcaseEventTrigger : EventTrigger
{
    public HighlightColors highlightColors;
    private SuitcaseDisplay suitcaseDisplay;
    private Color originalColor;
    private Image image;
    private static bool isPickingFirstSuitcase = true;

    // External Dependencies
    private ChosenSuitcaseDisplay chosenSuitcase;
    private Banker banker;
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
        if (isPickingFirstSuitcase)
        {
            // initialize chosen suitcase with valid number and money amount
            chosenSuitcase.SuitcaseData = suitcaseDisplay.suitcase;
            suitcaseDisplay.gameObject.SetActive(false);
            isPickingFirstSuitcase = false;

            banker.DecrementSuitcaseCount();
            banker.ReduceTotalMoneyAmount(suitcaseDisplay.suitcase.moneyAmount);
            banker.CalculateOffer(0.15f);
        }
        else
        {
            banker.DecrementSuitcaseCount();
            banker.ReduceTotalMoneyAmount(suitcaseDisplay.suitcase.moneyAmount);
            banker.CalculateOffer(0.15f);

            // reveal money amount suitcase contained
            suitcaseDisplay.transform.Find("Closed").gameObject.SetActive(false);
        }
    }
}
