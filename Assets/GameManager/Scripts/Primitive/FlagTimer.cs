using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FlagTimer
{
    [SerializeField] private bool flag;

    [SerializeField] private float count;
    [SerializeField] private float time;

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
		var t = count / time;
		if (t > 1.0f) t = 1.0f;
		return t;
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
