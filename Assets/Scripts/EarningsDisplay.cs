using UnityEngine;
using UnityEngine.UI;

public class EarningsDisplay : MonoBehaviour
{
    private GameState gameState;
    void Awake()
    {
        gameState = Resources.Load<GameState>("GameStateData");
    }

    void OnEnable()
    {
        GetComponent<Text>().text = $"You Won {gameState.totalEarnings}";
    }
}
