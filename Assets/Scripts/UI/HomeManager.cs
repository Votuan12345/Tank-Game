using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public static HomeManager instance;

    [SerializeField] private List<CharacterData> availableCharacters;

    private HomeUI homeUI;
    private int playerCoins = 0;

    public int PlayerCoins 
    { 
        get => playerCoins;
        set
        {
            playerCoins = value;
            Prefs.playerCoins = playerCoins;
            homeUI.ShowPlayerCoinsText();
        }
    }

    public List<CharacterData> AvailableCharacters { get => availableCharacters;}

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
        homeUI = GetComponent<HomeUI>();
    }

    private void Start()
    {
        playerCoins = Prefs.playerCoins;
        if(availableCharacters != null && availableCharacters.Count > 0)
        {
            LoadCharacterData();
        }
        CharacterList.instance.Initialize();
    }

    public void BuyCharacter(int characterIndex)
    {
        if (availableCharacters[characterIndex].unlockPrice <= playerCoins)
        {
            PlayerCoins -= availableCharacters[characterIndex].unlockPrice;
            availableCharacters[characterIndex].unlocked = true;
            SaveCharacterData();

            if (AudioController.instance)
            {
                AudioController.instance.PlaySfx(
                    AudioController.instance.buyItemSound);
            }
        }
        else
        {
            if(homeUI != null)
            {
                string notification = "Not enough money to buy this item!";
                homeUI.ShowNotificationText(notification);
            }
        }
    }

    private void SaveCharacterData()
    {
        for (int i = 0; i < availableCharacters.Count; i++)
        {
            PlayerPrefs.SetInt("Character_" + i + "_unlocked", availableCharacters[i].unlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadCharacterData()
    {
        for (int i = 0; i < availableCharacters.Count; i++)
        {
            // 0 : locked - 1 : unlocked
            if(i == 0)
            {
                PlayerPrefs.SetInt("Character_" + i + "_unlocked", 1);
                PlayerPrefs.Save();
            }
            int unlocked = PlayerPrefs.GetInt("Character_" + i + "_unlocked", 0);
            availableCharacters[i].unlocked = unlocked == 1 ? true : false;
        }
    }

    public void LoadNextLevel(int level)
    {
        SceneManager.LoadSceneAsync("Level" + level);
    }
}
