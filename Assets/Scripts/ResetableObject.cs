using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableObject : MonoBehaviour
{
    public static List<ResetableObject> resetObjects = new List<ResetableObject>();

    protected virtual void Start()
    {
        if (!resetObjects.Contains(this))
        {
            resetObjects.Add(this);
        }
    }

    public static void ResetAll()
    {
        foreach(ResetableObject resetobject in resetObjects)
        {
            resetobject.Reset();
        }
    }
    public virtual void Reset()
    {
    }
}
