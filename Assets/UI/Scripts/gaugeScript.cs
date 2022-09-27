using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaugeScript : MonoBehaviour
{
    //Size‚ğ•Ï‚¦‚é

    [SerializeField] UnityEngine.UI.Image bar;

    public void Change(float _y)
    {
        var _x = bar.rectTransform.localScale.x;

        //ó‚¯æ‚Á‚½Š„‡‚Ésize.y‚ğ•ÏX
        bar.rectTransform.localScale = new Vector2(_x, _y);
    }

}
