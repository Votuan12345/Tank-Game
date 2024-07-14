using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform characterPos;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        characterPos = GameObject.Find("StartPoint").transform;

        if (CharacterList.instance != null && characterPos != null)
        {
            int index = Prefs.characterSelectionIndex;
            GameObject player = Instantiate(CharacterList.instance.Characters[index], 
                characterPos.position, Quaternion.identity);
            var tankController = player.GetComponentInChildren<TankController>();
            virtualCamera.Follow = tankController.transform;
        }
        else
        {
            Debug.Log("Character not found!");
        }
    }

}
