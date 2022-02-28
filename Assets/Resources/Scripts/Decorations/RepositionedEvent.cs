using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepositionedEvent : MonoBehaviour
{
    public UnityEvent OnReposition;

    public void SendRepositioningMessage()
    {
        OnReposition.Invoke();
    }
}
