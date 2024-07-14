using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private TextMeshProUGUI playerCoinsText;
    [SerializeField] private TextMeshProUGUI notificationText;

    private void Awake()
    {
        if (homePanel != null && levelPanel != null && optionsPanel != null)
        {
            homePanel.SetActive(true);
            levelPanel.SetActive(false);
            optionsPanel.SetActive(false);
        }
    }

    public void OnPlayButtonClick()
    {
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        if (homePanel != null && levelPanel != null && optionsPanel != null)
        {
            homePanel.SetActive(false);
            levelPanel.SetActive(true);
            optionsPanel.SetActive(false);
        }
    }

    public void OnOptionsButtonClick()
    {
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        if (homePanel != null && levelPanel != null && optionsPanel != null)
        {
            homePanel.SetActive(false);
            levelPanel.SetActive(false);
            optionsPanel.SetActive(true);

            if(playerCoinsText != null)
            {
                ShowPlayerCoinsText();
            }
            if(notificationText != null)
            {
                ShowNotificationText("");
            }
        }
    }

    public void OnBackButtonClick()
    {
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        if (homePanel != null && levelPanel != null && optionsPanel != null)
        {
            homePanel.SetActive(true);
            levelPanel.SetActive(false);
            optionsPanel.SetActive(false);
        }

    }

    public void OnQuitButtonClick()
    {
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        Application.Quit();
    }

    public void OnButtonSelectLevelClick(int level)
    {
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
        HomeManager.instance.LoadNextLevel(level);
    }

    public void ShowPlayerCoinsText()
    {
        playerCoinsText.text = Prefs.playerCoins.ToString();
    }

    public void ShowNotificationText(string txt)
    {
        notificationText.text = txt;
        Invoke("ResetNotificationText", 3f);
    }

    //Invoke
    private void ResetNotificationText()
    {
        ShowNotificationText("");
    }
}
