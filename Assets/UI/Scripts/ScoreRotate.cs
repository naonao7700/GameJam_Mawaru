using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRotate : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(rotateSpeed);
    }
    public void Rotate(float value)
    {
        transform.Rotate(0, 0, value);
    }
}
