using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingScreen : MonoBehaviour
{
    [SerializeField]  Button StartButton;
  
    void Start()
    {
        StartButton.onClick.AddListener(switchScene);
    }

    void switchScene()
    {
        SceneManager.LoadScene(0);
    }
}
