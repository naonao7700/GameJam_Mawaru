using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaugeScript : MonoBehaviour
{
    //Sizeを変える

    [SerializeField] UnityEngine.UI.Image bar;

    public void Change(float _y)
    {
        var _x = bar.rectTransform.localScale.x;

        //受け取った割合にsize.yを変更
        bar.rectTransform.localScale = new Vector2(_x, _y);
    }

}
