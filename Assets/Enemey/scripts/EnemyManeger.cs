using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    public GameObject[] Enemyspawn;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 1800)
        {
            Spawn(1);
        }
        if (count == 3600)
        {
            Spawn(2);
        }
        if (count == 5400)
        {
            Spawn(3);
        }
        if (count == 7200)
        {
            Spawn(4);
           
        }
        if (count == 9000)
        {
            Spawn(5);
          
        }
        if (count== 10800)
        {
            Spawn(6);
           
        }
        if (count == 12600)
        {
            Spawn(7);
           
        }
        if (count == 14400)
        {
            Spawn(8);
          
        }
        if (count == 16200)
        {
            Spawn(0);

        }
        if(count == 18000)
        {
            count = 0;
        }

        count++;
    }
    void Spawn(int i)
    {
        Instantiate(Enemyspawn[i],transform.position,transform.rotation);

    }
}
