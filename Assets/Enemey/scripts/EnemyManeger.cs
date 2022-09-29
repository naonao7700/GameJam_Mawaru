using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    public GameObject[] Enemyspawn;

    [SerializeField] float _count2;
    [SerializeField] int count;
    // Start is called before the first frame update
    void Start()
    {
        _count2 = 0.0f;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if( count == 0 )
        {
            Spawn(1);
            count++;
        }
        if(count == 3)
        {
            Spawn(1);
            count++;
        }
        if (count == 6)
        {
            Spawn(2);
            count++;
        }
        if (count == 9)
        {
            Spawn(3);
            count++;
        }
        if (count == 12)
        {
            Spawn(4);
            count++;

        }
        if (count == 15)
        {
            Spawn(5);
            count++;
        }
        if (count== 18)
        {
            Spawn(6);
            count++;

        }
        if (count == 21)
        {
            Spawn(7);
            count++;

        }
        if (count == 24)
        {
            Spawn(8);
            count++;

        }
        if (count == 27)
        {
            Spawn(0);
            count++;

        }
        if (count == 30)
        {
            count = 0;
        }

        _count2 += Time.deltaTime;
        if( _count2 > 1.0f )
        {
            _count2 = 0.0f;
            count++;
        }
    }
    void Spawn(int i)
    {
        Instantiate(Enemyspawn[i],transform.position,transform.rotation);

    }
}
