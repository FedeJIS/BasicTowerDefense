using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject wonScreen;
    [SerializeField] private GameObject lostScreen;

    [SerializeField] private TextMeshProUGUI coinsAmount;

    private void Start()
    {
        GameManager.PlayerWon += DisplayScreen;
        PlayerManager.PlayerRewarded += UpdateCoinsAmount;
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

    private void UpdateCoinsAmount(int amount)
    {
        coinsAmount.text = amount.ToString();
    }
}
