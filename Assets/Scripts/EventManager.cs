using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public const string EnemyDefeatedEvent = "EnemyDefeated";
    public const string EnemyScoreEvent = "EnemyScoreEvent";
    public const string BackWallTriggeredEvent = "BackWallTriggeredEvent";
    public const string GameOverEvent = "GameOverEvent";

    private static Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

    private static void ValidateDelegateType<T>(string eventName)
    {
        if (!eventDictionary.ContainsKey(eventName)) return;
        if (!(eventDictionary[eventName] is T))
        {
            throw new InvalidOperationException($"Event '{eventName}' does not match expected delegate type {typeof(T).Name}.");
        }
    }

    // Parameterless
    public static void Subscribe(string eventName, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = listener;
        else
            eventDictionary[eventName] = (Action)eventDictionary[eventName] + listener;
    }

    // Single parameter
    public static void Subscribe<T>(string eventName, Action<T> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = listener;
        else
            eventDictionary[eventName] = (Action<T>)eventDictionary[eventName] + listener;
    }

    // Two parameters
    public static void Subscribe<T1, T2>(string eventName, Action<T1, T2> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = listener;
        else
            eventDictionary[eventName] = (Action<T1, T2>)eventDictionary[eventName] + listener;
    }

    // Any number of parameters
    public static void Subscribe(string eventName, Action<object[]> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = listener;
        else
            eventDictionary[eventName] = (Action<object[]>)eventDictionary[eventName] + listener;
    }

    // Unsubscribe parameterless
    public static void Unsubscribe(string eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = (Action)eventDictionary[eventName] - listener;
    }

    // Unsubscribe single param
    public static void Unsubscribe<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = (Action<T>)eventDictionary[eventName] - listener;
    }

    // Unsubscribe two params
    public static void Unsubscribe<T1, T2>(string eventName, Action<T1, T2> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = (Action<T1, T2>)eventDictionary[eventName] - listener;
    }

    // Unsubscribe any params
    public static void Unsubscribe(string eventName, Action<object[]> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = (Action<object[]>)eventDictionary[eventName] - listener;
    }

    // Trigger parameterless
    public static void TriggerEvent(string eventName)
    {
        ValidateDelegateType<Action>(eventName);
        if (eventDictionary.ContainsKey(eventName))
            (eventDictionary[eventName] as Action)?.Invoke();
    }

    // Trigger single param
    public static void TriggerEvent<T>(string eventName, T arg)
    {
        Debug.Log("Triggering Event: " + eventName + " with argument " + typeof(T));
        ValidateDelegateType<Action<T>>(eventName);
        if (eventDictionary.ContainsKey(eventName))
            (eventDictionary[eventName] as Action<T>)?.Invoke(arg);
    }

    // Trigger two params
    public static void TriggerEvent<T1, T2>(string eventName, T1 arg1, T2 arg2)
    {
        ValidateDelegateType<Action<T1, T2>>(eventName);
        if (eventDictionary.ContainsKey(eventName))
            (eventDictionary[eventName] as Action<T1, T2>)?.Invoke(arg1, arg2);
    }

    // Trigger any number of params
    public static void TriggerEvent(string eventName, params object[] args)
    {
        ValidateDelegateType<Action<object[]>>(eventName);
        if (eventDictionary.ContainsKey(eventName))
            (eventDictionary[eventName] as Action<object[]>)?.Invoke(args);
    }

    public static void Clear() => eventDictionary.Clear();
}