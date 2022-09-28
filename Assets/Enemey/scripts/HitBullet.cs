using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBullet : MonoBehaviour
{
    bool Dethflag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Dethflag == true)
        {
            if(GameManager.TimeRate >= 1.0f)
            {
                Destroy(gameObject);
                GameManager.AddScore(100);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Dethflag)
            return;
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject, 0.01f);
            Dethflag = true;
            //Destroy(gameObject, 0.01f);
        }
    }
}
