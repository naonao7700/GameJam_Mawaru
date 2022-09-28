using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject enemyPrefab;　// 生成対象
    public float span; // 生成間隔
    private int count; 
    void Start()
    {
        //指定関数を指定後から指定秒間隔で呼び出す命令書を発行
        InvokeRepeating("Spawn", 0, span);//Updateから呼ばない
    }


    void Update()
    {
        //if (count % span == 0)
        //{
        //    Spawn();
        //    span -=1f;
        //}
        //count++;
    }

    //生成
    void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);

    }
}
