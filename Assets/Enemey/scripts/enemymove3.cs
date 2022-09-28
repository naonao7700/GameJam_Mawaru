using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove3 : MonoBehaviour
{
    public float speed; // �ړ����x
    private GameObject player;�@// �W�I
    private Vector3 dir; // �ړ�����
    private GameObject circle;
    private int count;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circle = GameObject.FindGameObjectWithTag("Circle");
        count = 0;
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
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        //.normalized ���K�����Ē������P��
        //Update()�ňړ�����ꍇ�́@Time.deltaTime ��������
        // Update�̍X�V�ɂ����������Ԃ������Ă���
        float addX = Mathf.Sin(count / 20)*3;
        Vector3 Movedir = new Vector3(addX, 1, 0);
        if (HitCheck(0, 0, this.transform.position.x, transform.position.y, circle.transform.localScale.x / 2, 0.8f) == true)
        {
            //Destroy(gameObject);
            //float speed2 = speed;
            //transform.Translate(dir.normalized * speed2 * Time.deltaTime);
            //speed2 -= 0.01f;
            //if (speed2 < 0)
            //    speed2 = 0;
        }

        if (HitCheck(0, 0, this.transform.position.x, transform.position.y, circle.transform.localScale.x / 2, 0.8f) == false)
        {
            transform.Translate(Movedir * speed * Time.deltaTime);
        }
        count++;
    }
    //�~���m�̓����蔻��
    bool HitCheck(float x1, float y1, float x2, float y2, float r1, float r2)
    {
        float len = CalcLength(x1, y1, x2, y2);
        return (len <= (r1 + r2)) ? true : false;
    }

    //2�_�Ԃ̋���
    float CalcLength(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

    }
}
