using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBullet : MonoBehaviour
{
    public GameObject DethEfect;
    public GameObject StopDethEfect;
    bool Dethflag;
    bool StopDethflag;
    public AudioClip deathSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Dethflag == true)
        {
            //Instantiate(StopEfect,transform.position, transform.rotation);
            if(GameManager.TimeRate >= 0.9f)
            {
                if(StopDethflag == true)
                {
                    GameManager.PlaySE(deathSE);
                    Destroy(gameObject);
                    Instantiate(StopDethEfect, transform.position, transform.rotation);
                    GameManager.AddScore(100);
                }
                if(StopDethflag == false)
                {
                    GameManager.PlaySE(deathSE);
                    Destroy(gameObject);
                    Instantiate(DethEfect, transform.position, transform.rotation);
                    GameManager.AddScore(100);
                }
            }else if(GameManager.TimeRate == 0.0f)
            {
                foreach(Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                StopDethflag = true;

                //Instantiate(StopEfect, transform.position, transform.rotation);
                //GameManager.AddScore(100);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Dethflag)
            return;
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Dethflag = true;
            //Destroy(gameObject, 0.01f);
        }
    }
}
