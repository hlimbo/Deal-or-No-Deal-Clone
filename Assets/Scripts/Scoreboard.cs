using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private MoneyDisplay[] displays;
    public MoneyDisplay[] Displays
    {
        get { return displays; }
    }
    void Awake()
    {
        displays = GetComponentsInChildren<MoneyDisplay>();
        for(int i = 0;i < GameConstants.SUITCASE_COUNT;++i)
        {
            displays[i].moneyAmount = GameConstants.moneyAmounts[i];
        }
    }
}
