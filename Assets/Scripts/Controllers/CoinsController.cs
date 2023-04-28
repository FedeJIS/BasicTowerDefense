using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController
{
    private float _coins;
    public void UpdateCoins(float amount)
    {
        _coins += amount;
    }
    

    public float GetCoins() => _coins;


}
