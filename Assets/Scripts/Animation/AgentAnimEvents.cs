using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventType
{
    Start, Finish, WeaponSound, DamageStart, DamageEnd
}

public class AgentAnimEvents : MonoBehaviour
{
    public event Action<EventType> OnAnimationEvent;

    public void CallAnimationEvent(EventType eventType)
    {
        OnAnimationEvent?.Invoke(eventType);
    }
}
