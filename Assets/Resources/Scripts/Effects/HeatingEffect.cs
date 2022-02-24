using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingEffect : MonoBehaviour
{
    OverHeatingSystem heat;
    Material mat;

    private void Awake()
    {
        heat = GetComponentInParent<OverHeatingSystem>();
        mat = new Material(GetComponent<Renderer>().material);
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.black);
        GetComponent<Renderer>().material = mat;
    }

    void OnEnable()
    {
        if(heat)
            heat.OnHeatChange.AddListener(changeColor);
    }

    void OnDisable()
    {
        if(heat)
            heat.OnHeatChange.RemoveListener(changeColor);
    }

    public void changeColor(float factor)
    {
        mat.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.red, factor));
    }
}
