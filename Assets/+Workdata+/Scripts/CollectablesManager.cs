using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] private int counterCoins = 0;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        counterCoins = 0;
        uiManager.UpdateCoinText(counterCoins);
    }
    public void AddCoin()
    { 
        counterCoins++;
        uiManager.UpdateCoinText(counterCoins);
    }
}
