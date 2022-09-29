using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private FlagTimer timer;

    public void SetFade(bool value)
    {
        timer.SetFlag(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer.DoUpdate(Time.deltaTime);
        var color = image.color;
        color.a = timer.GetRate();
        image.color = color;
    }
}
