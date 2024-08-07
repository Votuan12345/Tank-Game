using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    public static int characterSelectionIndex
    {
        get => PlayerPrefs.GetInt(GameConst.CharacterSelectionKey, 0);
        set
        {
            int curCharacter = PlayerPrefs.GetInt(GameConst.CharacterSelectionKey, 0);
            if (curCharacter != value)
            {
                PlayerPrefs.SetInt(GameConst.CharacterSelectionKey, value);
                PlayerPrefs.Save();
            }
        }
    }

    public static int playerCoins
    {
        get => PlayerPrefs.GetInt(GameConst.CoinKey, 0);
        set
        {
            int curCoin = PlayerPrefs.GetInt(GameConst.CoinKey, 0);
            if (curCoin != value)
            {
                PlayerPrefs.SetInt(GameConst.CoinKey, value);
                PlayerPrefs.Save();
            }
        }
    }
}
