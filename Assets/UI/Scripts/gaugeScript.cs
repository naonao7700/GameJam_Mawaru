using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaugeScript : MonoBehaviour
{
    //Size��ς���

    [SerializeField] UnityEngine.UI.Image bar;

    public void Change(float _y)
    {
        var _x = bar.rectTransform.localScale.x;

        //�󂯎����������size.y��ύX
        bar.rectTransform.localScale = new Vector2(_x, _y);
    }

}
