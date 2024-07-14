using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public GameObject character;
    public Sprite sprite;
    public bool unlocked;
    public int unlockPrice;

    public CharacterData()
    {

    }

    public CharacterData(GameObject character, Sprite sprite, bool unlocked, int price)
    {
        this.character = character;
        this.sprite = sprite;
        this.unlocked = unlocked;
        unlockPrice = price;
    }
}
