using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCounterCoin;
    
    [SerializeField] private GameObject WinningPanel;
    [SerializeField] private GameObject LosingPanel;
    
    [SerializeField] private Button buttonReloadLevel;
    [SerializeField] private Button buttonReloadLevelWin;
    [SerializeField] private Button buttonMainMenu;
    

    void Start()
    {
        WinningPanel.SetActive(false);
        LosingPanel.SetActive(false);
        buttonReloadLevel.onClick.AddListener(ReloadLevel);
        buttonReloadLevelWin.onClick.AddListener(ReloadLevel);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void UpdateCoinText(int newCoinCount) 
    {
        textCounterCoin.text = newCoinCount.ToString();
        if (newCoinCount == 8)
        {
            WinningPanel.SetActive(true);
        }
    }

    public void ShowLosingPanel()
    {
        LosingPanel.SetActive(true);
    }
    public void ShowWinningPanel()
    {
        WinningPanel.SetActive(true);
    }


}
