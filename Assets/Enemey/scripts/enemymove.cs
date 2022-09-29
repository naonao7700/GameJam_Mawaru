using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    public float speed; // 移動速度
    private GameObject player;　// 標的
    private Vector3 dir; // 移動方向
    private GameObject circle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circle = GameObject.FindGameObjectWithTag("Circle");
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            return;
        }

        // 方向 = 相手の座標 - 自分の座標;
        dir = player.transform.position - transform.position;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up,dir);
        //.normalized 正規化して長さを１に
        //Update()で移動する場合は　Time.deltaTime をかける
        // Updateの更新にかかった時間が入っている

        if (HitCheck(0, 0, this.transform.position.x, transform.position.y, circle.transform.localScale.x / 2, 0.8f) == true)
        {
            //Destroy(gameObject);
            //float speed2 = speed;
            //transform.Translate(dir.normalized * speed2 * Time.deltaTime);
            //speed2 -= 0.01f;
            //if (speed2 < 0)
            //    speed2 = 0;
        }

        transform.Translate(Vector3.up/*dir.normalized*/ * speed * Time.deltaTime * GameManager.TimeRate );
        //if (HitCheck(0, 0, this.transform.position.x, transform.position.y, circle.transform.localScale.x / 2, 0.8f) == false)
        //{
        //}
    }

    //円同士の当たり判定
    bool HitCheck(float x1, float y1, float x2, float y2, float r1, float r2)
    {
        float len = CalcLength(x1, y1, x2, y2);
        return (len <= (r1 + r2)) ? true : false;
    }

    //2点間の距離
    float CalcLength(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

    }
}