using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public static CharacterList instance;

    [SerializeField] private List<GameObject> characters;

    public List<GameObject> Characters => characters;
    private bool initialized = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        if (initialized) return;

        characters = new List<GameObject>();
        if (HomeManager.instance != null)
        {
            foreach (var c in HomeManager.instance.AvailableCharacters)
            {
                characters.Add(c.character);
            }
        }
        initialized = true;
    }
}
