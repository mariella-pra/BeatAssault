using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
public class PlayerEvents : MonoBehaviour
{
    public Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();
    
    // public event Action damagePlayer = delegate { };
    
    
    public void AddEvent<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            eventDictionary[eventName] = (Action<T>)existingEvent + listener;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public void RemoveEvent<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            eventDictionary[eventName] = (Action<T>)existingEvent - listener;
        }
    }

    public void PublishEvent<T>(string eventName, T arg)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            ((Action<T>)existingEvent)?.Invoke(arg);
        }
    }

    public void AddEvent(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            eventDictionary[eventName] = (Action)existingEvent + listener;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public void RemoveEvent(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            eventDictionary[eventName] = (Action)existingEvent - listener;
        }
    }

    public void PublishEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            ((Action)existingEvent)?.Invoke();
        }
    }
    // public void PublishEvent<T>(Action<T> action, T arg)
    // {
    //     action?.Invoke(arg);
    // }
    //
    // public void PublishEvent(Action action)
    // {
    //     action?.Invoke();
    // }

    // public void AddEvent<T>(ref Action<T> action, Action<T> subscriber)
    // {
    //     action += subscriber;
    // }
    //
    // public void AddEvent(ref Action action, Action subscriber)
    // {
    //     action += subscriber;
    // }
    // public void RemoveEvent<T>(ref Action<T> action, Action<T> subscriber)
    // {
    //     action -= subscriber;
    // }
    //
    // public void RemoveEvent(ref Action action, Action subscriber)
    // {
    //     action -= subscriber;
    // }
    // public void PublishNegativeFeedback()
    // {
    //     negativeFeedback?.Invoke();
    // }
    //
    // public void AddNegativeFeedback(Action subscriber)
    // {
    //     negativeFeedback += subscriber;
    // }
    // public void PublishHealthFeedback(float health)
    // {
    //     healthFeedback?.Invoke(health);
    // }
    // public void PublishDamageFeedback()
    // {
    //     damageFeedback?.Invoke();
    // }
    // public void RemoveNegativeFeedback(Action subscriber)
    // {
    //     negativeFeedback -= subscriber;
    // }
    //
    // public void AddHealthEvent(Action<float> subscriber)
    // {
    //     healthFeedback += subscriber;
    // }
    // public void RemoveHealthEvent(Action<float> subscriber)
    // {
    //     healthFeedback -= subscriber;
    // }
    // public void AddDamagePlayer(Action subscriber)
    // {
    //     damageFeedback += subscriber;
    // }
    // public void RemoveDamagePlayer(Action subscriber)
    // {
    //     damageFeedback -= subscriber;
    // }
    //
    
    
    // public void PublishPlayInstrument(int health)
    // {
    //     healthFeedback?.Invoke(health);
    // }
    // public void PublishLooseInstrument(int health)
    // {
    //     healthFeedback?.Invoke(health);
    // }

    // public void RemoveDamagePlayer(Action subscriber)
    // {
    //     damagePlayer -= subscriber;
    // }
    public void AddEvent(string eventName)
    {
        throw new NotImplementedException();
    }
}