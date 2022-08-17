using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CoinManager
{
    public static int coinNum { get; private set; } = 0;

    public static UnityEvent OnCoinsChanged = new UnityEvent();
    public static void AddCoinNum(int num)
    {
        coinNum += num;

        OnCoinsChanged.Invoke();
    }

    public static void Reset()
    {
        coinNum = 0;
    }
}
