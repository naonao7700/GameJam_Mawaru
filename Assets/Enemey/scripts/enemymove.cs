using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    public float speed; // 移動速度
    private GameObject player;　// 標的
    private Vector3 dir; // 移動方向
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        //.normalized 正規化して長さを１に
        //Update()で移動する場合は　Time.deltaTime をかける
        // Updateの更新にかかった時間が入っている
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }
}
