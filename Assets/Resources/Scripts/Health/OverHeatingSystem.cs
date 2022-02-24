using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OverHeatingSystem : MonoBehaviour
{
    public float maxHeatBeforeBurn = 3;
    float actualHeat = 0;

    public float burningDamagePerSecond = 1;

    public float CoolingPerSecond = 0;

    [HideInInspector]
    public bool isBurning { get; private set; } = false;

    public UnityEvent<float> OnHeatChange;
    public UnityEvent<float> OnBurn;
    bool heatedThisFrame = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(actualHeat > 0 && !heatedThisFrame)
        {
            //Debug.Log("cool");
            actualHeat = Mathf.Clamp(actualHeat - CoolingPerSecond * Time.deltaTime, 0, Mathf.Infinity);
            OnHeatChange.Invoke(actualHeat / maxHeatBeforeBurn);

        }
        
        if (maxHeatBeforeBurn <= actualHeat)
        {
            isBurning = true;
            //Debug.Log("AAAAAAAA QUEMA!");
            OnBurn.Invoke(burningDamagePerSecond * Time.deltaTime);
        }
        else if(maxHeatBeforeBurn > actualHeat && isBurning)
        {
            isBurning = false;
        }
        heatedThisFrame = false;
    }

    public void Heat(float heatAmount)
    {
        actualHeat += heatAmount;
        OnHeatChange.Invoke(actualHeat / maxHeatBeforeBurn);
        heatedThisFrame = true;
    }
}
