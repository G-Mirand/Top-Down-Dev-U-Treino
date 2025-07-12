using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer); 
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer); 
    }

    public void NotifyObserver(string action)
    {
        observers.ForEach((observer) =>
        {
            observer.OnNotify(action);
        });
    }
}
