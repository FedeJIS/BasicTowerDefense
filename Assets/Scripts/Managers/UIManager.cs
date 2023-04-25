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
    [SerializeField] private WaveComponent nextWaveScreen;

    [SerializeField] private TextMeshProUGUI coinsAmount;

   

    private void Start()
    {
        GameManager.PlayerWon += DisplayScreen;
        GameManager.WaveStarted += DisplayWave;
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

    private void DisplayWave(WaveData data)
    {
        var toDisplay = Instantiate(nextWaveScreen);
        toDisplay.WaveText.text = data.WaveName;
        Destroy(toDisplay.gameObject,GameManager.WaitTime/3f);
    }

    private void UpdateCoinsAmount(float amount)
    {
        coinsAmount.text = amount.ToString();
    }
}
