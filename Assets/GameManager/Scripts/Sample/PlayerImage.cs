using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Sprite deathImage;

    [Range(0, 360)] public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int GetAngleIndex( float angle )
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        if (y == 0 && x == 0) { return -1; }

        //境界上の判定の場合、左右より上下が優先になります
        if (y >= Mathf.Abs(x)) { return 0; } //上
        if (y <= -Mathf.Abs(x)) { return 2; } //下
        if (x < -Mathf.Abs(y)) { return 1; } //左
        if (x > Mathf.Abs(y)) { return 3; } //右

        return -1;    //こないはず
    }

    // Update is called once per frame
    void Update()
    {
        if( GameManager.IsPlayerDeath() )
        {
            render.sprite = deathImage;
        }
        else
        {
            int index = GetAngleIndex(angle);
            render.sprite = images[index];
        }
    }
}
