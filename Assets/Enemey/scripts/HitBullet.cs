using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBullet : MonoBehaviour
{
    public GameObject DethEfect;
    public GameObject StopEfect;
    public GameObject StopDethEfect;
    public AudioClip deathSE;
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
            //Instantiate(StopEfect,transform.position, transform.rotation);
            if(GameManager.TimeRate >= 0.9f)
            {
                GameManager.PlaySE(deathSE);
                Destroy(gameObject);
                Instantiate(StopDethEfect, transform.position, transform.rotation);
                GameManager.AddScore(100);
            }else if(GameManager.TimeRate == 0.0f)
            {
                foreach(Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                   

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
