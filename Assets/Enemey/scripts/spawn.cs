using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject enemyPrefab;�@// �����Ώ�
    public float span; // �����Ԋu
    void Start()
    {
        //�w��֐����w��ォ��w��b�Ԋu�ŌĂяo�����ߏ��𔭍s
        InvokeRepeating("Spawn", 0, span);//Update����Ă΂Ȃ�
    }


    void Update()
    {

    }

    //����
    void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);

    }
}
