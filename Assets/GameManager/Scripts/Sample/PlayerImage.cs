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

        //ã´äEè„ÇÃîªíËÇÃèÍçáÅAç∂âEÇÊÇËè„â∫Ç™óDêÊÇ…Ç»ÇËÇ‹Ç∑
        if (y >= Mathf.Abs(x)) { return 0; } //è„
        if (y <= -Mathf.Abs(x)) { return 2; } //â∫
        if (x < -Mathf.Abs(y)) { return 1; } //ç∂
        if (x > Mathf.Abs(y)) { return 3; } //âE

        return -1;    //Ç±Ç»Ç¢ÇÕÇ∏
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
