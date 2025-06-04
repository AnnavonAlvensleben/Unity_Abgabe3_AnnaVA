using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] private int counterCoins = 0; 
    [SerializeField] private int counterDiamonds = 0;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        counterCoins = 0;
        uiManager.UpdateCoinText(counterCoins);
        
        counterDiamonds = 0;
        uiManager.UpdateDiamondText(counterDiamonds);
    }
    public void AddCoin()
    { 
        counterCoins++;
        uiManager.UpdateCoinText(counterCoins);
    }
    
    public void AddDiamond()
    { 
        counterDiamonds++;
        uiManager.UpdateDiamondText(counterDiamonds);
    }
}
