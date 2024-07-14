using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] private Slider healthSlider;

    private SceneTransition sceneTransition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            sceneTransition = FindObjectOfType<SceneTransition>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        ShowCoinText(0.ToString());
        ShowMenuPanel(false);
        ShowGameOverPanel(false);
    }

    public void ShowCoinText(string txt)
    {
        if (m_TextMeshProUGUI == null) return;

        m_TextMeshProUGUI.text = "x" + txt;
    }

    public void ShowHealthBar(float value)
    {
        if (healthSlider == null) return;

        healthSlider.value = value;
    }

    public void ShowMenuPanel(bool value)
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(value);
        }
    }
    public void ShowGameOverPanel(bool value)
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(value);
        }
    }

    public void OnMenuButtonClick()
    {
        ShowMenuPanel(true);
        Time.timeScale = 0f;
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
    }

    public void OnResumeButtonClick()
    {
        ShowMenuPanel(false);
        Time.timeScale = 1f;
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
    }

    public void OnReplayButtonClick()
    {
        ShowMenuPanel(false);
        ShowGameOverPanel(false);
        Time.timeScale = 1f;
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        LoadAnimationEndGame();

        StartCoroutine(LoadReplayGameCoroutine());
    }

    public void OnHomeButtonClick()
    {
        ShowMenuPanel(false);
        ShowGameOverPanel(false);
        Time.timeScale = 1f;
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        LoadAnimationEndGame();

        StartCoroutine(LoadHomeGameCoroutine());
    }

    public void NextLevel()
    {
        ShowMenuPanel(false);
        ShowGameOverPanel(false);
        Time.timeScale = 1f;
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        LoadAnimationEndGame();

        StartCoroutine(LoadNextLevelCoroutine());
    }

    IEnumerator LoadReplayGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadReplayGame();
    }

    IEnumerator LoadHomeGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadHomeGame();
    }

    IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadNextLevel();
    }

    private void LoadAnimationEndGame()
    {
        sceneTransition.LoadAnimationEndGame();
    }
}
