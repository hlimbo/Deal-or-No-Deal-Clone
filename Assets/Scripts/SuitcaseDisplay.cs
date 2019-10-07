using UnityEngine;
using UnityEngine.UI;

public class SuitcaseDisplay : MonoBehaviour
{
    public Suitcase suitcase;
    private Text suitcaseNumber;
    private Text suitcaseMoneyAmount;
   
    // Start is called before the first frame update
    void Start()
    {
        suitcaseNumber = transform.Find("Closed/Text")?.GetComponent<Text>();
        suitcaseMoneyAmount = transform.Find("Open/Image/Text")?.GetComponent<Text>();
        if (suitcaseNumber != null) suitcaseNumber.text = suitcase.number.ToString();
        if (suitcaseMoneyAmount != null) suitcaseMoneyAmount.text = suitcase.moneyAmount.ToString("C");
    }
}
