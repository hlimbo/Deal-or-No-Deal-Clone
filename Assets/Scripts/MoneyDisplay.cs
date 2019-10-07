using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public float moneyAmount;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        transform.Find("Text").GetComponent<Text>().text = moneyAmount.ToString("C");
    }

    public void FadeoutDisplay()
    {
        image.color = Color.black;
    }
}
