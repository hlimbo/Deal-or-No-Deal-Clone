using UnityEngine;
using UnityEngine.UI;

public class ChosenSuitcaseDisplay : MonoBehaviour
{
    private Text suitcaseNumber;
    private Text suitcaseMoneyAmount;
    private Suitcase suitcaseData;
    public Suitcase SuitcaseData
    {
        get { return suitcaseData; }
        set
        {
            suitcaseData = value;
            suitcaseNumber.text = suitcaseData.number.ToString();
            suitcaseMoneyAmount.text = suitcaseData.moneyAmount.ToString();
        }
    }

    void Awake()
    {
        suitcaseNumber = transform.Find("Closed/Text")?.GetComponent<Text>();
        suitcaseMoneyAmount = transform.Find("Open/Image/Text")?.GetComponent<Text>();
    }
}
