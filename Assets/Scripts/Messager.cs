using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messager
{
    public static Dictionary<GameEvent, Delegate> mEventTable = new Dictionary<GameEvent, Delegate>();

    private static bool OnAddListener(GameEvent eventType,Delegate handler)//judge if the monitor listen the right yrpe 
    {
        if (!mEventTable.ContainsKey(eventType))
        {
            mEventTable.Add(eventType, null);
        }
        Delegate d = mEventTable[eventType];
        if (d != null && d.GetType() != handler.GetType())
        {
            Debug.LogError(d.GetType().Name + "unfit" + d.GetType().Name);
            return false;
        }
        return true;

    }
    public static void AddListener(GameEvent eventType, Callback handler)  //monitor with no parameter
    {
        if (!OnAddListener(eventType, handler)) //type filter
        {
            return;
        }
        mEventTable[eventType] = (Callback)mEventTable[eventType] + handler;
    }

    public static void AddListener<T>(GameEvent eventType, Callback<T> handler)//monitor with one parameter
    {
        if (!OnAddListener(eventType, handler)) //type filter
        {
            return;
        }
        mEventTable[eventType] = (Callback<T>)mEventTable[eventType] + handler;

    }


    private static bool OnRemoveListener(GameEvent eventType, Delegate handler)//judge if the monitor Remove the right yrpe 
    {
        if (mEventTable.ContainsKey(eventType))
        {

            Delegate d = mEventTable[eventType];
            if (d == null || d.GetType() != handler.GetType())
            {
                Debug.LogError(d.GetType().Name + "unfit" + d.GetType().Name + "or null");
                return false;
            }
        }
        else
        {
            Debug.LogError("cannot find target");
            return false;
        }
        return true;

    }
    public static void RemoveListener(GameEvent eventType,Callback handler)
    {
        if (!OnRemoveListener(eventType, handler))
        {
            return;
        }
        mEventTable[eventType] = (Callback)mEventTable[eventType] - handler;
        if (mEventTable[eventType] == null)
        {
            mEventTable.Remove(eventType);
        }
    }
    public static void RemoveListener<T>(GameEvent eventType, Callback<T> handler)
    {
        if (!OnRemoveListener(eventType, handler))
        {
            return;
        }
        mEventTable[eventType] = (Callback<T>)mEventTable[eventType] - handler;
        if (mEventTable[eventType] == null)
        {
            mEventTable.Remove(eventType);
        }
    }

    public static void Broadcast(GameEvent eventTyp)
    {
        if (!mEventTable.ContainsKey(eventTyp))
        {
            return;
        }
        Delegate d;
        if(mEventTable.TryGetValue(eventTyp,out d))
        {
            Callback callback = d as Callback;
            if (callback != null)
            {
                callback();
            }
            else
            {
                Debug.LogError("null");
            }
        }
    }
    public static void Broadcast<T>(GameEvent eventTyp,T arg1)
    {
        if (!mEventTable.ContainsKey(eventTyp))
        {
            return;
        }
        Delegate d;
        if (mEventTable.TryGetValue(eventTyp, out d))
        {
            Callback<T> callback = d as Callback<T>;
            if (callback != null)
            {
                callback(arg1);
            }
            else
            {
                Debug.LogError("null");
            }
        }
    }
}