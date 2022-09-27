using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    public float speed; // �ړ����x
    private GameObject player;�@// �W�I
    private Vector3 dir; // �ړ�����
    
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

        // ���� = ����̍��W - �����̍��W;
        dir = player.transform.position - transform.position;
        //.normalized ���K�����Ē������P��
        //Update()�ňړ�����ꍇ�́@Time.deltaTime ��������
        // Update�̍X�V�ɂ����������Ԃ������Ă���
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }
}
