using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Collectables")]
    [SerializeField] private TextMeshProUGUI textCounterCoin;
    [SerializeField] private TextMeshProUGUI textCounterDiamond;
    
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI textCounterTimer;
    private int textCounterInt;
    
    [SerializeField] private TextMeshProUGUI countdownText;
    private int countdownInt;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    private int scoreInt;
    
    [Header("Panels")]
    [SerializeField] private GameObject WinningPanel;
    [SerializeField] private GameObject LosingPanel;
    
    [Header("Buttons")]
    [SerializeField] private Button buttonReloadInGame;
    [SerializeField] private Button buttonReloadLevel;
    [SerializeField] private Button buttonReloadLevelWin;
    [SerializeField] private Button buttonMainMenuLose;
    [SerializeField] private Button buttonMainMenuWin;

    [SerializeField] CharacterController charactercontroller;
    [SerializeField] CollectablesManager collectablesManager;

    void Start()
    {
        WinningPanel.SetActive(false);
        LosingPanel.SetActive(false);
        
        buttonReloadInGame.onClick.AddListener(ReloadLevel);
        buttonReloadLevel.onClick.AddListener(ReloadLevel);
        buttonReloadLevelWin.onClick.AddListener(ReloadLevel);
        buttonMainMenuLose.onClick.AddListener(switchScene);
        buttonMainMenuWin.onClick.AddListener(switchScene);
        
        textCounterTimer.text = textCounterInt.ToString() + "s";
        
        StartCoroutine(Timer());
        StartCoroutine(Countdown());
    }

   

    void switchScene()
    {
        SceneManager.LoadScene(1);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
    //---- collectables ----//
    public void UpdateCoinText(int newCoinCount) 
    {
        textCounterCoin.text = newCoinCount.ToString();
        if (newCoinCount == 2)
        {
            ShowWinningPanel();
        }
    }
    
   public void UpdateDiamondText(int newDiamondCount) 
   {
       textCounterDiamond.text = newDiamondCount.ToString();
   }

    
    //---- Panels -----//
    public void ShowLosingPanel()
    {
        LosingPanel.SetActive(true);
        
    }
    public void ShowWinningPanel()
    {
        WinningPanel.SetActive(true);
        DisplayScore();
        
    }

    
    
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        for (textCounterInt = 1; ; textCounterInt++)
        {
            Debug.Log("Timer: " + textCounterInt);
            yield return new WaitForSeconds(1f);
            
            textCounterTimer.text = textCounterInt.ToString() + "s";
        }
    }
    
    public IEnumerator Countdown()
    {
        for (countdownInt = 3; countdownInt > 0; countdownInt--)
        {
            //Debug.Log("Countdown: " + countdownText.ToString());
            countdownText.text = countdownInt.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.gameObject.SetActive(false);
    }

    void DisplayScore()
    {
        scoreInt = textCounterInt - collectablesManager.counterCoins - collectablesManager.counterDiamonds;
        scoreText.text = scoreInt.ToString() + "s";
    }

}
