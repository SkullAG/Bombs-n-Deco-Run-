using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinMenu : MonoBehaviour
{
    public Text text;

    private void OnEnable()
    {
        CoinManager.OnCoinsChanged.AddListener(this.UpdateValue);

        UpdateValue();
    }

    private void OnDisable()
    {
        CoinManager.OnCoinsChanged.RemoveListener(this.UpdateValue);
    }

    public void UpdateValue()
    {
        text.text = "x" + CoinManager.coinNum;
    }
}
