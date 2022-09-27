using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜ZŽÔ
public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    float speed = 0f;


    private void Update()
    {
        rigidbody2D.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
