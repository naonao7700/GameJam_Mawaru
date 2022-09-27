using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Timer
{
    private float count;
    private float time;

    public Timer(float time, float count = 0.0f)
    {
        this.count = count;
        this.time = time;
    }

    public void DoUpdate(float deltaTime)
    {
        count += deltaTime;
    }

    public bool IsEnd() { return count >= time; }

    public void Reset(float rate = 0.0f)
    {
        count = time * rate;
    }

    public void Reset(float time, float rate = 0.0f)
    {
        this.time = time;
        this.count = time * rate;
    }

    public float GetRate()
    {
        return count / time;
    }

}
