using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject wonScreen;
    [SerializeField] private GameObject lostScreen;

    private void Start()
    {
        GameManager.PlayerWon += DisplayScreen;
    }

    private void OnDestroy()
    {
        GameManager.PlayerWon -= DisplayScreen;
    }

    private void DisplayScreen(bool hasWon)
    {
        if (hasWon)
        {
            Instantiate(wonScreen);
            return;
        }
        
        Instantiate(lostScreen);
        
    }
}
