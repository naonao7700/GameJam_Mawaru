using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FlagTimer
{
    private bool flag;

    private float count;
    private float time;

    public FlagTimer(float time, float count = 0.0f)
    {
        this.time = time;
        this.count = count;
        flag = false;
    }

    public void DoUpdate(float deltaTime)
    {
        if (flag)
        {
            count += deltaTime;
        }
        else
        {
            count -= deltaTime;
        }

        if (count < 0.0f) count = 0.0f;
        else if (count > time) count = time;
    }

    public float GetRate()
    {
        return count / time;
    }

    public void SetFlag(bool value)
    {
        flag = value;
    }

    public bool GetFlag()
    {
        return flag;
    }

}
