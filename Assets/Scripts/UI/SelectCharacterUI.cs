using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [SerializeField] private GameObject arrowLeftButton;
    [SerializeField] private GameObject arrowRightButton;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectedButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private Image characterImg;

    private List<CharacterData> characters;

    private int index = 0;

    private void Start()
    {
        index = Prefs.characterSelectionIndex;

        Initialize();

        ShowButton();
        if (characters != null && characters.Count > 0)
        {
            characterImg.sprite = characters[index].sprite;
        }
    }

    private void Initialize()
    {
        characters = HomeManager.instance.AvailableCharacters;
    }

    public void OnArrowLeftButtonClick()
    {
        if (index == 0)
        {
            index = characters.Count-1;
        }
        else
        {
            index--;
        }

        if (characters != null && characters.Count > 0)
        {
            characterImg.sprite = characters[index].sprite;
        }
        ShowButton();
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
    }

    public void OnArrowRightButtonClick()
    {
        if (index == characters.Count-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        if (characters != null && characters.Count > 0)
        {
            characterImg.sprite = characters[index].sprite;
        }
        ShowButton();
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
    }

    public void OnSelectButtonClick()
    {
        Prefs.characterSelectionIndex = index;
        ShowButton();
        if (AudioController.instance)
        {
            AudioController.instance.PlaySfx(
                AudioController.instance.buttonClickSound);
        }
    }

    public void OnBuyButtonClick()
    {
        HomeManager.instance.BuyCharacter(index);
        ShowButton();
    }

    #region ShowButton
    private void ShowButton()
    {
        if (index == Prefs.characterSelectionIndex)
        {
            ShowSelectedButton(true);
            ShowSelectButton(false);
            ShowBuyButton(false);
        }
        else if (characters[index].unlocked)
        {
            ShowSelectButton(true);
            ShowBuyButton(false);
            ShowSelectedButton(false);
        }
        else if (characters[index].unlocked == false)
        {
            ShowSelectButton(false);
            ShowBuyButton(true, characters[index].unlockPrice);
            ShowSelectedButton(false);
        }
    }

    private void ShowBuyButton(bool isShow, int price = 100)
    {
        if(buyButton != null && priceText != null)
        {
            buyButton.SetActive(isShow);
            priceText.text = price.ToString();
        }
    }

    private void ShowSelectButton(bool isShow)
    {
        if(selectButton != null)
        {
            selectButton.SetActive(isShow);
        }
    }

    private void ShowSelectedButton(bool isShow)
    {
        if(selectedButton != null)
        {
            selectedButton.SetActive(isShow);
        }
    }
    #endregion
}
