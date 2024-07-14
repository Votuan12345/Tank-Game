using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int coinCount = 0;

    public int CoinCount 
    { 
        get => coinCount;
        set
        {
            coinCount = value;
            if(UIManager.instance != null)
            {
                UIManager.instance.ShowCoinText(coinCount.ToString());
            }
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        UIManager.instance.ShowGameOverPanel(true);
    }

    #region LoadScence
    public void LoadHomeGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadReplayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
